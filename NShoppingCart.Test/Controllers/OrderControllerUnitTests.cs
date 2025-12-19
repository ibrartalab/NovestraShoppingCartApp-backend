using Microsoft.AspNetCore.Mvc;
using Moq;
using NShoppingCart.Core.Entities;
using NShoppingCart.Core.Interfaces.Services;
using NShoppingCart.Api.Controllers;

namespace NShoppingCart.Test.Controllers
{
    public class OrderControllerUnitTests
    {
        private readonly Mock<IOrderService> _orderServiceMock;
        private readonly OrderController _controller;

        public OrderControllerUnitTests()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _controller = new OrderController(_orderServiceMock.Object);
        }

        [Fact]
        public async Task Checkout_ShouldReturnOk_WithCreatedOrder()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new CheckoutRequest("123 Street", "Leave at door");
            var expectedOrder = new Order { OrderNumber = "ORD123" };

            _orderServiceMock.Setup(s => s.PlaceOrderAsync(userId, request.ShippingAddress, request.Notes))
                             .ReturnsAsync(expectedOrder);

            // Act
            var result = await _controller.Checkout(userId, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedOrder, okResult.Value);
        }
    }
}