using PatientOrdersService.Models;

namespace PatientOrdersService.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByPatientIdAsync(int patientId);
    }
}
