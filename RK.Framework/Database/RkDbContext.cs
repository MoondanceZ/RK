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
        }
        public DbSet<UserInfo> UserInfos { set; get; }

    }
}