using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Shared.Dto;

namespace Backend.Services.Services
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> CreateOrderAsync(OrderDto orderDto)
        {
            return await _orderRepository.CreateOrderAsync(orderDto);
        }

        public async Task<bool> ChangeOrderAsync(OrderDto orderDto)
        {
            return await _orderRepository.ChangeOrderAsync(orderDto);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            return await _orderRepository.DeleteOrderAsync(orderId);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            return await _orderRepository.GetAllOrders();
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            return _orderRepository.GetAllProducts();
        }
    }
}
