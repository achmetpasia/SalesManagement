using Application.Exceptions;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Core;
using Domain.Entites.Products;
using MediatR;

namespace Application.Features.Products.Commands.Create;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ObjectBaseResponse<CreateProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(IMapper mapper, IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ObjectBaseResponse<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _productRepository.IsExistAsync(s => s.Name == request.Name);
        if (isExist) throw new ConflictException("This product already exist.");

        var entity = new Product(request.Name, request.Price);

        await _productRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        var response = _mapper.Map<ObjectBaseResponse<CreateProductResponse>>(entity);

        return response;
    }
}
