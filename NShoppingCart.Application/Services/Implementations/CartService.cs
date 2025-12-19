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
        public async Task<Cart> GetCartAsync(Guid userId)
        {
            return await _cartRepository.GetOrCreateCartByUserIdAsync(userId);
        }

        public async Task AddItemToCartAsync(Guid userId, Guid productId, int quantity)
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
            var cartGuid = GetGuidFromInt(cart.Id);
            var existingItem = await _cartRepository.GetCartItemAsync(cartGuid, productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _cartRepository.UpdateCartItemAsync(existingItem);
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cartGuid,
                    ProductId = productId,
                    Quantity = quantity
                };
                await _cartRepository.AddItemToCartAsync(cartGuid, newItem);
            }
        }

        public async Task RemoveItemFromCartAsync(Guid userId, Guid productId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null) return;

            var cartGuid = GetGuidFromInt(cart.Id);
            var item = await _cartRepository.GetCartItemAsync(cartGuid, productId);

            if (item != null)
            {
                // Assuming DeleteCartItemAsync takes the CartItem's unique ID
                // If CartItem.Id is int, we convert it to Guid for the repo call
                await _cartRepository.DeleteCartItemAsync(GetGuidFromInt(item.Id));
            }
        }

        public async Task ClearCartAsync(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart != null)
            {
                await _cartRepository.ClearCartAsync(GetGuidFromInt(cart.Id));
            }
        }

        // Helper method to resolve the int-to-Guid mismatch temporarily
        private Guid GetGuidFromInt(int id)
        {
            // Warning: This is a workaround. 
            // In a real app, Primary Keys should be consistent (All Int or All Guid).
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(id).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

    }
}