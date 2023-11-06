using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Orders.Queries.Get
{
    public class GetOrderQuery : IRequest<ArrayBaseResponse<OrderResponseDto>>
    {
        public Guid CustomerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}
        public int PageIndex { get; set; }
        public int PageLenght { get; set; } = 10;
    }
}
