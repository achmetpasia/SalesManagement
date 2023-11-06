using Application.Features.Orders.Commands.Create;
using FluentValidation;

namespace Application.Features.Orders.Commands.Rules;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(s => s.CustomerId)
          .NotEqual(Guid.Empty);
    }
}
