using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class OccurrenceModel
    {
        [Key]
        public int OccurrenceId { get; set; }

        public string LicencePlateId { get; set; }

        public string Date { get; set; }

        public string Entrance { get; set; }

        public string Exit { get; set; }

        public string Duration { get; set; }

    }
}