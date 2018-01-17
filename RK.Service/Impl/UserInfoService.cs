using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Repository;
using RK.Framework.Database;
using System.Linq;
using RK.Framework.Common;
using RK.Model.Dto.Request;

namespace RK.Service.Impl
{
    public class UserInfoService : BaseService<UserInfo, IUserInfoRepository>, IUserInfoService
    {
        public UserInfoService(IUnitOfWork unitOfWork, IUserInfoRepository repository) : base(unitOfWork, repository)
        {
        }

        public UserInfo AddUserInfo(UserInfo userInfo)
        {
            _repository.Add(userInfo);
            _unitOfWork.Commit();
            return userInfo;
        }

        public bool Auth(string username, string password)
        {
            return true;
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
