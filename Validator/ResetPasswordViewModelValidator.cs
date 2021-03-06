using FluentValidation;
using ProjeCoreOrnekOzellikler.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Validator
{
    public class ResetPasswordViewModelValidator:AbstractValidator<ResetPasswordViewModel>
    {
        public ResetPasswordViewModelValidator()
        {
            RuleFor(t => t.Code).NotEmpty();

            //RuleFor(t => t.EMail).NotEmpty();
            //RuleFor(t => t.EMail).EmailAddress();

            RuleFor(t => t.Password).NotEmpty();
            RuleFor(t => t.Password).Equal(t => t.ConfirmPassword);
            RuleFor(t => t.ConfirmPassword).NotEmpty();
        }
    }
}
