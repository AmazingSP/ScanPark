using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Server.Models
{
    public class UsersModel : IdentityUser
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
    }
}