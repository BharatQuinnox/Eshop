using System.Collections.Generic;
using System.Net;
using System.Threading;
using AutoMapper;
using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Application.IServices;
using EShop.Application.Services;
using EShop.Core.Entities;
using MediatR;
using Moq;
using Xunit;

public class ProductServiceTests
{
    [Fact]
    public async Task GetProductList_Returns_OK_Response_When_ProductsExist()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var mapperMock = new Mock<IMapper>();

        // Mock the behavior of IMediator to return a list of products
        var products = new List<Product>
    {
        new Product {  Name = "Product 1" },
        new Product { Name = "Product 2" }
    };

        mediatorMock.Setup(m => m.Send(It.IsAny<ProductEntity>(), CancellationToken.None))
                    .ReturnsAsync(products); // Provide the correct instance of GetProductByQuery

        // Create an instance of ProductService with the mock dependencies
        var productService = new ProductService(mediatorMock.Object, mapperMock.Object);

        // Act
        var result = await productService.GetProductList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.False(result.HasError);
    }


}
