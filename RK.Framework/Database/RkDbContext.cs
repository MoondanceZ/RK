using Microsoft.EntityFrameworkCore;
using RK.Model;

namespace RK.Framework.Database
{
    public class RkDbContext : DbContext
    {
        public RkDbContext(DbContextOptions<RkDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountRecord>().HasQueryFilter(e => e.Status == 1);
            modelBuilder.Entity<AccountType>().HasQueryFilter(e => e.Status == 1);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserInfo> UserInfo { set; get; }
        public DbSet<AccountRecord> AccountRecord { get; set; }
        public DbSet<AccountType> AccountType { get; set; }
    }
}