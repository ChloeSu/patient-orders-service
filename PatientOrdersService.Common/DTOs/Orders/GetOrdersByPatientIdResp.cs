namespace PatientOrdersService.Common.Dtos
{
    public class GetOrdersByPatientIdResp
    {
        public int PatientId { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; } = Enumerable.Empty<OrderDto>();
    }
}
