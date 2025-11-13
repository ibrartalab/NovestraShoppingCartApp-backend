using System;

namespace NShoppingCart.Application.Services.Interfaces;

public interface ICartService
{
    Task ClearCart(int userId);

    Task<Cart> GetCartByUserId(int userId);

    Task<Cart> AddItemToCart(int userId, int productId, int quantity);
    Task<Cart> UpdateCartItem(int userId, int productId, int quantity);
    Task<Cart> RemoveItemFromCart(int userId, int productId);
    
}
