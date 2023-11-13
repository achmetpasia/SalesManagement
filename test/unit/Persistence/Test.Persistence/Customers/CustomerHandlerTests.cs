using Application.Exceptions;
using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Update;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Core;
using Domain.Entites.Customers;
using FakeItEasy;
using System.Linq.Expressions;

namespace Test.Persistence.Customers;

public class CustomerHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerHandlerTests()
    {
        _customerRepository = A.Fake<ICustomerRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _mapper = A.Fake<Mapper>();
    }

    [Fact]
    public async Task ShouldSuccess_CreateCustomer()
    {
        var handler = new CreateCustomerHandler(_mapper, _customerRepository, _unitOfWork);

        var command = GenerateCreateCustomerCommandInstance();

        A.CallTo(() => _customerRepository.IsExistAsync(A<Expression<Func<Customer, bool>>>.Ignored)).Returns(false);

        var result = await handler.Handle(command, CancellationToken.None);

        
        A.CallTo(() => _customerRepository.CreateAsync(A<Customer>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        Assert.NotNull(result);
        Assert.IsType<ObjectBaseResponse<CreateCustomerResponse>>(result);

        // Add more assertions based on your actual implementation
    }

    [Fact]
    public async Task UpdateCustomerHandler_ShouldUpdateCustomer_WhenCustomerExists()
    {
        // Arrange
        var mapper = A.Fake<IMapper>();
        var customerRepository = A.Fake<ICustomerRepository>();
        var unitOfWork = A.Fake<IUnitOfWork>();

        var handler = new UpdateCustomerHandler(mapper, customerRepository, unitOfWork);

        var existingCustomer = new Customer("John", "Doe", "123 Main St", "12345");
        var command = new UpdateCustomerCommand(existingCustomer.Id, "Jane", "Doe", "456 Side St", "67890");

        A.CallTo(() => customerRepository.FindByIdAsync(command.Id)).Returns(existingCustomer);
        A.CallTo(() => customerRepository.IsExistAsync(A<Expression<Func<Customer, bool>>>.Ignored)).Returns(false);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        A.CallTo(() => customerRepository.Update(A<Customer>.Ignored, A<DateTime>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        Assert.NotNull(result);
        Assert.IsType<ObjectBaseResponse<UpdateCustomerResponse>>(result);

        // Add more assertions based on your actual implementation
    }

    [Fact]
    public async Task UpdateCustomerHandler_ShouldFail_WhenCustomerDoesNotExist()
    {
        // Arrange
        var mapper = A.Fake<IMapper>();
        var customerRepository = A.Fake<ICustomerRepository>();
        var unitOfWork = A.Fake<IUnitOfWork>();

        var handler = new UpdateCustomerHandler(mapper, customerRepository, unitOfWork);

        var command = new UpdateCustomerCommand(Guid.NewGuid(), "Jane", "Doe", "456 Side St", "67890");

        A.CallTo(() => customerRepository.FindByIdAsync(command.Id)).Returns(Task.FromResult<Customer>(null));

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateCustomerHandler_ShouldFail_WhenCustomerWithSameNameAlreadyExists()
    {
        // Arrange
        var mapper = A.Fake<IMapper>();
        var customerRepository = A.Fake<ICustomerRepository>();
        var unitOfWork = A.Fake<IUnitOfWork>();

        var handler = new UpdateCustomerHandler(mapper, customerRepository, unitOfWork);

        var existingCustomer = new Customer("Jane", "Doe", "456 Side St", "67890");
        var command = new UpdateCustomerCommand(existingCustomer.Id, "John", "Doe", "789 Another St", "56789");

        A.CallTo(() => customerRepository.FindByIdAsync(command.Id)).Returns(existingCustomer);
        A.CallTo(() => customerRepository.IsExistAsync(A<Expression<Func<Customer, bool>>>.Ignored)).Returns(true);

        // Act & Assert
        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteCustomerHandler_ShouldDeleteCustomer_WhenCustomerExists()
    {
        // Arrange
        var customerRepository = A.Fake<ICustomerRepository>();
        var unitOfWork = A.Fake<IUnitOfWork>();

        var handler = new DeleteCustomerHandler(customerRepository, unitOfWork);

        var existingCustomer = new Customer("John", "Doe", "123 Main St", "12345");
        var command = new DeleteCustomerCommand(existingCustomer.Id);

        A.CallTo(() => customerRepository.FindByIdAsync(command.Id)).Returns(existingCustomer);
        A.CallTo(() => unitOfWork.SaveChangesAsync()).Returns(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        A.CallTo(() => customerRepository.Delete(A<Customer>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => unitOfWork.SaveChangesAsync()).MustHaveHappenedOnceExactly();

        Assert.NotNull(result);
        Assert.Equal(System.Net.HttpStatusCode.NoContent, result.StatusCode);
        Assert.Equal("Delete Successfully", result.Message);

        // Add more assertions based on your actual implementation
    }

    [Fact]
    public async Task DeleteCustomerHandler_ShouldFail_WhenCustomerDoesNotExist()
    {
        // Arrange
        var customerRepository = A.Fake<ICustomerRepository>();
        var unitOfWork = A.Fake<IUnitOfWork>();

        var handler = new DeleteCustomerHandler(customerRepository, unitOfWork);

        var command = new DeleteCustomerCommand(Guid.NewGuid());

        A.CallTo(() => customerRepository.FindByIdAsync(command.Id)).Returns(Task.FromResult<Customer>(null));

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    private static CreateCustomerCommand GenerateCreateCustomerCommandInstance()
    {
        return new CreateCustomerCommand("John", "Doe", "Address", "12345");
    }
}
