using System;
using NShoppingCart.Application.Services.Interfaces;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;

namespace NShoppingCart.Application.Services.Implementations;

public class CartService(ICartRepository cartRepository) : ICartService
{
    public async Task ClearCart(Guid userId)
    {
        var cart = await cartRepository.GetCartByUserIdAsync(userId);

        if (cart != null)
        {
            await cartRepository.ClearCartAsync(cart.Id);
        }
    }

    public async Task<Cart> GetCartByUserId(Guid userId)
    {
        var cart = await cartRepository.GetOrCreateCartByUserIdAsync(userId);

        return cart;
    }

    public async Task<Cart> AddItemToCart(Guid userId, Guid productId, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }
        var cart = await cartRepository.GetOrCreateCartByUserIdAsync(userId);
        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var newItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = productId,
                Quantity = quantity
            };
            cart.CartItems.Add(newItem);
        }
        await cartRepository.UpdateCartAsync(cart);
        return cart;
    }
    public async Task<Cart> UpdateCartItem(Guid userId, Guid productId, int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative.");
        }
        var cart = await cartRepository.GetOrCreateCartByUserIdAsync(userId);
        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (existingItem == null)
        {
            throw new InvalidOperationException("Item not found in cart.");
        }
        if (quantity == 0)
        {
            cart.CartItems.Remove(existingItem);
        }
        else
        {
            existingItem.Quantity = quantity;
        }
        await cartRepository.UpdateCartAsync(cart);
        return cart;
    }
    public async Task<Cart> RemoveItemFromCart(Guid userId, Guid productId)
    {
        var cart = await cartRepository.GetOrCreateCartByUserIdAsync(userId);
        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (existingItem == null)
        {
            throw new InvalidOperationException("Item not found in cart.");
        }
        cart.CartItems.Remove(existingItem);
        await cartRepository.UpdateCartAsync(cart);
        return cart;
    }
}
