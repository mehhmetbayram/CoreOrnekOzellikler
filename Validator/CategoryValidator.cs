using FluentValidation;
using ProjeCoreOrnekOzellikler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Validator
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(t => t.CategoryName)
                .NotEmpty()
                .WithMessage("Kategori Adi Girin");
        }
    }
}
