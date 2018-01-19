using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RK.Infrastructure;
using System.IO;

namespace RK.Framework.Database.Impl
{
    public class DatabaseFactory : IDatabaseFactory, IDesignTimeDbContextFactory<RkDbContext>
    {
        private readonly RkDbContext _dbContext;

        public DatabaseFactory()
        {
            _dbContext = CreateDbContext(null);
        }
        public RkDbContext DataContext
        {
            get
            {
                return _dbContext;
            }
        }

        public RkDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<RkDbContext>();
            var connStr = ConfigHelper.GetConnectionString("ConnStr");
            builder.UseMySql(connStr);

            //Ensure database creation
            var dataContext = new RkDbContext(builder.Options);
            //dataContext.Database.EnsureCreated();  //删除这句  因为执行 Update-Database 报错
            return dataContext;
        }
    }
}
