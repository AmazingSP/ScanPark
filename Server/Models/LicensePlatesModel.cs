using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("LicensePlates")]
    public class LicensePlatesModel
    {
        [Key]
        public int LicensePlateID { get; set; }
        public string LicensePlate { get; set; }
    }
}