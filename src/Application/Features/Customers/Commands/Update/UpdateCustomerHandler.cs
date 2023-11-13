using Application.Exceptions;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Core;
using Domain.Entites.Customers;
using MediatR;

namespace Application.Features.Customers.Commands.Update;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, ObjectBaseResponse<UpdateCustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(IMapper mapper, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ObjectBaseResponse<UpdateCustomerResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _customerRepository.FindByIdAsync(request.Id);
        if (entity == null) throw new NotFoundException("Customer dont exist.");

        var isExist = await _customerRepository.IsExistAsync(s => s.FirstName == request.FirstName && s.LastName == request.LastName && s.Id != request.Id);
        if (isExist) throw new ConflictException($"This customer with {request.FirstName} and {request.LastName} already exist.");

        entity.SetFirstName(request.FirstName);
        entity.SetLastName(request.LastName);
        entity.SetAddress(request.Address);
        entity.SetPostalCode(request.PostalCode);

        _customerRepository.Update(entity, DateTime.UtcNow);
        await _unitOfWork.SaveChangesAsync();

        var response = _mapper.Map<ObjectBaseResponse<UpdateCustomerResponse>>(entity);

        return response;
    }
}
