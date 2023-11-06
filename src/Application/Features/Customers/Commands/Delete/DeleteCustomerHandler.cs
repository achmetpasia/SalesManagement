using Application.Features.Customers.Services;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using MediatR;

namespace Application.Features.Customers.Commands.Delete;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, ResponseBase>
{
    private readonly ICustomerCommandService _customerCommandService;

    public DeleteCustomerHandler(ICustomerCommandService customerCommandService)
    {
        _customerCommandService = customerCommandService;
    }

    public async Task<ResponseBase> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var success = await _customerCommandService.DeleteAsync(request);

        return success;
    }
}
