using System.ComponentModel.DataAnnotations;

namespace PatientOrdersService.Common.Dtos
{
    public class UpdateOrderReq
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
