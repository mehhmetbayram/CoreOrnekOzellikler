using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using FormHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjeCoreOrnekOzellikler.Business.Abstract;
using ProjeCoreOrnekOzellikler.Business.Concrete;
using ProjeCoreOrnekOzellikler.Entities;
using ProjeCoreOrnekOzellikler.Identity;
using ProjeCoreOrnekOzellikler.Models.Security;
using ProjeCoreOrnekOzellikler.Validator;

namespace ProjeCoreOrnekOzellikler
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddFormHelper(new FormHelperConfiguration
            {
                CheckTheFormFieldsMessage = "Please Complete Verification"
            });
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddMvc()
                .AddFluentValidation()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);

            services.AddIdentity<AppIdentityUser, AppIdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;


                options.Lockout.MaxFailedAccessAttempts = 6;//Yanlis sifre girme siniri
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);//30 dakika giris yapamayacak
                options.Lockout.AllowedForNewUsers = true;//Yeni kullanicilar icinde gecerli


                options.User.RequireUniqueEmail = true;//Email tekil olacak

                options.SignIn.RequireConfirmedEmail = true;//Login email onayli olsun mu 
                options.SignIn.RequireConfirmedPhoneNumber = false;//Login telefon onayli olsunmu

            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Security/Login";
                options.LogoutPath = "/Security/Logout";
                options.AccessDeniedPath = "/Security/AccessDenied";//Yetkisi yok ise yonledirilecek sayfa
                options.SlidingExpiration = true;//Kullanici Logout default suresi kullanici yeniden giris yaptiginda  yenilenecekmi

                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = "Aponi.Cookie.Name",//Cookie name degistiriyoruz
                    Path = "/",//Cookileri root ta tut
                    SameSite = SameSiteMode.Lax,//arastir
                    SecurePolicy = CookieSecurePolicy.SameAsRequest,//Yapilan istekle ayni olacak sekilde ayarliyoruz


                };

               


            });

            services.AddDbContext<DataContext>(options => options.UseSqlServer(_configuration.GetConnectionString("CoreConnection")));

            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("CoreConnection")));

            services.AddTransient<IValidator<Category>, CategoryValidator>();
            services.AddScoped<IValidator<cdItem>, ProductValidator>();
            services.AddScoped<IValidator<LoginViewModel>, LoginViewModelValidator>();
            services.AddScoped<IValidator<RegisterViewModel>, RegisterViewModelValidator>();
            services.AddScoped<IValidator<ResetPasswordViewModel>, ResetPasswordViewModelValidator>();
            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<ICartSessionService, CartSessionManager>();
            services.AddSession(RegisterSessionOptions);
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();

           
        }

        void RegisterSessionOptions(SessionOptions sessionOptions)
        {
            sessionOptions.IdleTimeout = TimeSpan.FromDays(1);
            sessionOptions.IOTimeout = TimeSpan.FromDays(2);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //env.EnvironmentName = EnvironmentName.Production;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute
                (
                        name: "default",
                        pattern: "{controller}/{action}/{id?}",
                        defaults: new { controller = "Home", action = "Index" }

                        );

            });





            app.UseFileServer();
            app.UseStaticFiles();
            //app.UseNodeModules(env.ContentRootPath);
        }
    }
}
