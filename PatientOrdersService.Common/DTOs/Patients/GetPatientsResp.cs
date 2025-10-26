namespace PatientOrdersService.Common.Dtos
{
    public class GetPatientsResp
    {
        public IEnumerable<PatientDto> Patients { get; set; } = Enumerable.Empty<PatientDto>();
    }
}
