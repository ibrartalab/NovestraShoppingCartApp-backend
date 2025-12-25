using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Core.Interfaces.Services;

namespace NShoppingCart.Application.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        // Implement cart-related methods here
        public async Task<Cart> GetCartAsync(int userId)
        {
            return await _cartRepository.GetOrCreateCartByUserIdAsync(userId);
        }

        public async Task AddItemToCartAsync(int userId, int productId, int quantity)
        {
            // 1. Validate Product Existence and Stock
            // Note: If IProductRepository.GetProductByIdAsync expects a Guid, 
            // but Product.Id is an int, you must ensure these types match in your Repository.
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            if (product.Stock < quantity)
                throw new InvalidOperationException("Insufficient stock available.");

            // 2. Get or Create the User's Cart
            var cart = await _cartRepository.GetOrCreateCartByUserIdAsync(userId);

            // 3. Check if item already exists in cart
            // Using CartId as a Guid to match your Repository Interface
            
            var existingItem = await _cartRepository.GetCartItemAsync(cart.Id, productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _cartRepository.UpdateCartItemAsync(existingItem);
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity
                };
                await _cartRepository.AddItemToCartAsync(cart.Id, newItem);
            }
        }

        public async Task RemoveItemFromCartAsync(int userId, int productId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null) return;

            var cartGuid = cart.Id;
            var item = await _cartRepository.GetCartItemAsync(cart.Id, productId);

            if (item != null)
            {
                // Assuming DeleteCartItemAsync takes the CartItem's unique ID
                // If CartItem.Id is int, we convert it to Guid for the repo call
                await _cartRepository.DeleteCartItemAsync(item.Id);
            }
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart != null)
            {
                await _cartRepository.ClearCartAsync(cart.Id);
            }
        }

        // Helper method to resolve the int-to-Guid mismatch temporarily
        // private (int id)
        // {
        //     // Warning: This is a workaround. 
        //     // In a real app, Primary Keys should be consistent (All Int or All Guid).
        //     byte[] bytes = new byte[16];
        //     BitConverter.GetBytes(id).CopyTo(bytes, 0);
        //     return new Guid(bytes);
        // }

    }
}