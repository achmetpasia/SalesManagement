using FluentValidation;
using SalesManagement.API.Models.Orders;

namespace SalesManagement.API.Rules.Orders;

public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator()
    {
        RuleFor(s => s.Id)
       .NotEqual(Guid.Empty);
    }
}
