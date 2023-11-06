using Application.Abstarctions.Repositories.BaseRepository;
using Domain.Entites.Customers;
using Domain.Entites.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstarctions.Repositories.ItemRepositories
{
    public interface IItemWriteRepository : IWriteRepository<Item>
    {
    }
}
