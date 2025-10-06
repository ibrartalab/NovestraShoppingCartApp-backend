using NShoppingCart.Core.Entities;

namespace NShoppingCart
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> GetOrderItemByIdAsync(int id);

        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);

        Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem);
    }
}