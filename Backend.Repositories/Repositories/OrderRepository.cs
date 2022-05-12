using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.Context;
using Backend.Data.Models;
using Backend.Repositories.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;

namespace Backend.Repositories.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataBaseContext _dbContext;

        public OrderRepository(DataBaseContext dataBaseContext)
        {
            _dbContext = dataBaseContext;
        }

        public async Task<bool> CreateOrderAsync(OrderDto orderDto)
        {
            if (await _dbContext.UserModels.AsNoTracking().FirstOrDefaultAsync(t => t.Id == orderDto.User.Id) == null)
                return false;
            var order = orderDto.Adapt<OrderModel>();
            await _dbContext.OrderModel.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeOrderAsync(OrderDto orderDto)
        {
            if (await _dbContext.UserModels.AsNoTracking().FirstOrDefaultAsync(t => t.Id == orderDto.User.Id) == null)
                return false;
            var order = await _dbContext.OrderModel.FirstOrDefaultAsync(t => t.Id == orderDto.Id);
            if (order == null) return false;

            order.Products = orderDto.Products.Adapt<List<ProductModel>>();
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _dbContext.OrderModel.FirstOrDefaultAsync(t => t.Id == orderId);
            if (order == null) return false;
            _dbContext.Remove(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            var orders = await _dbContext.OrderModel.Include(t => t.Products).Include(t => t.User).ToListAsync();

            var ordersResult = new List<OrderDto>();
            foreach (var order in orders)
            {
                ordersResult.Add(new OrderDto()
                {
                    Id = order.Id,
                    Products =
                        order.Products.Select(x => new ProductDto()
                            {
                                Id = x.Id, Description = x.Description, Name = x.Name, Price = x.Price
                            })
                            .ToList(),
                    User = new UserDto()
                    {
                        Email = order.User.Email,
                        Login = order.User.Login,
                        Password = order.User.Password,
                        Id = order.User.Id
                    },
                    Sum = order.Sum
                });
            }

            return ordersResult;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            return _dbContext.ProductModel.ToList().Adapt<IEnumerable<ProductDto>>();
        }
    }
}
