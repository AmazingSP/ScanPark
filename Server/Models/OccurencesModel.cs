using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("Occurences")]
    public class OccurencesModel
    {
        [Key]
        public int OccurenceID { get; set; }
        public int LicensePlateID { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int Duration { get; set; }
    }
}