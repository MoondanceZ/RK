using RK.Framework.Database;
using RK.Model;
using RK.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Model.Dto.Request;
using RK.Infrastructure;
using System.Linq;

namespace RK.Service.Impl
{
    public class AccountTypeService : BaseService<AccountType, IAccountTypeRepository>, IAccountTypeService
    {
        public AccountTypeService(IUnitOfWork unitOfWork, IAccountTypeRepository repository) : base(unitOfWork, repository)
        {
        }

        public ReturnStatus<List<AccountType>> GetAccountRecordTypes(int userInfoId)
        {
            return ReturnStatus<List<AccountType>>.Success(string.Empty, _repository.GetMany(m => m.UserInfoId == 0 || m.UserInfoId == userInfoId).ToList());
        }
    }
}
