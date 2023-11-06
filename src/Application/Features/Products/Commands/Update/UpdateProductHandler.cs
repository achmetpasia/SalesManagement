using Application.Features.Products.Services;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ObjectBaseResponse<UpdateProductResponse>>
{
    private readonly IProductCommandService _productCommandService;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IProductCommandService productCommandService, IMapper mapper)
    {
        _productCommandService = productCommandService;
        _mapper = mapper;
    }

    public async Task<ObjectBaseResponse<UpdateProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _productCommandService.UpdateAsync(request);

        var response = _mapper.Map<ObjectBaseResponse<UpdateProductResponse>>(entity.Data);

        return response;
    }
}
