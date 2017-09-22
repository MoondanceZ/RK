using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySQL.Data.EntityFrameworkCore.Extensions;
using RK.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RK.Framework.Database.DatabaseFactory
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private RkDbContext DataContext { get; set; }
        public RkDbContext GetDbContext()
        {
            if (DataContext == null)
            {
                var configuration = new ConfigurationBuilder().Build();
                var optionsBuilder = new DbContextOptionsBuilder<RkDbContext>();
                optionsBuilder.UseMySQL(configuration.GetConnectionString("ConnStr"));

                //Ensure database creation
                var context = new RkDbContext(optionsBuilder.Options);
                context.Database.EnsureCreated();
            }
            return DataContext;
        }

        protected override void DisposeCore()
        {
            if (DataContext != null)
                DataContext.Dispose();
        }
    }
}
