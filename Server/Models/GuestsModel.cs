using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("Guests")]
    public class GuestsModel
    {
        [Key]
        public int GuestID { get; set; }
        public int LicensePlateID { get; set; }
        public int GuestPaymentCode { get; set; }
    }
}