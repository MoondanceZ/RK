using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Infrastructure;
using RK.Model.Dto.Request;
using RK.Model.Dto.Reponse;
using System.Threading.Tasks;

namespace RK.Service
{
    public interface IUserInfoService : IBaseService<UserInfo>
    {
        bool AuthUser(string username, string password);
        UserInfo GetUserByAccount(string account);
        Task<ReturnStatus<UserSignInResponse>> CreateAsync(UserSignUpRequest request);
        ReturnStatus Update(UpdateUserRequest request);
        Task<ReturnStatus<UserSignInResponse>> LoginAsync(UserSignInRequest request);
    }
}
