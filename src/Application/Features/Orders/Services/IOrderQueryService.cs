using Application.Features.Orders.Queries;
using Application.Features.Orders.Queries.Get;

namespace Application.Features.Orders.Services
{
    public interface IOrderQueryService
    {
        Task<(List<OrderResponseDto> datalist, int totalCount)> GetAll(GetOrderQuery command);
    }
}
