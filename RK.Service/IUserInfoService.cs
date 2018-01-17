using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Framework.Common;
using RK.Model.Dto.Request;
using RK.Model.Dto.Reponse;

namespace RK.Service
{
    public interface IUserInfoService : IBaseService<UserInfo>
    {
        List<UserInfo> ListAll();
        bool Auth(string username, string password);
        UserInfo GetUserByAccount(string account);
        ReturnStatus<UserSignUpResponse> Create(UserSignUpRequest request);
    }
}
