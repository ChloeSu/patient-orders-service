using AutoMapper;
using Microsoft.Extensions.Logging;
using PatientOrdersService.Repositories.Interfaces;
using PatientOrdersService.Services.Interfaces;
using PatientOrdersService.Common.Dtos;

namespace PatientOrdersService.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientService> _logger;

        public PatientService(IPatientRepository patientRepository, IMapper mapper, ILogger<PatientService> logger)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            _logger.LogInformation("Fetching all patients...");
            var patients = await _patientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }
    }
}
