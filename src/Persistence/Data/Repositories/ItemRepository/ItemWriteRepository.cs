using Application.Abstarctions.Repositories.ItemRepositories;
using Domain.Entites.Orders;
using Persistence.Data.Contexts;
using Persistence.Data.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Repositories.ItemRepository
{
    public class ItemWriteRepository : WriteRepository<Item>, IItemWriteRepository
    {
        public ItemWriteRepository(Context context) : base(context)
        {
        }
    }
}
