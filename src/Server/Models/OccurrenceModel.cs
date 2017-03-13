using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class OccurrenceModel
    {
        [Key]
        public int OccurrenceId { get; set; }

        public int LicensePlate { get; set; }

        public string Date { get; set; }

        public string Entrance { get; set; }

        public string Exit { get; set; }

        public string Dureation { get; set; }

        public string Price { get; set; }

        public bool Paied { get; set; }
    }
}