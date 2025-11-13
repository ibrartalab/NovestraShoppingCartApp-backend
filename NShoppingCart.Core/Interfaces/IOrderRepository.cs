using System;
using NShoppingCart.Core.Entities;

namespace NShoppingCart.Core.Interfaces;

public interface IOrderRepository
{
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
    Task<IEnumerable<Order>> GetOrdersByOrderStatusAsync(string status);
    Task<Order> CreateOrderAsync(Order order);
    Task<string> UpdateOrderStatus(Guid orderId, string newStatus);
    Task<bool> DeleteOrderById(Guid id);
}
