using System.Threading.Tasks;
using EShop.Controllers;
using EShop.Application.Dto;
using EShop.Application.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;

public class ShippingSlipControllerTests
{
    [Fact]
    public async Task Post_ValidOrder_ReturnsOkResult()
    {
        // Arrange
        var orderServiceMock = new Mock<IOrderService>();
        orderServiceMock.Setup(service => service.GenerateSlipIfRequired(It.IsAny<Order>()))
                       .ReturnsAsync(true);

        var controller = new ShippingSlipController(orderServiceMock.Object);
        var validOrder = new Order(); // Create a valid Order object to pass to the controller

        // Act
        var result = await controller.Post(validOrder);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidOrder_ReturnsInternalServerError()
    {
        // Arrange
        var orderServiceMock = new Mock<IOrderService>();
        orderServiceMock.Setup(service => service.GenerateSlipIfRequired(It.IsAny<Order>()))
                       .ReturnsAsync(false);

        var controller = new ShippingSlipController(orderServiceMock.Object);
        var invalidOrder = new Order(); // Create an invalid Order object to pass to the controller

        // Act
        var result = await controller.Post(invalidOrder);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
    }
}
