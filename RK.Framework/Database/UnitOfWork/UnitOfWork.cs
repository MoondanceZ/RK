using Microsoft.EntityFrameworkCore;
using RK.Framework.Database.DatabaseFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Framework.Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private RkDbContext dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected RkDbContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.GetDbContext()); }
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
