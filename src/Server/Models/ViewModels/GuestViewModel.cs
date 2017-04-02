using System.ComponentModel.DataAnnotations;

namespace Server.Models.ViewModels
{
    public class GuestViewModel
    {
        [Required]
        [Display(Name = "Landkreis (z.B. KA)")]
        public string District { get; set; }


        [Required]
        [Display(Name = "Kennzeichen")]
        public string Identifier { get; set; }


        [Required]
        [Display(Name = "Nummer")]
        public int Number { get; set; }


        [Required]
        [Display(Name = "Einmaliger Zahlungscode")]
        public string OneTimePaymentCode { get; set; }
    }
}
