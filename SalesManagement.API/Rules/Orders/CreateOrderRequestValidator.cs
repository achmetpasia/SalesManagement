using FluentValidation;
using SalesManagement.API.Models.Orders;

namespace SalesManagement.API.Rules.Orders;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(s => s.CustomerId)
            .NotEqual(Guid.Empty);
    }
}
