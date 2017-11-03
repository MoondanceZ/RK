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
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserInfo> UserInfo { set; get; }

    }
}