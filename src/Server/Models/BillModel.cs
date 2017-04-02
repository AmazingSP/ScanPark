using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class BillModel
    {
        [Key]
        public int BillId { get; set; }

        [ForeignKey("CarParkId")]
        public int CarParkId { get; set; }

        [ForeignKey("OccurenceId")]
        public int OccurenceId { get; set; }

        public string Amount { get; set; }

        public bool Paied { get; set; }
    }
}
