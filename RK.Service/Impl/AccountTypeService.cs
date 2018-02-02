using RK.Framework.Database;
using RK.Model;
using RK.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Model.Dto.Request;
using RK.Infrastructure;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace RK.Service.Impl
{
    public class AccountTypeService : BaseService<AccountType, IAccountTypeRepository>, IAccountTypeService
    {
        public AccountTypeService(IUnitOfWork unitOfWork, IAccountTypeRepository repository, IHttpContextAccessor httpConetext) : base(unitOfWork, repository, httpConetext)
        {
        }

        public ReturnStatus<List<AccountType>> GetAccountRecordTypes(int userInfoId)
        {
            if (!CheckCurrentUserValid(userInfoId))
                throw new Exception("无权限操作");
            return ReturnStatus<List<AccountType>>.Success(string.Empty, _repository.GetMany(m => m.UserInfoId == 0 || m.UserInfoId == userInfoId).ToList());
        }
    }
}
