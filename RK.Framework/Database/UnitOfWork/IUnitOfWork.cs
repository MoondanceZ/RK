using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Framework.Database.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        void CommitAsync();

        void BeginTransaction();
        void BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();

    }
}
