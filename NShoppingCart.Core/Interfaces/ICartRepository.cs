

using NShoppingCart.Core.Entities;

namespace NShoppingCart.Core.Interfaces;

public interface ICartRepository
{
    Task<Cart?> GetOrCreateCartByUserIdAsync(int userId);
    Task<string> AddItemToCartAsync(int userId, int productId, int quantity);
    Task<CartItem?> UpdateCartItemAsync(int userId, int productId, int quantity);
}
