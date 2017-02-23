using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Server.Models
{
    public class UserRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
