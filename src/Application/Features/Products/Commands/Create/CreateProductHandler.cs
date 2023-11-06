using Application.Features.Products.Services;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.Commands.Create;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ObjectBaseResponse<CreateProductResponse>>
{
    private readonly IProductCommandService _productCommandService;
    private readonly IMapper _mapper;

    public CreateProductHandler(IProductCommandService productCommandService, IMapper mapper)
    {
        _productCommandService = productCommandService;
        _mapper = mapper;
    }

    public async Task<ObjectBaseResponse<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var createdEntity = await _productCommandService.CreateAsync(request);

        var response = _mapper.Map<ObjectBaseResponse<CreateProductResponse>>(createdEntity);

        return response;
    }
}
