using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cqrs.Domain;
using EShop.Application.Command;
using EShop.Application.Dto;
using EShop.Application.IServices;
using EShop.Application.Services;
using EShop.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class ShippingServiceTests
{
    // Mocked dependencies
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IMapper> _mapperMock;

    public ShippingServiceTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _mapperMock = new Mock<IMapper>();
    }

    [Fact]
    public async Task GetShippingData_ReturnsValidApiResponse_WhenShippingDataExists()
    {
        // Arrange
        int customerId = 1;
        var expectedResult = new List<ShippingEntity>(); // Replace with your expected data

        _mediatorMock
            .Setup(x => x.Send(It.IsAny<GetShippingAddressByIdQuery>(), CancellationToken.None))
            .ReturnsAsync(expectedResult);
        _mapperMock
            .Setup(x => x.Map<List<EShop.Application.Dto.Shipping>>(It.IsAny<List<ShippingEntity>>()))
            .Returns(new List<EShop.Application.Dto.Shipping>()); // Replace with your expected DTOs

        var shippingService = new ShippingService(_mediatorMock.Object, _mapperMock.Object);

        // Act
        var result = await shippingService.GetShippingData(customerId);

        // Assert
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
        Assert.True(result.HasError);
        Assert.NotEmpty(result.Error);
    }

    [Fact]
    public async Task GetShippingData_ReturnsNoContentApiResponse_WhenShippingDataIsEmpty()
    {
        // Arrange
        int customerId = 1;
        List<ShippingEntity> expectedResult = new List<ShippingEntity>(); // Empty data

        _mediatorMock
            .Setup(x => x.Send(It.IsAny<GetShippingAddressByIdQuery>(), CancellationToken.None))
            .ReturnsAsync(expectedResult);

        var shippingService = new ShippingService(_mediatorMock.Object, _mapperMock.Object);

        // Act
        var result = await shippingService.GetShippingData(customerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
        Assert.True(result.HasError);
        Assert.Equal("No data found", result.Error);
    }

    [Fact]
    public async Task InsertShippingAddress_ReturnsValidApiResponse()
    {
        // Arrange
        var shippingDto = new EShop.Application.Dto.Shipping(); // Replace with your DTO data
        var shippingEntity = new ShippingEntity(); // Replace with your entity data

        _mapperMock
            .Setup(x => x.Map<ShippingEntity>(shippingDto))
            .Returns(shippingEntity);

        _mediatorMock
            .Setup(x => x.Send(It.IsAny<CreateShippingCommand>(), CancellationToken.None))
            .ReturnsAsync(1); // Replace with your expected result

        var shippingService = new ShippingService(_mediatorMock.Object, _mapperMock.Object);

        // Act
        var result = await shippingService.InsertShippingAddress(shippingDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Data);
    }
}
