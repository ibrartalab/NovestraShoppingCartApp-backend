using System;

namespace NShoppingCart.Application.Services.Interfaces;

public interface ICartService
{
    Task ClearCart(int userId);

    Task<CartDto> GetCartByUserId(int userId);

    Task<CartDto> AddItemToCart(int userId, int productId, int quantity);
}
