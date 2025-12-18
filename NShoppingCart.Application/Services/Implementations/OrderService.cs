using NShoppingCart;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Core.Interfaces.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepo, ICartRepository cartRepo, IProductRepository prodRepo)
    {
        _orderRepository = orderRepo;
        _cartRepository = cartRepo;
        _productRepository = prodRepo;
    }

    public async Task<Order> CheckoutAsync(Guid userId, string shippingAddress, string? notes)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null || !cart.CartItems.Any()) throw new Exception("Cart is empty.");

        var order = new Order
        {
            UserId = userId,
            OrderNumber = $"ORD-{Guid.NewGuid().ToString().ToUpper().Substring(0, 8)}",
            ShippingAddress = shippingAddress,
            Notes = notes,
            TotalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity)
        };

        foreach (var item in cart.CartItems)
        {
            order.OrderItems.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.Product.Price
            });

            // Reduce Stock
            item.Product.Stock -= item.Quantity;
            await _productRepository.UpdateProductAsync(item.Product);
        }

        var createdOrder = await _orderRepository.CreateOrderAsync(order);
        await _cartRepository.ClearCartAsync(new Guid(cart.Id.ToString()));

        return createdOrder;
    }

    public async Task<IEnumerable<Order>> GetUserOrderHistoryAsync(Guid userId) => 
        await _orderRepository.GetOrdersByUserIdAsync(userId);
}