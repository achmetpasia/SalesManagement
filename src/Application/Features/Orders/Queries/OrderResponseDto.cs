using Domain.Entites.Orders;

namespace Application.Features.Orders.Queries
{
    public class OrderResponseDto 
    {
        public List<Order> Orders { get; set; }
    }
}
