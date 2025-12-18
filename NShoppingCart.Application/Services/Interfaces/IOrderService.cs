namespace NShoppingCart.Core.Interfaces.Services;

public interface IOrderService
{
    Task<Order> PlaceOrderAsync(Guid userId, string shippingAddress, string? notes);
    Task<Order> GetOrderDetailsAsync(Guid orderId);
    Task<IEnumerable<Order>> GetUserOrderHistoryAsync(Guid userId);
    Task<bool> CancelOrderAsync(Guid orderId);
}