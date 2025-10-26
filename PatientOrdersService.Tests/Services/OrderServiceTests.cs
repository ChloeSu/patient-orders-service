using Xunit;
using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PatientOrdersService.Services;
using PatientOrdersService.Models;
using PatientOrdersService.Repositories.Interfaces;
using PatientOrdersService.Mappings;
using Microsoft.Extensions.Logging.Abstractions;

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
}
