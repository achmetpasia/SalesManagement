using Application.Features.Products.Commands.Create;
using FluentValidation;

namespace Application.Features.Products.Commands.Rules;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(c => c.Name)
           .NotEmpty()
           .NotNull();

        RuleFor(c => c.Price)
            .GreaterThan(0);
    }
}
