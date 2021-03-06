
using FluentValidation;
using ProjeCoreOrnekOzellikler.Entities;

namespace ProjeCoreOrnekOzellikler.Validator
{
    public class ProductValidator : AbstractValidator<cdItem>
    {
        public ProductValidator()
        {

            RuleFor(t => t.ItemName).NotNull();
            RuleFor(t => t.ItemName).NotEqual("adam").WithMessage("Urun adi adam olamaz");

            RuleFor(t => t.Price).GreaterThan(50).When(x => x.CategoryId == 30).WithMessage("Topuklu kategorisinden urun sectiniz fiyat 50 den buyuk olmak zorunda");

            RuleFor(x => x.ItemName)
                .Must(x => x.StartsWith("PR"))
                .WithMessage("Urun adi PR ile baslamalidir");

     

         
            

         
        }
    }
}
