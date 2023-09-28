using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using EShop.Core.Interfaces;
using EShop.Core.Entities;

namespace EShop.Application.Command.Handler.Tests
{
    public class CreateCartCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsTrue()
        {
            // Arrange
            var command = new CreateCartCommand(); // Create a valid command here
            var cartRepositoryMock = new Mock<ICartRepository>();
            var mapperMock = new Mock<IMapper>();
            cartRepositoryMock.Setup(repo => repo.AddCartItem(It.IsAny<CartEntity>()))
                             .ReturnsAsync(true); // Mock repository to return true

            var handler = new CreateCartCommandHandler(cartRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Handle_InvalidCommand_ReturnsFalse()
        {
            // Arrange
            var command = new CreateCartCommand(); // Create an invalid command here
            var cartRepositoryMock = new Mock<ICartRepository>();
            var mapperMock = new Mock<IMapper>();

            var handler = new CreateCartCommandHandler(cartRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
        }

    }
}
