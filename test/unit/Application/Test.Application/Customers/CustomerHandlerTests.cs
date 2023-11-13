using Application.Exceptions;
using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Update;
using Application.Features.Customers.Profiles;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Core;
using Domain.Entites.Customers;
using FakeItEasy;
using System.Linq.Expressions;
using System.Net;

namespace Test.Application.Customers;

public class CustomerHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerHandlerTests()
    {
        _customerRepository = A.Fake<ICustomerRepository>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        var myProfile = new CustomerMappingProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        _mapper = new Mapper(configuration);
    }

    [Fact]
    public async Task ShouldSuccess_CreateCustomer()
    {

        var command = GenerateCreateCustomerCommandInstance();

        A.CallTo(() => _customerRepository.IsExistAsync(A<Expression<Func<Customer, bool>>>.Ignored)).Returns(false);

        var handler = new CreateCustomerHandler(_mapper, _customerRepository, _unitOfWork);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<ObjectBaseResponse<CreateCustomerResponse>>(result);
    }

    [Fact]
    public async Task ShouldFail_CreateCustomer_WhenCustomerAlreadyExists()
    {
        var command = GenerateCreateCustomerCommandInstance();

        A.CallTo(() => _customerRepository.IsExistAsync(A<Expression<Func<Customer, bool>>>.Ignored)).Returns(true);

        var handler = new CreateCustomerHandler(_mapper, _customerRepository, _unitOfWork);

        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, CancellationToken.None));
    }


    [Fact]
    public async Task ShouldSuccess_UpdateCustomer()
    {
        var command = GenerateUpdateCustomerCommandInstance();
        var entity = GenerateAggregateInstance();

        A.CallTo(() => _customerRepository.FindByIdAsync(command.Id)).Returns(entity);
        A.CallTo(() => _customerRepository.IsExistAsync(A<Expression<Func<Customer, bool>>>.Ignored)).Returns(false);

        var handler = new UpdateCustomerHandler(_mapper, _customerRepository, _unitOfWork);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<ObjectBaseResponse<UpdateCustomerResponse>>(result);
    }

    [Fact]
    public async Task ShouldFail_UpdateCustomer_WhenCustomerDoesNotExist()
    {
        var command = GenerateUpdateCustomerCommandInstance();

        A.CallTo(() => _customerRepository.FindByIdAsync(command.Id)).Returns(Task.FromResult<Customer>(null));

        var handler = new UpdateCustomerHandler(_mapper, _customerRepository, _unitOfWork);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ShouldFail_UpdateCustomer_WhenCustomerAlreadyExists()
    {
        var command = GenerateUpdateCustomerCommandInstance();
        var existingCustomer = GenerateAggregateInstance();

        A.CallTo(() => _customerRepository.FindByIdAsync(command.Id)).Returns(existingCustomer);
        A.CallTo(() => _customerRepository.IsExistAsync(A<Expression<Func<Customer, bool>>>.Ignored)).Returns(true);

        var handler = new UpdateCustomerHandler(_mapper, _customerRepository, _unitOfWork);

        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task ShouldSuccess_DeleteCustomer()
    {
        var command = GenerateDeleteCustomerCommandInstance();
        var existingCustomer = GenerateAggregateInstance();

        A.CallTo(() => _customerRepository.FindByIdAsync(command.Id)).Returns(existingCustomer);

        var handler = new DeleteCustomerHandler(_customerRepository, _unitOfWork);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        Assert.Equal("Delete Successfully", result.Message);
    }

    [Fact]
    public async Task ShouldFail_DeleteCustomer_WhenCustomerDoesNotExist()
    {
        var command = GenerateDeleteCustomerCommandInstance();

        A.CallTo(() => _customerRepository.FindByIdAsync(command.Id)).Returns(Task.FromResult<Customer>(null));

        var handler = new DeleteCustomerHandler(_customerRepository, _unitOfWork);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
    }

    private Customer GenerateAggregateInstance()
    {
        return new Customer("John", "Doe", "Address", "12345");
    }

    private static CreateCustomerCommand GenerateCreateCustomerCommandInstance()
    {
        return new CreateCustomerCommand(Guid.NewGuid(),"John", "Doe", "Address", "12345");
    }

    private UpdateCustomerCommand GenerateUpdateCustomerCommandInstance()
    {
        return new UpdateCustomerCommand(Guid.NewGuid(), "NewFirstName", "NewLastName", "NewAddress", "54321");
    }

    private DeleteCustomerCommand GenerateDeleteCustomerCommandInstance()
    {
        return new DeleteCustomerCommand(Guid.NewGuid());
        
    }
}
