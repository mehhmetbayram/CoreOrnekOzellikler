using FluentValidation;
using ProjeCoreOrnekOzellikler.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Validator
{
    public class RegisterViewModelValidator:AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(t => t.UserName).NotEmpty();

            RuleFor(t => t.Password).NotEmpty();
            RuleFor(t => t.Password).Equal(t => t.ConfirmPassword);

            RuleFor(t => t.ConfirmPassword).NotEmpty();
           
            RuleFor(t => t.EMail).NotEmpty();
            RuleFor(t => t.EMail).EmailAddress();
            
        }
    }
}
