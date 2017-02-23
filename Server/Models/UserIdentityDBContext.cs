using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class UserIdentityDBContext : IdentityDbContext<UserIdentity, UserRole, string>
    {
        public UserIdentityDBContext(DbContextOptions<UserIdentityDBContext> options) : base(options)
        {
            // Kein Code
        }

    }
}
