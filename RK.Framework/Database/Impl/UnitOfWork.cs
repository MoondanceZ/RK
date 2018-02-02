using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RK.Framework.Database.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private readonly RkDbContext dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
            this.dataContext = databaseFactory.DataContext;
        }

        protected RkDbContext DataContext
        {
            get
            {
                return dataContext;
            }
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
            //foreach (var item in DataContext.ChangeTracker.Entries())
            //{
            //    item.State = EntityState.Detached;
            //}
        }
    }
}
