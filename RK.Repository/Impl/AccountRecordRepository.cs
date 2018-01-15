using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Framework.Database;

namespace RK.Repository.Impl
{
    public class AccountRecordRepository : BaseRepository<AccountRecord>, IAccountRecordRepository
    {
        public AccountRecordRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
