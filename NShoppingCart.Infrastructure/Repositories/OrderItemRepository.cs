using Microsoft.EntityFrameworkCore;
using NShoppingCart.Core.Entities;
using NShoppingCart.Infrastructure.Data;

namespace NShoppingCart
{
    public class OrderitemRepository : IOrderItemRepository
    {
        private readonly NShoppingCartDbContext _dbContex;

        public OrderitemRepository(NShoppingCartDbContext dbContext)
        {
            _dbContex = dbContext;
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await _dbContex.OrderItems.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await _dbContex.OrderItems.Where(o => o.OrderId == orderId).ToListAsync();
        }

        public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem)
        {
            await _dbContex.OrderItems.AddAsync(orderItem);

            await _dbContex.SaveChangesAsync();

            return orderItem;
        }
    }
}