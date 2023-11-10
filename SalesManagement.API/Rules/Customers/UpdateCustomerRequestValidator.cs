using FluentValidation;
using SalesManagement.API.Models.Customers;

namespace SalesManagement.API.Rules.Customers;

/// <summary>
/// Validator for UpdateCustomerRequest class information.
/// </summary>
public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator()
    {
        /// <summary>
        /// Validates the first name property.
        /// </summary>
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50);

        /// <summary>
        /// Validates the last name property.
        /// </summary>
        RuleFor(c => c.LastName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50);

        /// <summary>
        /// Validates the address property.
        /// </summary>
        RuleFor(c => c.Address)
            .NotEmpty()
            .NotNull()
            .MaximumLength(255); 

        /// <summary>
        /// Validates the postal code property.
        /// </summary>
        RuleFor(c => c.PostalCode)
            .NotEmpty()
            .NotNull()
            .Matches(@"^\d{5}(-\d{4})?$") 
            .WithMessage("Invalid postal code format");
    }
}