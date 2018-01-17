using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Repository;
using RK.Framework.Database;
using System.Linq;
using RK.Framework.Common;
using RK.Model.Dto.Request;
using RK.Model.Dto.Reponse;

namespace RK.Service.Impl
{
    public class UserInfoService : BaseService<UserInfo, IUserInfoRepository>, IUserInfoService
    {
        public UserInfoService(IUnitOfWork unitOfWork, IUserInfoRepository repository) : base(unitOfWork, repository)
        {
        }

        public bool Auth(string account, string password)
        {
            var user = _repository.Get(m=>m.Account==account);
            return true;
        }

        public ReturnStatus<UserSignUpResponse> Create(UserSignUpRequest request)
        {
            throw new NotImplementedException();
        }

        public UserInfo GetUserByAccount(string account)
        {
            return _repository.Get(m => m.Account == account);
        }

        public List<UserInfo> ListAll()
        {
            var list = _repository.GetAll();
            return list.ToList();
        }        
    }
}
