using RK.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Repository;
using RK.Framework.Database;
using System.Linq;
using RK.Infrastructure;
using RK.Model.Dto.Request;
using RK.Model.Dto.Reponse;

namespace RK.Service.Impl
{
    public class UserInfoService : BaseService<UserInfo, IUserInfoRepository>, IUserInfoService
    {
        public UserInfoService(IUnitOfWork unitOfWork, IUserInfoRepository repository) : base(unitOfWork, repository)
        {
        }

        public bool AuthUser(string account, string password)
        {
            var user = _repository.Get(m => m.Account == account);
            if (user != null && user.Password == EncryptHelper.AESDecrypt(password))
                return true;
            return false;
        }

        public ReturnStatus<UserSignUpResponse> Create(UserSignUpRequest request)
        {
            var isExist = _repository.IsExist(m => m.Account == request.Account);
            if (isExist)
                return ReturnStatus<UserSignUpResponse>.Error("该帐号已存在");
            var user = new UserInfo
            {
                Account = request.Account,
                Password = EncryptHelper.AESEncrypt(request.Password)
            };
            _repository.Add(user);
            _unitOfWork.Commit();
            return ReturnStatus<UserSignUpResponse>.Success("注册成功", new UserSignUpResponse
            {
                Id = user.Id,
                Account = user.Account
            });
        }

        public ReturnStatus<UserInfoResponse> Get(string account)
        {
            var user = _repository.Get(m => m.Account == account);
            if (user != null)
                return ReturnStatus<UserInfoResponse>.Success(null, new UserInfoResponse
                {
                    Id = user.Id,
                    Account = user.Account,
                    AvatarUrl = user.AvatarUrl,
                    Email = user.Email,
                    Name = user.Name,
                    Phone = user.Phone,
                    Sex = user.Sex
                });
            else
                return ReturnStatus<UserInfoResponse>.Error("该帐号不存在");
        }

        public UserInfo GetUserByAccount(string account)
        {
            return _repository.Get(m => m.Account == account);
        }

        public ReturnStatus Update(UpdateUserRequest request)
        {
            var user = _repository.Get(m => m.Id == request.Id);
            if (user != null)
            {
                user.AvatarUrl = request.AvatarUrl;
                user.Email = request.Email;
                user.Password = EncryptHelper.AESEncrypt(request.Password);
                user.Phone = request.Phone;
                user.Name = request.Name;
                user.Sex = request.Sex;
                _repository.Update(user);
                _unitOfWork.Commit();
                return ReturnStatus.Success("更新成功");
            }
            else
                return ReturnStatus.Error("该帐号不存在");
        }
    }
}
