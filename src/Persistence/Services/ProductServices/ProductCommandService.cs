using Application.Abstarctions;
using Application.Abstarctions.Repositories.ProductRepositories;
using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Dtos;
using Application.Features.Products.Services;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using Application.Utilities.Common.ResponseBases.Concrate;
using Domain.Entites.Products;

namespace Persistence.Services.ProductServices;

public class ProductCommandService : IProductCommandService
{
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductCommandService(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IUnitOfWork unitOfWork)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ObjectBaseResponse<ProductDto>> CreateAsync(CreateProductCommand command)
    {
        var isExist = await _productReadRepository.IsExistsAsync(s => s.Name == command.Name);
        if (isExist) return new ObjectBaseResponse<ProductDto>(System.Net.HttpStatusCode.Conflict, "Already exist.");

        var entity = new Product(command.Name, command.Price);

        await _productWriteRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return new ObjectBaseResponse<ProductDto>(new ProductDto(entity.Id), System.Net.HttpStatusCode.Created);
    }

    public async Task<ResponseBase> DeleteAsync(DeleteProductCommand command)
    {
        var entity = await _productReadRepository.FindByIdAsync(command.Id);
        if (entity == null) return new ResponseBase() { StatusCode = System.Net.HttpStatusCode.NotFound, Message = "Product dont exist." };

        _productWriteRepository.HardDelete(entity);
        await _unitOfWork.SaveChangesAsync();

        return new ResponseBase() { StatusCode = System.Net.HttpStatusCode.NoContent, Message = "Delete Successfully" };
    }

    public async Task<ObjectBaseResponse<ProductDto>> UpdateAsync(UpdateProductCommand command)
    {
        var entity = await _productReadRepository.FindByIdAsync(command.Id);
        if (entity == null) return new ObjectBaseResponse<ProductDto>(System.Net.HttpStatusCode.NotFound, "Product dont exist.");

        var isExist = await _productReadRepository.IsExistsAsync(s => s.Name == command.Name && s.Id != command.Id);
        if (isExist) return new ObjectBaseResponse<ProductDto>(System.Net.HttpStatusCode.Conflict, "Already exist.");

        entity.SetName(command.Name);
        entity.SetPrice(command.Price);

        _productWriteRepository.Update(entity, DateTime.UtcNow);
        await _unitOfWork.SaveChangesAsync();

        return new ObjectBaseResponse<ProductDto>(new ProductDto(entity.Id), System.Net.HttpStatusCode.OK);
    }
}
