using Application.Exceptions;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using Domain.Entites.Core;
using Domain.Entites.Products;
using MediatR;

namespace Application.Features.Products.Commands.Delete;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ResponseBase>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseBase> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.FindByIdAsync(request.Id);
        if (entity == null) throw new NotFoundException("This product dont exist.");

        _productRepository.Delete(entity);
        await _unitOfWork.SaveChangesAsync();

        return new ResponseBase() { StatusCode = System.Net.HttpStatusCode.NoContent, Message = "Delete Successfully" };
    }
}
