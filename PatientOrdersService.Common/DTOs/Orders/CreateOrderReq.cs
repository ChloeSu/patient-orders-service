using System.ComponentModel.DataAnnotations;

namespace PatientOrdersService.Common.Dtos
{
    public class CreateOrderReq
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
