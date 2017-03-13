using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Models.Identity;

namespace Server.Models
{
    public class ApplicationDbContext : IdentityDbContext<RegisteredUserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<ProfileModel> Profiles { get; set; }

        public DbSet<BillModel> Bills { get; set; }

        public DbSet<OccurrenceModel> Occurrences { get; set; }

        public DbSet<RegisteredPlateModel> RegisteredPlates { get; set; }
    }
}
