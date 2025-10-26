using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PatientOrdersService.Services;
using PatientOrdersService.Models;
using PatientOrdersService.Repositories.Interfaces;
using PatientOrdersService.Mappings;
using Microsoft.Extensions.Logging.Abstractions;
using PatientOrdersService.Common.Dtos;

namespace PatientOrdersService.Tests;

public class OrderServiceTests
{
    private readonly IMapper _mapper;

    public OrderServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }, NullLoggerFactory.Instance);

        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task GetOrdersByPatientId_Success()
    {
        int patientId = 1;
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        mockRepo.Setup(r => r.GetOrdersByPatientIdAsync(patientId))
                .ReturnsAsync(new List<Order>
                {
                    new Order { Id = 1, PatientId = 1, Message = "測試" }
                });

        var service = new OrderService(mockRepo.Object, _mapper, Mock.Of<ILogger<OrderService>>());

        // Act
        var result = await service.GetOrdersByPatientIdAsync(patientId);

        // Assert
        Assert.Single(result);
        Assert.Equal(patientId, result.First().Id);
        Assert.Equal("測試", result.First().Message);
    }

    [Fact]
    public async Task GetOrdersByPatientId_NoData()
    {
        int patientId = 1;
        
        // Arrange
        var mockRepo = new Mock<IOrderRepository>();
        mockRepo.Setup(r => r.GetOrdersByPatientIdAsync(patientId))
                .ReturnsAsync(new List<Order>());

        var service = new OrderService(mockRepo.Object, _mapper, Mock.Of<ILogger<OrderService>>());

        // Act
        var result = await service.GetOrdersByPatientIdAsync(patientId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task UpdateOrder_Success()
    {
        int orderId = 1;
        int patientId = 1;

        var existingOrder = new List<Order> { new Order { Id = orderId, PatientId = patientId, Message = "原本訊息" } };

        var mockRepo = new Mock<IOrderRepository>();
        mockRepo.Setup(r => r.GetByKeyAsync(It.Is<object>(k => k != null)))
                .ReturnsAsync(existingOrder);



        var service = new OrderService(mockRepo.Object, _mapper, Mock.Of<ILogger<OrderService>>());

        await service.UpdateOrderAsync(new UpdateOrderReq
        {
            Id = orderId,
            PatientId = patientId,
            Message = "更新訊息"
        });

        mockRepo.Verify(r => r.UpdateAsync(It.Is<Order>(o => o.Message == "更新訊息")), Times.Once);
    }
    
    [Fact]
    public async Task UpdateOrder_InvalidPatientId()
    {
        int orderId = 1;
        int correctPatientId = 1;
        int wrongPatientId = 2;

        var existingOrder = new List<Order> { new Order { Id = orderId, PatientId = correctPatientId, Message = "原本訊息" } };

        var mockRepo = new Mock<IOrderRepository>();
        mockRepo.Setup(r => r.GetByKeyAsync(It.Is<object>(k => k != null)))
                .ReturnsAsync(existingOrder);


        var service = new OrderService(mockRepo.Object, _mapper, Mock.Of<ILogger<OrderService>>());

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await service.UpdateOrderAsync(new UpdateOrderReq
            { 
                Id = orderId, 
                PatientId = wrongPatientId, 
                Message = "更新訊息"
            });
        });

        Assert.Contains("病患資料不一致", ex.Message);
    }

}
