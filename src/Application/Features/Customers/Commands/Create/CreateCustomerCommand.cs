using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;

namespace Application.Features.Customers.Commands.Create;

public class CreateCustomerCommand : IRequest<ObjectBaseResponse<CreateCustomerResponse>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }

    public CreateCustomerCommand(string firstName, string lastName, string address, string postalCode)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        PostalCode = postalCode;
    }
}
