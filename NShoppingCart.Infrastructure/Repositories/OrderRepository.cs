using System;
using Microsoft.EntityFrameworkCore;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Infrastructure.Data;

namespace NShoppingCart.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly NShoppingCartDbContext _dbContext;

    public OrderRepository(NShoppingCartDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        return await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }
    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
    {
        return await _dbContext.Orders.Where(o => o.UserId == userId).ToListAsync();
    }
    public async Task<IEnumerable<Order>> GetOrdersByOrderStatusAsync(string status)
    {
        return await _dbContext.Orders.Where(o => o.Status == status).ToListAsync();
    }
    public async Task<Order> CreateOrderAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);

        await _dbContext.SaveChangesAsync();

        return order;
    }
    public async Task<string> UpdateOrderStatus(Guid orderId, string newStatus)
    {
        var exOrder = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

        if (exOrder == null)
        {
            return "No order found!";
        }

        exOrder.Status = newStatus;

        await _dbContext.SaveChangesAsync();

        return "Your order status has been updated successfully!";
    }
    public async Task<bool> DeleteOrderById(Guid id)
    {
        var exOrder = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

        if (exOrder is not null)
        {
            _dbContext.Remove(exOrder);

            int ch = await _dbContext.SaveChangesAsync();

            return ch > 0;
        }

        return false;
    }

}
