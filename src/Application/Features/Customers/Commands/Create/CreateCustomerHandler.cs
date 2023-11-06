using Application.Features.Customers.Services;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using MediatR;

namespace Application.Features.Customers.Commands.Create;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, ObjectBaseResponse<CreateCustomerResponse>>
{
    private readonly ICustomerCommandService _customerCommandService;
    private readonly IMapper _mapper;

    public CreateCustomerHandler(ICustomerCommandService customerCommandService, IMapper mapper)
    {
        _customerCommandService = customerCommandService;
        _mapper = mapper;
    }

    public async Task<ObjectBaseResponse<CreateCustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var createdEntity = await _customerCommandService.CreateAsync(request);
        
        var response = _mapper.Map<ObjectBaseResponse<CreateCustomerResponse>>(createdEntity);

        return response;
    }
}
