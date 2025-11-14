using System;

namespace NShoppingCart.Application.Services.Interfaces;

public interface ICartService
{
    Task ClearCart(Guid userId);
    Task<Cart> GetCartByUserId(Guid userId);
    Task<Cart> AddItemToCart(Guid userId, Guid productId, int quantity);
    Task<Cart> UpdateCartItem(Guid userId, Guid productId, int quantity);
    Task<Cart> RemoveItemFromCart(Guid userId, Guid productId);
    
}
