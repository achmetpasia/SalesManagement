using Application.Features.Orders.Commands.Update;
using FluentValidation;

namespace Application.Features.Orders.Commands.Rules;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        RuleFor(s => s.Id)
           .NotEqual(Guid.Empty);
    }
}
