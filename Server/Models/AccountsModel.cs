using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("Accounts")]
    public class AccountsModel
    {
        [Key]
        public int AccountID { get; set; }
        public string IBAN { get; set; }
        public int BankID { get; set; }
    }
}