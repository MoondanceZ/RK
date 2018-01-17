using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Infrastructure;
using RK.Model.Dto.Request;
using RK.Model.Dto.Reponse;

namespace RK.Service
{
    public interface IUserInfoService : IBaseService<UserInfo>
    {
        bool AuthUser(string username, string password);
        UserInfo GetUserByAccount(string account);
        ReturnStatus<UserSignUpResponse> Create(UserSignUpRequest request);
        ReturnStatus<UserInfoResponse> Get(int id);
        ReturnStatus Update(UpdateUserRequest request);
    }
}
