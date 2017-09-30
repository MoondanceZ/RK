using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RK.Framework.Common;

namespace RK.Framework.Database.DatabaseFactory
{
    public class DatabaseFactory : Disposable, IDatabaseFactory, IDesignTimeDbContextFactory<RkDbContext>
    {    
        public RkDbContext DataContext { get; private set; }

        public RkDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<RkDbContext>();
            builder.UseMySql(@"server=(local);userid=root;pwd=123456;port=3306;database=rk;sslmode=none;");

            DataContext = new RkDbContext(builder.Options);

            return DataContext;
        }

        protected override void DisposeCore()
        {
            if (DataContext != null)
                DataContext.Dispose();
        }
    }
}
