using System;
using NShoppingCart.Application.Services.Interfaces;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;

namespace NShoppingCart.Application.Services.Implementations;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        return await orderRepository.GetOrderByIdAsync(id);
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
    {
        return await orderRepository.GetOrdersByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Order>> GetOrdersByOrderStatusAsync(string status)
    {
        return await orderRepository.GetOrdersByOrderStatusAsync(status);
    }

    public async Task<Order> CreateOrderAsync(Order orderDto)
    {
        return await orderRepository.CreateOrderAsync(orderDto);
    }

    public async Task<string> UpdateOrderStatus(Guid orderId, string newStatus)
    {
        return await orderRepository.UpdateOrderStatus(orderId, newStatus);
    }

    public async Task<bool> DeleteOrderById(Guid id)
    {
        return await orderRepository.DeleteOrderById(id);
    }
}
