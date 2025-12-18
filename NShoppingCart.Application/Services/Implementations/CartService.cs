<<<<<<< HEAD
using NShoppingCart;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Core.Interfaces.Services;
=======
using System;
using NShoppingCart.Application.Services.Interfaces;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;

namespace NShoppingCart.Application.Services.Implementations;
>>>>>>> 947f1ba63b774114116ecd2dee7c55a89a3b20bb

public class CartService(ICartRepository cartRepository) : ICartService
{
<<<<<<< HEAD
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }

    public async Task<NShoppingCart.Cart> GetCartAsync(Guid userId) => 
        await _cartRepository.GetOrCreateCartByUserIdAsync(userId);

    public async Task AddItemToCartAsync(Guid userId, Guid productId, int quantity)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product == null || product.Stock < quantity)
            throw new Exception("Product unavailable or insufficient stock.");

        var cart = await _cartRepository.GetOrCreateCartByUserIdAsync(userId);
        var existingItem = await _cartRepository.GetCartItemAsync(new Guid(cart.Id.ToString()), productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
            await _cartRepository.UpdateCartItemAsync(existingItem);
        }
        else
        {
            await _cartRepository.AddItemToCartAsync(new Guid(cart.Id.ToString()), new CartItem 
            { 
                ProductId = productId, 
                Quantity = quantity 
            });
        }
    }

    public async Task RemoveItemFromCartAsync(Guid userId, Guid productId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) return;

        var item = await _cartRepository.GetCartItemAsync(new Guid(cart.Id.ToString()), productId);
        if (item != null) await _cartRepository.DeleteCartItemAsync(new Guid(item.Id.ToString()));
    }

    public async Task ClearCartAsync(Guid userId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart != null) await _cartRepository.ClearCartAsync(new Guid(cart.Id.ToString()));
    }
}
=======
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
>>>>>>> 947f1ba63b774114116ecd2dee7c55a89a3b20bb
