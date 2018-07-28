using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Framework.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RK.Infrastructure.Enum;

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

        public decimal GetSumIncome(int userId, DateTime startDate, DateTime endDate)
        {
            return this.Dbset.Where(m => m.UserInfoId == userId && m.Type == (int)AccountRecordTypeEnum.Income && m.AccountDate >= startDate && m.AccountDate < endDate)
                .Sum(m => m.Amount);
        }

        public decimal GetSumExpend(int userId, DateTime startDate, DateTime endDate)
        {
            return this.Dbset.Where(m => m.UserInfoId == userId && m.Type == (int)AccountRecordTypeEnum.Expend && m.AccountDate >= startDate && m.AccountDate < endDate)
                .Sum(m => m.Amount);
        }

        public IEnumerable<AccountRecord> GetLastMonthTop3Expend(int userId, DateTime startDate, DateTime endDate)
        {
            return this.Dbset.Include(m => m.AccountType)
                .Where(m => m.UserInfoId == userId && m.Type == (int)AccountRecordTypeEnum.Expend && m.AccountDate >= startDate && m.AccountDate < endDate)
                .OrderByDescending(m => m.Amount).Take(3);
        }
    }
}
