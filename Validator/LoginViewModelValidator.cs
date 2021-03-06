using FluentValidation;
using ProjeCoreOrnekOzellikler.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Validator
{
    public class LoginViewModelValidator:AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(t => t.UserName).NotEmpty();
            RuleFor(t => t.Password).NotEmpty();
        }
    }
}
