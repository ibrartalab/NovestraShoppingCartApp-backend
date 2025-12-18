namespace NShoppingCart.Core.Interfaces.Services;

public interface ICartService
{
<<<<<<< HEAD
    Task<Cart> GetCartAsync(Guid userId);
    Task AddItemToCartAsync(Guid userId, Guid productId, int quantity);
    Task RemoveItemFromCartAsync(Guid userId, Guid productId);
    Task ClearCartAsync(Guid userId);
}
=======
    Task ClearCart(Guid userId);
    Task<Cart> GetCartByUserId(Guid userId);
    Task<Cart> AddItemToCart(Guid userId, Guid productId, int quantity);
    Task<Cart> UpdateCartItem(Guid userId, Guid productId, int quantity);
    Task<Cart> RemoveItemFromCart(Guid userId, Guid productId);
    
}
>>>>>>> 947f1ba63b774114116ecd2dee7c55a89a3b20bb
