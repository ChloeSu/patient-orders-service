using PatientOrdersService.Models;
using PatientOrdersService.Data;
using PatientOrdersService.Repositories.Interfaces;
using Dapper;

namespace PatientOrdersService.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory) { }

        public async Task<IEnumerable<Order>> GetOrdersByPatientIdAsync(int patientId)
        {
            return await GetByKeyAsync(new { PatientId = patientId });
        }
    }
}
