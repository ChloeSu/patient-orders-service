using PatientOrdersService.Common.Dtos;

namespace PatientOrdersService.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersByPatientIdAsync(int patientId);
        Task<int> CreateOrderAsync(CreateOrderReq order);
        Task<int> UpdateOrderAsync(UpdateOrderReq order);
    }
}
