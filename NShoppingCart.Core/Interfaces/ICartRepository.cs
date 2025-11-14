using NShoppingCart.Core.Entities;

namespace NShoppingCart
{
    public interface ICartRepository
    {
        // Cart (container) operations
        Task<Cart?> GetCartByUserIdAsync(Guid userId);
        Task<Cart> GetOrCreateCartByUserIdAsync(Guid userId);
        Task<Cart> CreateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(Guid cartId);
        Task<bool> ClearCartAsync(Guid cartId);
        Task<Cart>UpdateCartAsync(Cart cart);

        // CartItem operations
        Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(Guid cartId);
        Task<CartItem?> GetCartItemByIdAsync(Guid cartItemId);
        Task<CartItem?> GetCartItemAsync(Guid cartId, Guid productId); // helpful for checking duplicates
        Task<CartItem> AddItemToCartAsync(Guid cartId, CartItem cartItem);
        Task<bool> UpdateCartItemAsync(CartItem cartItem);
        Task<bool> DeleteCartItemAsync(Guid cartItemId);
    }
}