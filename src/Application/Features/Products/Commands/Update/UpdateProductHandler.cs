using Application.Exceptions;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Core;
using Domain.Entites.Products;
using MediatR;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ObjectBaseResponse<UpdateProductResponse>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(IMapper mapper, IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ObjectBaseResponse<UpdateProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.FindByIdAsync(request.Id);
        if (entity == null) throw new NotFoundException("This product dont exist.");

        var isExist = await _productRepository.IsExistAsync(s => s.Name == request.Name && s.Id != request.Id);
        if (isExist) throw new ConflictException("This product Already exist.");

        entity.SetName(request.Name);
        entity.SetPrice(request.Price);

        _productRepository.Update(entity, DateTime.UtcNow);
        await _unitOfWork.SaveChangesAsync();

        var response = _mapper.Map<ObjectBaseResponse<UpdateProductResponse>>(entity);

        return response;
    }
}
