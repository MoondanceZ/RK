using RK.Framework.Common;
using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Service
{
    public interface IAccountTypeService : IBaseService<AccountType>
    {
        ReturnStatus<List<AccountType>> GetAccountRecordTypes(int userInfoId);
    }
}
