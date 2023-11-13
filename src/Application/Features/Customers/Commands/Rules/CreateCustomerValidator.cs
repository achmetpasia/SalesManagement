using Application.Features.Customers.Commands.Create;
using FluentValidation;

namespace Application.Features.Customers.Commands.Rules;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
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
            .NotNull()
            .Matches(@"^\d{5}(-\d{4})?$")
            .WithMessage("Invalid postal code format");
    }
}
