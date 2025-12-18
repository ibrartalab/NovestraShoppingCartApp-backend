using NShoppingCart.Core.Entities;

namespace NShoppingCart.Core.Interfaces.Services;

public interface IOrderService
{
    Task<Order> CheckoutAsync(Guid userId, string shippingAddress, string? notes);
    Task<IEnumerable<Order>> GetUserOrderHistoryAsync(Guid userId);
}