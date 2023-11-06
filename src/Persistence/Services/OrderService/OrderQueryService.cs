using Application.Abstarctions;
using Application.Abstarctions.Repositories.CustomerRepositories;
using Application.Abstarctions.Repositories.OrderRepositories;
using Application.Features.Orders.Queries;
using Application.Features.Orders.Queries.Get;
using Application.Features.Orders.Services;
using Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services.OrderService
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerReadRepository _customerReadRepository;


        public OrderQueryService(
           IOrderReadRepository orderReadRepository,
           IUnitOfWork unitOfWork,
            ICustomerReadRepository customerReadRepository)
        {
            _orderReadRepository = orderReadRepository;
            _unitOfWork = unitOfWork;
            _customerReadRepository = customerReadRepository;
        }

        public async Task<(List<OrderResponseDto> datalist, int totalCount)> GetAll(GetOrderQuery command)
        {
            IQueryable<Order> queryable;

            if (command.CustomerId != Guid.Empty)
            {
                queryable = _orderReadRepository.FindAllByCondition(x => x.CustomerId == command.CustomerId);
            }
            else
            {
                queryable = _orderReadRepository.FindAllAsQueryable();
            }

            #region Filter

            if (command.StartDate.HasValue)
            {
                queryable = queryable.Where(x => x.CreatedDate >= command.StartDate);
            }
            if (command.EndDate.HasValue)
            {
                queryable = queryable.Where(x => x.CreatedDate <= command.EndDate);
            }

            #endregion

            var totalCount = await queryable.CountAsync();

            queryable = queryable.OrderByDescending(x => x.CreatedDate);

            OrderResponseDto data = new OrderResponseDto();

            var response = new List<OrderResponseDto>
            {
                new OrderResponseDto
                {
                    CustomerId = command.CustomerId,
                    Orders = queryable.Include(x => x.Items).ToList()
                }

            };

            return (response, totalCount);
        }
    }
}
