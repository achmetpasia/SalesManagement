using FluentValidation;
using SalesManagement.API.Models.Orders;

namespace SalesManagement.API.Rules.Orders;

/// <summary>
/// Validator for CreateOrderRequest Class information.
/// </summary>
public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        /// <summary>
        /// Validates the customer identifier property.
        /// </summary>
        RuleFor(s => s.CustomerId)
            .NotEqual(Guid.Empty);
    }
}
