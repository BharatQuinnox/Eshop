using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Application.IServices;
using EShop.Controllers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EShop.Tests.Controllers
{
    public class ShippingControllerTests
    {
        [Fact]
        public async Task Get_Should_Return_OkResult_When_Valid_CustomerId()
        {

            List<Shipping> shippings = new List<Shipping>();
            var returnresult = new ApiResponse<List<Shipping>>
            {
                Data = shippings,
                StatusCode =(int) HttpStatusCode.OK
            };

            int customerId = 1;
            var shippingServiceMock = new Mock<IShippingService>();
            shippingServiceMock.Setup(service => service.GetShippingData(customerId))
                .ReturnsAsync(returnresult);



            var controller = new ShippingController(shippingServiceMock.Object, null);

            // Act
            var result = await controller.Get(customerId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_Should_Return_NotFoundResult_When_Invalid_CustomerId()
        {
            List<Shipping> shippings = new List<Shipping>();
            var returnresult = new ApiResponse<List<Shipping>>
            {
                Data = shippings,
                StatusCode = (int)HttpStatusCode.BadRequest,
                HasError = true
            };


            // Arrange
            int customerId = 2;
            var shippingServiceMock = new Mock<IShippingService>();
            shippingServiceMock
                .Setup(service => service.GetShippingData(customerId))
                .ReturnsAsync(returnresult);

            var controller = new ShippingController(shippingServiceMock.Object, null);

            // Act
            var result = await controller.Get(customerId);

            // Assert
            result.Equals(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_Should_Return_CreatedAtAction_When_Valid_Shipping()
        {

            List<Shipping> shippings = new List<Shipping>();
            var returnresult = new ApiResponse<int>
            {
                Data = 1,
                StatusCode = (int)HttpStatusCode.BadRequest,
                HasError = false
            };
            // Arrange
            var shipping = new Shipping { /* Initialize with valid data */ };
            var validatorMock = new Mock<IValidator<Shipping>>();
            validatorMock
                .Setup(validator => validator.ValidateAsync(shipping, default))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            var shippingServiceMock = new Mock<IShippingService>();
            shippingServiceMock
                .Setup(service => service.InsertShippingAddress(shipping))
                .ReturnsAsync(returnresult);

            var controller = new ShippingController(shippingServiceMock.Object, validatorMock.Object);

            // Act
            var result = await controller.Post(shipping);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(ShippingController.Post), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task Post_Should_Return_BadRequest_When_Invalid_Shipping()
        {
            // Arrange
            var shipping = new Shipping { /* Initialize with invalid data */ };
            var validationResult = new FluentValidation.Results.ValidationResult();
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Property", "Error message"));

            var validatorMock = new Mock<IValidator<Shipping>>();
            validatorMock
                .Setup(validator => validator.ValidateAsync(shipping, default))
                .ReturnsAsync(validationResult);

            var controller = new ShippingController(null, validatorMock.Object);

            // Act
            var result = await controller.Post(shipping);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<string>>(badRequestResult.Value);
        }
    }
}
