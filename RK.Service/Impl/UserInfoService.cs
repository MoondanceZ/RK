using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Repository;
using RK.Framework.Database;

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
    }
}
