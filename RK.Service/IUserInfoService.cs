using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Framework.Common;
using RK.Model.Dto.Request;

namespace RK.Service
{
    public interface IUserInfoService : IBaseService<UserInfo>
    {
        UserInfo AddUserInfo(UserInfo userInfo);
        List<UserInfo> ListAll();
        bool Auth(string username, string password);
        UserInfo GetUserByAccount(string account);
    }
}
