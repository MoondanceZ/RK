using RK.Framework.Common;
using RK.Model;
using RK.Model.Dto.Reponse;
using RK.Model.Dto.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Service
{
    public interface IAccountRecordService : IBaseService<AccountRecord>
    {
        ReturnStatus<AccountResponse> Create(AccountRequest request);
        ReturnStatus<AccountResponse> Update(int id, AccountRequest request);
        ReturnPage<AccountResponse> GetList(int pageIndex, int pageSize, int userId);
        ReturnStatus<AccountResponse> Get(int id);
        ReturnStatus Delete(int id);
    }
}
