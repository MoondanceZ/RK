using RK.Infrastructure;
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
        ReturnStatus Update(int id, AccountRequest request);
        ReturnPage<DateAccountResponse> GetList(AccountPageListRequest request);
        ReturnStatus<AccountResponse> Get(int id);
        ReturnStatus Delete(int id);
    }
}
