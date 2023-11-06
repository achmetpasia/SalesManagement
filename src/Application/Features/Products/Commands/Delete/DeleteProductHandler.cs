using Application.Features.Products.Services;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using MediatR;

namespace Application.Features.Products.Commands.Delete;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ResponseBase>
{
    private readonly IProductCommandService _productCommandService;

    public DeleteProductHandler(IProductCommandService productCommandService)
    {
        _productCommandService = productCommandService;
    }

    public async Task<ResponseBase> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var success = await _productCommandService.DeleteAsync(request);

        return success;
    }
}
