using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("UserBankAccounts")]
    public class UserBankAccountsModel
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }
        [ForeignKey("AccountID")]
        public int AccountID { get; set; }
    }
}
