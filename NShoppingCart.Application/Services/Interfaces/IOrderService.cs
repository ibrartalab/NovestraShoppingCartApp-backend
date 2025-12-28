using NShoppingCart.Core.Entities;

namespace NShoppingCart.Core.Interfaces.Services;

public interface IOrderService
{
    Task<Order> PlaceOrderAsync(int userId, string shippingAddress);
    Task<Order> GetOrderDetailsAsync(int orderId);
    Task<IEnumerable<Order>> GetUserOrderHistoryAsync(int userId);
    Task<bool> CancelOrderAsync(int orderId);
}