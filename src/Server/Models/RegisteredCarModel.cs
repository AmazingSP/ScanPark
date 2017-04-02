using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class RegisteredCarModel
    {
        [Key]
        public string RegisteredCarId { get; set; }

        public string RegisteredUserId { get; set; }

        public string RegisteredLicenceId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }
    }
}
