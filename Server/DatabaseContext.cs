using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server
{
    public class DataBaseContext : DbContext
    {
        //public DbSet<EntryModel> Entries { get; set; }

        public DbSet<AccountsModel> Accounts { get; set; }
        public DbSet<BanksModel> Banks { get; set; }
        public DbSet<BillsModel> Bills { get; set; }
        public DbSet<CarParksModel> CarParks { get; set; }
        public DbSet<GuestsModel> Guests { get; set; }
        public DbSet<LicensePlatesModel> LicensePlates { get; set; }
        public DbSet<OccurencesModel> Occurences { get; set; }
        public DbSet<UserBankAccountsModel> UserBankAccounts { get; set; }
        public DbSet<UserLicensePlatesModel> UsereLicensePlates { get; set; }
        public DbSet<UsersModel> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=scanpark.db");
        }
    }
}