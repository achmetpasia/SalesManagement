using Domain.Entites.Orders;

namespace Application.Features.Orders.Queries
{
    public class OrderResponseDto 
    {
        public Guid CustomerId { get; set; }
        public List<Order> Orders { get; set; }
    }
}
