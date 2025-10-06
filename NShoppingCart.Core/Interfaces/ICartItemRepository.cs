using NShoppingCart.Core.Entities;

namespace NShoppingCart
{
    public interface ICartItemRepository
    {
        Task<CartItem> GetCartItemByIdAsync(int id);

        Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId);

        Task<CartItem> CreateCartItemAsync(CartItem cartItem);

        Task<bool> DeleteCartItemAsync(int id);
    }
}