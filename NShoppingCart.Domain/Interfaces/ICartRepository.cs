using NShoppingCart.Core.Entities;

namespace NShoppingCart
{
    public interface ICartRepository
    {
        // Cart (container) operations
        Task<Cart?> GetCartByUserIdAsync(int userId);
        Task<Cart> GetOrCreateCartByUserIdAsync(int userId);
        Task<Cart> CreateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(int cartId);
        Task<bool> ClearCartAsync(int cartId);
        Task<Cart>UpdateCartAsync(Cart cart);

        // CartItem operations
        Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId);
        Task<CartItem?> GetCartItemByIdAsync(int cartItemId);
        Task<CartItem?> GetCartItemAsync(int cartId, int productId); // helpful for checking duplicates
        Task<CartItem> AddItemToCartAsync(int cartId, CartItem cartItem);
        Task<bool> UpdateCartItemAsync(CartItem cartItem);
        Task<bool> DeleteCartItemAsync(int cartItemId);
    }
}