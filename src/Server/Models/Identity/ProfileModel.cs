using System.ComponentModel.DataAnnotations;

namespace Server.Models.Identity
{
    public class ProfileModel
    {
        [Key]
        public int ProfileId { get; set; }

        [Display(Name = "Vorname")]
        public string FirstName { get; set; }

        [Display(Name = "Nachname")]
        public string LastName { get; set; }

        [Display(Name = "Straße")]
        public string Street { get; set; }

        [Display(Name = "Ort")]
        public string City { get; set; }

        [Display(Name = "Postleitzahl")]
        public string ZipCode { get; set; }

        [Display(Name = "Land")]
        public string Country { get; set; }

        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Display(Name = "IBAN")]
        public string IBAN { get; set; }

        [Display(Name = "BIC")]
        public string BIC { get; set; }

        public RegisteredUserModel User { get; set; }
    }
}
