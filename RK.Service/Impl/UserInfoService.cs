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
using Microsoft.Extensions.Caching.Memory;
using IdentityModel.Client;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace RK.Service.Impl
{
    public class UserInfoService : BaseService<UserInfo, IUserInfoRepository>, IUserInfoService
    {
        private IMemoryCache _cache;
        private readonly ILogger<UserInfoService> _logger;
        private readonly string _identityServer4Url;
        public UserInfoService(IUnitOfWork unitOfWork, IUserInfoRepository repository, IMemoryCache cache, IHttpContextAccessor httpConetext, ILogger<UserInfoService> logger) : base(unitOfWork, repository, httpConetext)
        {
            _cache = cache;
            _logger = logger;
            _identityServer4Url = ConfigHelper.ConfigurationBuilder.GetSection("IdentityServer4Url").Value;
        }

        public bool AuthUser(string account, string password)
        {
            var user = _repository.Get(m => m.Account == account);
            if (user != null && user.Password == EncryptHelper.AESDecrypt(password))
                return true;
            return false;
        }

        public async Task<ReturnStatus<UserSignInResponse>> CreateAsync(UserSignUpRequest request)
        {
            var isExist = _repository.IsExist(m => m.Account == request.Account);
            if (isExist)
                throw new Exception("该帐号已存在");
            try
            {
                var user = new UserInfo
                {
                    Account = request.Account,
                    Password = EncryptHelper.AESEncrypt(request.Password)
                };
                _repository.Add(user);
                _unitOfWork.Commit();


                try
                {
                    var token = await GetToken(request.Account, request.Password);
                    _cache.Set(user.Id, $"{token.token_type} {token.access_token}", TimeSpan.FromSeconds(token.expires_in));
                    return ReturnStatus<UserSignInResponse>.Success("注册成功", new UserSignInResponse
                    {
                        UserInfo = new Model.Dto.Reponse.UserInfoResponse
                        {
                            Id = user.Id,
                            Account = user.Account,
                            AvatarUrl = user.AvatarUrl,
                            Email = user.Email,
                            Name = user.Name,
                            Phone = user.Phone,
                            Sex = user.Sex
                        },
                        Token = token
                    });
                }
                catch (Exception)
                {
                    throw new Exception("注册成功，获取 Token 失败，请重新登录");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ReturnStatus<UserSignInResponse>> LoginAsync(UserSignInRequest request)
        {
            var user = _repository.Get(m => m.Account == request.Account);
            if (user != null)
            {
                var token = await GetToken(request.Account, request.Password);
                _cache.Set(user.Id, $"{token.token_type} {token.access_token}", TimeSpan.FromSeconds(token.expires_in));
                return ReturnStatus<UserSignInResponse>.Success("登录成功", new UserSignInResponse
                {
                    UserInfo = new Model.Dto.Reponse.UserInfoResponse
                    {
                        Id = user.Id,
                        Account = user.Account,
                        AvatarUrl = user.AvatarUrl,
                        Email = user.Email,
                        Name = user.Name,
                        Phone = user.Phone,
                        Sex = user.Sex
                    },
                    Token = token
                });
            }
            else
                throw new Exception("该帐号不存在");
        }

        public UserInfo GetUserByAccount(string account)
        {
            return _repository.Get(m => m.Account == account);
        }

        public ReturnStatus Update(UpdateUserRequest request)
        {
            if (!CheckCurrentUserValid(request.Id))
                throw new Exception("无权限操作");

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
                throw new Exception("该帐号不存在");
        }

        private async Task<UserSignInResponse.TokenModel> GetToken(string account, string password)
        {
            try
            {
                var disco = await DiscoveryClient.GetAsync(_identityServer4Url);
                if (disco.IsError)
                {
                    throw new Exception(disco.Error);
                }

                var tokenClient = new TokenClient(disco.TokenEndpoint, "pwd_client", "pwd_secret");
                var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(account, password, "rk offline_access");
                if (tokenResponse.IsError)
                {
                    throw new Exception(tokenResponse.Error);
                }
                return new UserSignInResponse.TokenModel
                {
                    access_token = tokenResponse.AccessToken,
                    expires_in = tokenResponse.ExpiresIn,
                    refresh_token = tokenResponse.RefreshToken,
                    token_type = tokenResponse.TokenType
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());

                throw new Exception("注册失败，获取 Token 异常");
            }
        }
    }
}
