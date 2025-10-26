using PatientOrdersService.Common.Dtos;

namespace PatientOrdersService.Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
    }
}
