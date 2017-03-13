using System.ComponentModel.DataAnnotations;

namespace Server.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Kennwort")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Kennwort wiederholen")]
        [Compare("Password", ErrorMessage = "Die Kennwörter stimmen nicht überein")]
        public string ConfirmPassword { get; set; }
    }
}
