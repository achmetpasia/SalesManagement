using Application.Abstarctions;
using Application.Abstarctions.Repositories.CustomerRepositories;
using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Dtos;
using Application.Features.Customers.Commands.Update;
using Application.Utilities.Common.ResponseBases.Concrate;
using Domain.Entites.Customers;
using FakeItEasy;
using Persistence.Services.CustomerService;
using System.Linq.Expressions;


namespace Test.Persistence.Customers;

public class CustomerCommandServiceTests
{
    private readonly ICustomerWriteRepository _customerWriteRepository;
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CustomerCommandService _customerCommandService;

    public CustomerCommandServiceTests()
    {
        _customerWriteRepository = A.Fake<ICustomerWriteRepository>();
        _customerReadRepository = A.Fake<ICustomerReadRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _customerCommandService = new CustomerCommandService(_customerWriteRepository, _customerReadRepository, _unitOfWork);
    }

    [Fact]
    public async Task ShouldSuccess_CreateCustomer()
    {
        var command = GenerateCreateCustomerCommandInstance();

        A.CallTo(() => _customerReadRepository.IsExistsAsync(A<Expression<Func<Customer, bool>>>.Ignored)).Returns(false);

        var result = await _customerCommandService.CreateAsync(command);

        Assert.IsType<ObjectBaseResponse<CustomerDto>>(result);
    }

    [Fact]
    public async Task ShouldFail_CreateCustomer_IfCustomerExists()
    {
        var command = GenerateCreateCustomerCommandInstance();

        A.CallTo(() => _customerReadRepository.IsExistsAsync(A<Expression<Func<Customer, bool>>>.Ignored)).Returns(true);

        var result = await _customerCommandService.CreateAsync(command);

        Assert.Equal(System.Net.HttpStatusCode.Conflict, result.StatusCode);
        Assert.Equal("Already exist.", result.Message);
    }

    [Fact]
    public async Task ShouldSuccess_UpdateCustomer()
    {
        var entity = GenerateCustomerInstance();
        var command = GenerateUpdateCustomerCommandInstance(entity.Id);

        A.CallTo(() => _customerReadRepository.FindByIdAsync(command.Id)).Returns(entity);
        A.CallTo(() => _customerReadRepository.IsExistsAsync(A<Expression<Func<Customer, bool>>>.Ignored)).Returns(false);

        var result = await _customerCommandService.UpdateAsync(command);

        Assert.IsType<ObjectBaseResponse<CustomerDto>>(result);
        Assert.Equal(command.Id, result.Data.Id);
    }

    [Fact]
    public async Task ShouldFail_UpdateCustomer_IfCustomerNotExist()
    {
        var entity = GenerateCustomerInstance();
        var command = GenerateUpdateCustomerCommandInstance(entity.Id);

        A.CallTo(() => _customerReadRepository.FindByIdAsync(command.Id)).Returns(Task.FromResult<Customer>(null));

        var result = await _customerCommandService.UpdateAsync(command);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        Assert.Equal("Customer dont exist.", result.Message);
    }

    [Fact]
    public async Task ShouldSuccess_DeleteCustomer()
    {
        var command = new DeleteCustomerCommand(Guid.NewGuid());
        var entity = GenerateCustomerInstance();

        A.CallTo(() => _customerReadRepository.FindByIdAsync(command.Id)).Returns(entity);
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).Returns(1);

        var result = await _customerCommandService.DeleteAsync(command);

        Assert.Equal(System.Net.HttpStatusCode.NoContent, result.StatusCode);
        Assert.Equal("Delete Successfully", result.Message);
    }

    [Fact]
    public async Task ShouldFail_DeleteCustomer_IfCustomerNotExist()
    {
        var command = new DeleteCustomerCommand(Guid.NewGuid());

        A.CallTo(() => _customerReadRepository.FindByIdAsync(command.Id)).Returns(Task.FromResult<Customer>(null));

        var result = await _customerCommandService.DeleteAsync(command);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        Assert.Equal("Customer dont exist.", result.Message);
    }

    private Customer GenerateCustomerInstance()
    {
        return new Customer("John", "Doe", "Address", "12345");
    }

    private static CreateCustomerCommand GenerateCreateCustomerCommandInstance()
    {
        return new CreateCustomerCommand("John", "Doe", "Address", "12345");
    }

    private static UpdateCustomerCommand GenerateUpdateCustomerCommandInstance(Guid customerId)
    {
        return new UpdateCustomerCommand(customerId, "John", "Doe", "Address", "12345");
    }
}
