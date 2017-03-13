using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class RegisterViewModel
    {
        //[Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get
            {
                return UserName;
            }

            set
            {
                UserName = value;
            }
        }
        [Required]
        public string FullName { get; set; }
    }
}
