using NShoppingCart.Core.Entities;

namespace NShoppingCart
{
    public interface ICartRepository
    {
        Task<CartItem?> GetCartItemByIdAsync(int id);
        Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId);
        Task<CartItem> CreateCartItemAsync(CartItem cartItem);
        Task<bool> DeleteCartItemAsync(int id);
        Task<bool> UpdateCartItemAsync(CartItem cartItem);
        Task<bool> ClearCartAsync(int cartId);
        Task<bool> AddItemToCartAsync(int cartId, CartItem cartItem);
        Task<Cart?> GetCartByUserIdAsync(int userId);
        Task<Cart> CreateCartAsync(Cart cart);
        Task<bool> UpdateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(int cartId);
    }
}