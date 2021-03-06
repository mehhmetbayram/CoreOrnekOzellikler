using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjeCoreOrnekOzellikler.Helper;
using ProjeCoreOrnekOzellikler.Identity;
using ProjeCoreOrnekOzellikler.Models.Security;

namespace ProjeCoreOrnekOzellikler.Controllers
{
    public class SecurityController : Controller
    {

        UserManager<AppIdentityUser> _userManager;
        SignInManager<AppIdentityUser> _signInManager;
        private IHttpContextAccessor _httpContextAccessor;
        string baseUri;
        public SecurityController(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager,IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
         
            baseUri = _httpContextAccessor.HttpContext.Request.Scheme+ "://" + _httpContextAccessor.HttpContext.Request.Host.Value;
          
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Login & Logout
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }



            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError("", "EMail address is not verified");
                    return View(loginViewModel);
                }


            }

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Hesabınız kilitlendi");
                return View(loginViewModel);
            }

            return View(loginViewModel);

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Security");
        }
        #endregion

        #region AccessDenied
        public IActionResult AccessDenied()
        {

            //Yetkiniz yok mesajini verecegimiz sayfa
            return View();
        } 
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = new AppIdentityUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.EMail,


            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                
                string callBackUrl = baseUri + Url.Action("ConfirmEmail", "Security", new { userId = user.Id, code = confirmationCode });

                bool statusEMail = MailSenderHelper.SendMail(registerViewModel.EMail, callBackUrl,"Confirm Email");

                if (user.EmailConfirmed)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred in the mail validation process");
                    return View(registerViewModel);
                }

            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
            return View(registerViewModel);
        }
        #endregion

        #region ConfirmEmail
       
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {

                //Hata mesaji ile yollanabilir view'a 
                return RedirectToAction("Index", "Home");
            }

            //Model binding ile gelen datalar ile kullaniciyi id' ye gore buluyoruz
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException("Unable to find user");

            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                //Email dogrulandi mesaji ile birlikte view'a yonlendiriyoruz

                TempData["userMessage"] = $"{user.UserName}--{user.Email} eposta adresi dogrulandi ";

                return View("ConfirmEmail", "Security");
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Forgot & Reset Password
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {

                TempData["userMessage"] = "Please write email address";
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                TempData["userMessage"] = "Email address not found";
                return View();
            }

            var confirmationCode = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callBackUrl =baseUri+Url.Action("ResetPassword", "Security", new { userId = user.Id, code = confirmationCode });

            bool statusEmail = MailSenderHelper.SendMail(email, callBackUrl,"Reset Password");

            if (statusEmail)
            {
                return RedirectToAction("ForgotPasswordEmailSend");
            }
            return View();


        }


        public IActionResult ForgotPasswordEmailSend()
        {
            return View();
        }

        public    IActionResult ResetPassword(string userId, string code)
        {
            if (userId == null || code == null)
            {
                throw new ApplicationException("User Id or Code must be supplied for reset password");
            }

            var model = new ResetPasswordViewModel()
            {
                Code = code,
                UserId=userId
               

            };

         

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(resetPasswordViewModel);
            }

            var user = await _userManager.FindByIdAsync(resetPasswordViewModel.UserId);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(resetPasswordViewModel);
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);

            if (result.Succeeded)
            {
                //Bilgilendirme mesaji ile birlikte
                return RedirectToAction("ResetPasswordConfirm", "Security");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View(resetPasswordViewModel);
                }
            }
            return View();
        }

        public IActionResult ResetPasswordConfirm()
        {
            return View();
        } 
        #endregion
    }
}