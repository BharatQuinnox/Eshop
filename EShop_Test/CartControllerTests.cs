using Xunit;
using Moq;
using EShop.Controllers;
using EShop.Application.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EShop.Application.Dto;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

public class CartControllerTests
{
    [Fact]
    public async Task Get_ReturnsOkResult_WhenValidCustomerId()
    {

        List<CartItem> cartItems = new List<CartItem>();
        var returnresult = new ApiResponse<List<CartItem>>
        {
            Data = cartItems,
            StatusCode = (int)HttpStatusCode.OK
        };

        // Arrange
        var customerId = 1;
        var mockCartService = new Mock<ICartService>();
        var mockValidator = new Mock<IValidator<Cart>>();

        // Set up the mock to return a valid result
        mockCartService.Setup(service => service.GetCartItem(customerId))
            .ReturnsAsync(returnresult);

        var controller = new CartController(mockCartService.Object, mockValidator.Object, null, null, null);

        // Act
        var result = await controller.Get(customerId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Get_ReturnsNotFoundResult_WhenInvalidCustomerId()
    {

        List<CartItem> cartItems = new List<CartItem>();
        var returnresult = new ApiResponse<List<CartItem>>
        {
            Data = cartItems,
            HasError=true
        };
        // Arrange
        var customerId = 1;
        var mockCartService = new Mock<ICartService>();
        var mockValidator = new Mock<IValidator<Cart>>();

        // Set up the mock to return an error
        mockCartService.Setup(service => service.GetCartItem(customerId))
            .ReturnsAsync(returnresult);

        var controller = new CartController(mockCartService.Object, mockValidator.Object, null, null, null);

        // Act
        var result = await controller.Get(customerId);

        result.Equals(404);
    }

    [Fact]
    public async Task Post_ReturnsCreatedResult_WhenValidCart()
    {
         var returnresult = new ApiResponse<string>
        {
            Data = "Valid",
            StatusCode = (int)HttpStatusCode.OK
        };
         var createResult = new ApiResponse<bool>
        {
            Data = true,
            StatusCode = (int)HttpStatusCode.OK
        };

        var customevalidationResponse = new ApiResponse<List<string>>
        {
            Data = new List<string>(),
            StatusCode = (int)HttpStatusCode.OK
        };

        Cart cart = new Cart();

        // Arrange
        var mockCartService = new Mock<ICartService>();
        var mockValidator = new Mock<IValidator<Cart>>();
         var mockLogger = new Mock<ILogger<CartController>>();


        var validCart = new Cart(); // Create a valid cart

        // Set up the mock to return a valid result for validation
        mockValidator.Setup(validator => validator.ValidateAsync(validCart, default))
     .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        // Set up the mock to return a valid result for cart validation
        mockCartService.Setup(service => service.ValidatCartInvalidData(validCart))
            .ReturnsAsync(customevalidationResponse);

        // Set up the mock to return a valid result for adding cart items
        mockCartService.Setup(service => service.AddCartItems(validCart))
            .ReturnsAsync(createResult); // Mocked cart ID

        var controller = new CartController(mockCartService.Object, mockValidator.Object, null, null, mockLogger.Object);

        // Act
        var result = await controller.Post(validCart);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
    }


}

