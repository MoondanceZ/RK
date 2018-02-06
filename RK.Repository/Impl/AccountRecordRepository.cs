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

        /// <summary>
        /// 逻辑删
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(AccountRecord entity)
        {
            entity.Status = -1;
            entity.DeletedTime = DateTime.Now;
            base.Update(entity);
        }
    }
}
