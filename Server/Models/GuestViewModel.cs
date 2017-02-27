using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class GuestViewModel
    {
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public string OneTimePaymentCode { get; set; }
    }
}
