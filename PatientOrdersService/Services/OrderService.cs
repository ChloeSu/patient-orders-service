using AutoMapper;
using Microsoft.Extensions.Logging;
using PatientOrdersService.Repositories.Interfaces;
using PatientOrdersService.Services.Interfaces;
using PatientOrdersService.Common.Dtos;
using PatientOrdersService.Models;

namespace PatientOrdersService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByPatientIdAsync(int patientId)
        {
            _logger.LogInformation("Fetching orders for patient {PatientId}", patientId);
            var orders = await _orderRepository.GetOrdersByPatientIdAsync(patientId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<int> CreateOrderAsync(CreateOrderReq req)
        {
            _logger.LogInformation("Creating new order for patient {PatientId}", req.PatientId);
            var order = _mapper.Map<Order>(req);
            return await _orderRepository.InsertAsync(order);
        }

        public async Task<int> UpdateOrderAsync(UpdateOrderReq req)
        {
            _logger.LogInformation("Updating order {OrderId}", req.Id);
            var order = _mapper.Map<Order>(req);
            return await _orderRepository.UpdateAsync(order);
        }
    }
}
