using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Application.IServices;
using EShop.Application.Services;
using EShop.Core.Entities;
using MediatR;
using Moq;
using Xunit;

namespace EShop.Application.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task GetCustomerById_ReturnsCustomer_WhenCustomerExists()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();

            var customerEntity = new CustomerEntity { CustomerId = 1 };

            var customer = new Customer { CustomerId = 1 };
            var customerDto = new Customer { CustomerId = 1 };

            mediatorMock.Setup(x => x.Send(new GetCustomerByIdQuery { CustomerId = 1 } , It.IsAny<CancellationToken>()))
                .ReturnsAsync(customerEntity);

            mapperMock
                .Setup(x => x.Map<Customer>(It.IsAny<Customer>()))
                .Returns(customer);

            var service = new CustomerService(mediatorMock.Object, mapperMock.Object);

            // Act
            var response = await service.GetCustomerById(customer);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(customer, response.Data);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
            Assert.False(response.HasError);
            Assert.Equal("", response.Error);

            mediatorMock.Verify(x => x.Send(It.IsAny<GetCustomerByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(x => x.Map<Customer>(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task GetCustomerById_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<IMapper>();

            Customer customer = null;
            var customerEntity = new CustomerEntity { CustomerId = 1 };

            mediatorMock.Setup(x => x.Send(new GetCustomerByIdQuery { CustomerId = 1 }, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customerEntity);

            var service = new CustomerService(mediatorMock.Object, mapperMock.Object);

            // Act
            var response = await service.GetCustomerById(new Customer { CustomerId = 1 });

            // Assert
            Assert.NotNull(response);
            Assert.Null(response.Data);
            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
            Assert.False(response.HasError);
            Assert.Equal("", response.Error);

        }
    }
}
