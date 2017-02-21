using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("Bills")]
    public class BillsModel
    {
        [Key]
        public int BillID { get; set; }
        public int LicensePlateID { get; set; }
        public int Amount { get; set; }
        public int Paid { get; set; }
    }
}