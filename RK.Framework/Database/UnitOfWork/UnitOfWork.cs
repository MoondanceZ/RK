using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RK.Framework.Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDesignTimeDbContextFactory<RkDbContext> databaseFactory;
        private RkDbContext dataContext;

        public UnitOfWork(IDesignTimeDbContextFactory<RkDbContext> databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected RkDbContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.CreateDbContext(null)); }
        }

        public void BeginTransaction()
        {
            DataContext.Database.BeginTransaction();
        }

        public void BeginTransactionAsync()
        {
            DataContext.Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }

        public void CommitAsync()
        {
            DataContext.SaveChangesAsync();
        }

        public void CommitTransaction()
        {
            DataContext.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            DataContext.Database.RollbackTransaction();
            foreach (var item in DataContext.ChangeTracker.Entries())
            {
                item.State = EntityState.Detached;
            }
        }
    }
}
