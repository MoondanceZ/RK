using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Repository
{
    public interface IAccountRecordRepository : IBaseRepository<AccountRecord>
    {
        decimal GetSumIncome(int userId, DateTime startDate, DateTime endDate);
        decimal GetSumExpend(int userId, DateTime startDate, DateTime endDate);
        IEnumerable<AccountRecord> GetLastMonthTop3Expend(int userId, DateTime startDate, DateTime endDate);
    }
}
