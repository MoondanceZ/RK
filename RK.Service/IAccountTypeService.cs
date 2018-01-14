using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Service
{
    public interface IAccountTypeService : IBaseService<AccountType>
    {
        IEnumerable<AccountType> GetAccountRecordTypes(int userInfoId);
    }
}
