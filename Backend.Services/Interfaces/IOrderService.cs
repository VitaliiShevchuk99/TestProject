using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Dto;

namespace Backend.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<bool> CreateOrderAsync(OrderDto orderDto);
        public Task<bool> ChangeOrderAsync(OrderDto orderDto);
        public Task<bool> DeleteOrderAsync(int orderId);
        public Task<IEnumerable<OrderDto>> GetAllOrders();
        public IEnumerable<ProductDto> GetAllProducts();
    }
}