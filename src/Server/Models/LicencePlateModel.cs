using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class LicencePlateModel
    {
        [Key]
        public string LicencePlateId { get; set; }

        public string District { get; set; }

        public string Identifier { get; set; }

        public int Number { get; set; }
    }
}
