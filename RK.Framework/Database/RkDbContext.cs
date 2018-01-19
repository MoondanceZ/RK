using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using RK.Infrastructure;
using RK.Model;

namespace RK.Framework.Database
{
    public class RkDbContext : DbContext
    {
        //private static readonly ILoggerFactory _loggerFactory = new LoggerFactory(new[] {
        //    new ConsoleLoggerProvider((category, level)=> category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true)
        //});
        public RkDbContext(DbContextOptions<RkDbContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLoggerFactory(_loggerFactory);
        //}
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