using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces;
using NShoppingCart.Core.Enums;
using NShoppingCart.Core.Interfaces.Services;
using NShoppingCart;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(
        IOrderRepository orderRepo,
        ICartRepository cartRepo,
        IProductRepository prodRepo)
    {
        _orderRepository = orderRepo;
        _cartRepository = cartRepo;
        _productRepository = prodRepo;
    }

    public async Task<Order> PlaceOrderAsync(int userId, string shippingAddress, string? notes)
    {
        // 1. Retrieve the cart and its items
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null || !cart.CartItems.Any())
        {
            throw new InvalidOperationException("Cannot place an order with an empty cart.");
        }

        // 2. Map Cart to Order
        var order = new Order
        {
            UserId = userId,
            OrderNumber = GenerateOrderNumber(),
            ShippingAddress = shippingAddress,
            Notes = notes,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            TotalAmount = 0 // Will calculate below
        };

        foreach (var cartItem in cart.CartItems)
        {
            // 3. Check and Update Stock
            var product = cartItem.Product;
            if (product.Stock < cartItem.Quantity)
            {
                throw new Exception($"Insufficient stock for product: {product.Name}");
            }

            // Deduct stock
            product.Stock -= cartItem.Quantity;
            await _productRepository.UpdateProductAsync(product);

            // 4. Create OrderItem (Capture price at time of purchase)
            var orderItem = new OrderItem
            {
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                UnitPrice = product.Price
            };

            order.OrderItems.Add(orderItem);
            order.TotalAmount += (orderItem.UnitPrice * orderItem.Quantity);
        }

        // 5. Save Order and Clear Cart
        var createdOrder = await _orderRepository.CreateOrderAsync(order);

        // Note: Using the cart's unique ID for clearing
        await _cartRepository.ClearCartAsync(cart.Id);

        return createdOrder;
    }

    public async Task<Order> GetOrderDetailsAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order == null) throw new KeyNotFoundException("Order not found.");
        return order;
    }

    public async Task<IEnumerable<Order>> GetUserOrderHistoryAsync(int userId)
    {
        return await _orderRepository.GetOrdersByUserIdAsync(userId);
    }

    public async Task<bool> CancelOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        if (order == null) return false;

        if (order.Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException("Only pending orders can be cancelled.");
        }

        return await _orderRepository.DeleteOrderById(orderId);
    }

    private string GenerateOrderNumber()
    {
        return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 5).ToUpper()}";
    }
}