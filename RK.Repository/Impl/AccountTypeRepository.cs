using RK.Framework.Database;
using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Repository.Impl
{
    public class AccountTypeRepository : BaseRepository<AccountType>, IAccountTypeRepository
    {
        public AccountTypeRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
