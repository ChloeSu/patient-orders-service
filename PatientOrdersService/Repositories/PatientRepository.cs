using PatientOrdersService.Models;
using PatientOrdersService.Data;
using PatientOrdersService.Repositories.Interfaces;

namespace PatientOrdersService.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(IDbConnectionFactory connectionFactory) 
            : base(connectionFactory) { }
    }
}
