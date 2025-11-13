using System;
using NShoppingCart.Core.Entities;

namespace NShoppingCart.Application.Services.Interfaces;

public interface IOrderService
{
    Task<Order> GetOrderByIdAsync(Guid id);

    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);

    Task<IEnumerable<Order>> GetOrdersByOrderStatusAsync(string status);

    Task<Order> CreateOrderAsync(Order orderDto);

    Task<string> UpdateOrderStatus(Guid orderId, string newStatus);

    Task<bool> DeleteOrderById(Guid id);
}
