using AutoMapper;
using EShop.Application.Order;
using EShop.Core.Entities;
using Moq;
using OnlineLibraryShop.Application.Command.Handler;
using OnlineLibraryShop.Core.Interfaces;
using Xunit;

public class CreateOrderCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Order()
    {
        // Arrange
        var orderRepositoryMock = new Mock<IOrderRepository>();
        var mapperMock = new Mock<IMapper>();

        var createOrderCommand = new CreateOrderCommand();

        var expectedOrderId = 1; // You can set the expected OrderId here.

        orderRepositoryMock
            .Setup(repo => repo.CreateOrder(It.IsAny<OrderEntity>()))
            .ReturnsAsync(expectedOrderId);

        var handler = new CreateOrderCommandHandler(orderRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(createOrderCommand, CancellationToken.None);

        // Assert
        Assert.Equal(expectedOrderId, result);
        orderRepositoryMock.Verify(repo => repo.CreateOrder(It.IsAny<OrderEntity>()), Times.Once);
    }
}
