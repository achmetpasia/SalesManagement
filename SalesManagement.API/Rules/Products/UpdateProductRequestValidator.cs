using FluentValidation;
using SalesManagement.API.Models.Products;

namespace SalesManagement.API.Rules.Products
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator() 
        {
            RuleFor(c => c.Name)
          .NotEmpty()
          .NotNull();

            RuleFor(c => c.Price)
                .GreaterThan(0);
        }
    }
}
