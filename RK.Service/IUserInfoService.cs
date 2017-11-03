using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Service
{
    public interface IUserInfoService : IBaseService<UserInfo>
    {
        UserInfo AddUserInfo(UserInfo userInfo);
        List<UserInfo> ListAll();
    }
}
