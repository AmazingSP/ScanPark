using System.ComponentModel.DataAnnotations;

namespace Server.Models.ViewModels
{
    public class GuestViewModel
    {
        [Required]
        public string LicensePlate { get; set; }

        [Required]
        public string OneTimePaymentCode { get; set; }
    }
}
