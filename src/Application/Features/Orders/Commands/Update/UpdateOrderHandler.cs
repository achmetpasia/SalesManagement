using Application.Exceptions;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using Domain.Entites.Core;
using Domain.Entites.Orders;
using MediatR;

namespace Application.Features.Orders.Commands.Update
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, ObjectBaseResponse<UpdateOrderResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderHandler(IMapper mapper, IItemRepository itemRepository, IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        public async Task<ObjectBaseResponse<UpdateOrderResponse>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var existingItem = await _itemRepository.FindByIdAsync(request.Id);
            if (existingItem == null) throw new NotFoundException("This item dont exist");

            var order = existingItem.Order;

            var totalPrice = order.TotalPrice;

            var oldItemPrice = existingItem.ItemPrice;

            var orderDate = DateTime.UtcNow;

            decimal newTotalPrice = 0;

            if (request.Quantity > 0)
            {
                var productPrice = existingItem.ItemPrice / existingItem.Quantity;

                existingItem.SetQuantity(request.Quantity);

                var newItemPrice = productPrice * request.Quantity;

                existingItem.SetItemPrice(newItemPrice);

                newTotalPrice = totalPrice - oldItemPrice + newItemPrice;

                _itemRepository.Update(existingItem, orderDate);
            }
            else
            {
                newTotalPrice = totalPrice - oldItemPrice;

                _itemRepository.Delete(existingItem);
            }

            order.SetTotalPrice(newTotalPrice);

            order.SetOrderDate(orderDate);

            _orderRepository.Update(order, orderDate);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<ObjectBaseResponse<UpdateOrderResponse>>(order);

            return response;
        }
    }
}
