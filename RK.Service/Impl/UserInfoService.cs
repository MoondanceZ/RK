using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Repository;
using RK.Framework.Database.UnitOfWork;

namespace RK.Service.Impl
{
    public class UserInfoService : BaseService<UserInfo, IUserInfoRepository>, IUserInfoService
    {
        public UserInfoService(IUnitOfWork unitOfWork, IUserInfoRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
