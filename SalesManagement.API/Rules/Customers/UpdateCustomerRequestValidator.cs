using FluentValidation;
using SalesManagement.API.Models.Customers;

namespace SalesManagement.API.Rules.Customers
{
    public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerRequestValidator()
        {
            RuleFor(c => c.FirstName)
            .NotEmpty()
            .NotNull();

            RuleFor(c => c.LastName)
                .NotEmpty()
                .NotNull();

            RuleFor(c => c.Address)
                .NotEmpty()
                .NotNull();

            RuleFor(c => c.PostalCode)
                .NotEmpty()
                .NotNull();
        }
    }
}
