using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Identity
{
    public class RegisteredPlateModel
    {
        [Key]
        public int RegisteredPlateId { get; set; }

        [ForeignKey("Profile")]
        public int ProfileId { get; set; }

        public string PlateDistrict { get; set; }

        public string PlateIdentifier { get; set; }

        public int PlateNumber { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }
    }
}
