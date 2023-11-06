using Application.Abstarctions.Repositories.BaseRepository;
using Domain.Entites.Customers;

namespace Application.Abstarctions.Repositories.CustomerRepositories;

public interface ICustomerWriteRepository : IWriteRepository<Customer>
{
}
