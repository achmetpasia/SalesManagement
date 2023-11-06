using Application.Features.Customers.Services;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using MediatR;

namespace Application.Features.Customers.Commands.Update;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, ObjectBaseResponse<UpdateCustomerResponse>>
{
    private readonly ICustomerCommandService _customerCommandService;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(ICustomerCommandService customerCommandService, IMapper mapper)
    {
        _customerCommandService = customerCommandService;
        _mapper = mapper;
    }

    public async Task<ObjectBaseResponse<UpdateCustomerResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _customerCommandService.UpdateAsync(request);

        var response = _mapper.Map<ObjectBaseResponse<UpdateCustomerResponse>>(entity);

        return response;
    }
}
