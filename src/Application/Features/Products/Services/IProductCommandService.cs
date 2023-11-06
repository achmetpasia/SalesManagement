using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Dtos;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using Application.Utilities.Common.ResponseBases.Concrate;
using Domain.Entites.Products;

namespace Application.Features.Products.Services;

public interface IProductCommandService
{
    Task<ObjectBaseResponse<ProductDto>> CreateAsync(CreateProductCommand command);
    Task<ObjectBaseResponse<ProductDto>> UpdateAsync(UpdateProductCommand command);
    Task<ResponseBase> DeleteAsync(DeleteProductCommand command);
}
