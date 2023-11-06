using Application.Abstarctions;
using Application.Abstarctions.Repositories.CustomerRepositories;
using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Dtos;
using Application.Features.Customers.Commands.Update;
using Application.Features.Customers.Services;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using Application.Utilities.Common.ResponseBases.Concrate;
using Domain.Entites.Customers;

namespace Persistence.Services.CustomerService;

public class CustomerCommandService : ICustomerCommandService
{
    private readonly ICustomerWriteRepository _customerWriteRepository;
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerCommandService(ICustomerWriteRepository customerWriteRepository, ICustomerReadRepository customerReadRepository, IUnitOfWork unitOfWork)
    {
        _customerWriteRepository = customerWriteRepository;
        _customerReadRepository = customerReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ObjectBaseResponse<CustomerDto>> CreateAsync(CreateCustomerCommand command)
    {
        var isExist = await _customerReadRepository.IsExistsAsync(s => s.FirstName == command.FirstName && s.LastName == command.LastName);
        if (isExist) return new ObjectBaseResponse<CustomerDto>(System.Net.HttpStatusCode.Conflict, "Already exist.");
        
        var entity = new Customer(command.FirstName, command.LastName, command.Address, command.PostalCode);
        await _customerWriteRepository.CreateAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return new ObjectBaseResponse<CustomerDto>(new CustomerDto(entity.Id), System.Net.HttpStatusCode.Created);
    }

    public async Task<ResponseBase> DeleteAsync(DeleteCustomerCommand command)
    {
        var entity = await _customerReadRepository.FindByIdAsync(command.Id);

        if (entity == null) return new ResponseBase() { StatusCode = System.Net.HttpStatusCode.NotFound, Message = "Customer dont exist."};

        _customerWriteRepository.HardDelete(entity);

        await _unitOfWork.SaveChangesAsync();
        
        return new ResponseBase() { StatusCode = System.Net.HttpStatusCode.NoContent, Message = "Delete Successfully" }; 
    }

    public async Task<ObjectBaseResponse<CustomerDto>> UpdateAsync(UpdateCustomerCommand command)
    {
        var entity = await _customerReadRepository.FindByIdAsync(command.Id);

        if (entity == null) return new ObjectBaseResponse<CustomerDto>(System.Net.HttpStatusCode.NotFound, "Customer dont exist.");

        var isExist = await _customerReadRepository.IsExistsAsync(s => s.FirstName == command.FirstName && s.LastName == command.LastName && s.Id != command.Id);
        if (isExist) return new ObjectBaseResponse<CustomerDto>(System.Net.HttpStatusCode.Conflict, "Already exist.");

        entity.SetFirstName(command.FirstName);
        entity.SetLastName(command.LastName);
        entity.SetAddress(command.Address);
        entity.SetPostalCode(command.PostalCode);

        _customerWriteRepository.Update(entity, DateTime.UtcNow);
        await _unitOfWork.SaveChangesAsync();

        return new ObjectBaseResponse<CustomerDto>(new CustomerDto(entity.Id), System.Net.HttpStatusCode.OK);
    }
}
