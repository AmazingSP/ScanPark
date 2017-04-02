using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class ApplicationDbContext : IdentityDbContext<RegisteredUserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<RegisteredUserModel> RegisteredUsers { get; set; }

        public DbSet<RegisteredCarModel> RegisteredCars { get; set; }

        public DbSet<OccurrenceModel> Occurrences { get; set; }

        public DbSet<BillModel> Bills { get; set; }

        public DbSet<CarParkModel> CarParks { get; set; }

        public DbSet<LicencePlateModel> LicencePlates { get; set; }
    }
}
