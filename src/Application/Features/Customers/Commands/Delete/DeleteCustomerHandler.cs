using Application.Exceptions;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using Domain.Entites.Core;
using Domain.Entites.Customers;
using MediatR;

namespace Application.Features.Customers.Commands.Delete;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, ResponseBase>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomerHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseBase> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _customerRepository.FindByIdAsync(request.Id);
        if (entity == null) throw new NotFoundException("This Customer is not exist.");
        
        _customerRepository.Delete(entity);
        await _unitOfWork.SaveChangesAsync();

        return new ResponseBase() { StatusCode = System.Net.HttpStatusCode.NoContent, Message = "Delete Successfully"};
    }
}
