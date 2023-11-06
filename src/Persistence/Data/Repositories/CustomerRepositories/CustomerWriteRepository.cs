using Application.Abstarctions.Repositories.CustomerRepositories;
using Domain.Entites.Customers;
using Persistence.Data.Contexts;
using Persistence.Data.Repositories.BaseRepository;

namespace Persistence.Data.Repositories.CustomerRepositories;

public class CustomerWriteRepository : WriteRepository<Customer>, ICustomerWriteRepository
{
    public CustomerWriteRepository(Context context) : base(context)
    {
    }
}
