using FluentValidation;
using SalesManagement.API.Models.Products;

namespace SalesManagement.API.Rules.Products;

/// <summary>
/// Validator for  UpdateProductRequest class.
/// </summary>
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        /// <summary>
        /// Validates the product name property.
        /// </summary>
        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull();

        /// <summary>
        /// Validates the product price property.
        /// </summary>
        RuleFor(c => c.Price)
            .GreaterThan(0);
    }
}
