using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RK.Framework.Database
{
    public class DatabaseFactory : IDesignTimeDbContextFactory<RkDbContext>
    {
        public RkDbContext DataContext { get; private set; }

        public RkDbContext CreateDbContext(string[] args)
        {
            if (DataContext == null)
            {
                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection()    //将配置文件的数据加载到内存中
                    .SetBasePath(Directory.GetCurrentDirectory())   //指定配置文件所在的目录
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)  //指定加载的配置文件
                    .Build();    //编译成对象  
                var builder = new DbContextOptionsBuilder<RkDbContext>();
                var connStr = configuration.GetConnectionString("ConnStr");
                builder.UseMySql(connStr);

                //Ensure database creation
                this.DataContext = new RkDbContext(builder.Options);
                this.DataContext.Database.EnsureCreated();
                return DataContext;
            }

            return DataContext;
        }
    }
}
