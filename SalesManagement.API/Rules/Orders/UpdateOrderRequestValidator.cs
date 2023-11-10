using FluentValidation;
using SalesManagement.API.Models.Orders;

namespace SalesManagement.API.Rules.Orders;

/// <summary>
/// Validator for UpdateOrderRequest class.
/// </summary>
public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator()
    {
        /// <summary>
        /// Validates the order identifier property.
        /// </summary>
        RuleFor(s => s.Id)
            .NotEqual(Guid.Empty);
    }
}
