using Application.Exceptions;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Core;
using Domain.Entites.Customers;
using MediatR;

namespace Application.Features.Customers.Commands.Create;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, ObjectBaseResponse<CreateCustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCustomerHandler(IMapper mapper, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ObjectBaseResponse<CreateCustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _customerRepository.IsExistAsync(s => s.FirstName == request.FirstName && s.LastName == request.LastName);
        if (isExist) throw new ConflictException("This Customer already exist.");

        var entity = new Customer(request.FirstName, request.LastName, request.Address, request.PostalCode);

        await _customerRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync();
     
        var response = _mapper.Map<ObjectBaseResponse<CreateCustomerResponse>>(entity);

        return response;
    }
}
