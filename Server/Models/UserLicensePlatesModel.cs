using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("UserLicensePlates")]
    public class UserLicensePlatesModel
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }
        [ForeignKey("LicensePlateID")]
        public int LicensePlateID { get; set; }
    }
}