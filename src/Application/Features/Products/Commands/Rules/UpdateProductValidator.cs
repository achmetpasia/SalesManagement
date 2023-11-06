using Application.Features.Products.Commands.Update;
using FluentValidation;

namespace Application.Features.Products.Commands.Rules;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(s => s.Id)
           .NotEqual(Guid.Empty);

        RuleFor(c => c.Name)
          .NotEmpty()
          .NotNull();

        RuleFor(c => c.Price)
            .GreaterThan(0);
    }
}
