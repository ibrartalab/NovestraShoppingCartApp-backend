using System;
using NShoppingCart.Core.Entities;

namespace NShoppingCart.Core.Interfaces;

public interface IOrderRepository
{
    Task<Order> GetOrderByIdAsync(int id);
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
    Task<IEnumerable<Order>> GetOrdersByOrderStatusAsync(string status);
    Task<Order> CreateOrderAsync(Order order);
    Task<string> UpdateOrderStatus(int orderId, string newStatus);
    Task<bool> DeleteOrderById(int id);
}
