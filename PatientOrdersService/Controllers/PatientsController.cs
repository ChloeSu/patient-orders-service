using Microsoft.AspNetCore.Mvc;
using PatientOrdersService.Services.Interfaces;

namespace PatientOrdersService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var result = await _patientService.GetAllPatientsAsync();
            return Ok(result);
        }
    }
}
