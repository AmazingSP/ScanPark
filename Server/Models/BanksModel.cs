using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("Banks")]
    public class BanksModel
    {
        [Key]
        public int BankID { get; set; }
        public string Name { get; set; }
        public string BIC { get; set; }
    }
}