using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RK.Infrastructure;
using System.IO;

namespace RK.Framework.Database.Impl
{
    public class DatabaseFactory : IDatabaseFactory, IDesignTimeDbContextFactory<RkDbContext>
    {
        public DatabaseFactory()
        {
            DataContext = DataContext ?? CreateDbContext(null);
        }
        public RkDbContext DataContext { get; }
        public DatabaseFactory(RkDbContext dbContext)
        {
            DataContext = dbContext;
        }

        public RkDbContext CreateDbContext(string[] args)
        {
            //var builder = new DbContextOptionsBuilder<RkDbContext>();
            //var connStr = ConfigHelper.GetConnectionString("ConnStr");
            //builder.UseMySql(connStr);

            ////Ensure database creation
            //var dataContext = new RkDbContext(builder.Options);
            ////dataContext.Database.EnsureCreated();  //删除这句  因为执行 Update-Database 报错
            //return dataContext;
            return new RkDbContext(new DbContextOptionsBuilder<RkDbContext>().UseMySql(ConfigHelper.GetConnectionString("ConnStr")).Options);
        }
    }
}
