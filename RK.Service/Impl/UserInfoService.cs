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
using RK.Model.Dto.Common;

namespace RK.Service.Impl
{
    public class UserInfoService : BaseService<UserInfo, IUserInfoRepository>, IUserInfoService
    {
        private readonly ILogger<UserInfoService> _logger;
        private readonly string _identityServer4Url;
        public UserInfoService(IUnitOfWork unitOfWork, IUserInfoRepository repository, IHttpContextAccessor httpConetext, ILogger<UserInfoService> logger) : base(unitOfWork, repository, httpConetext)
        {
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
                //user.Password = EncryptHelper.AESEncrypt(request.Password);
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

        private async Task<TokenModel> GetToken(string account, string password)
        {
            try
            {
                var discoveryClient = new DiscoveryClient(_identityServer4Url);
                if (_identityServer4Url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    discoveryClient.Policy.RequireHttps = false;
                }
                var disco = await discoveryClient.GetAsync();

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
                return new TokenModel
                {
                    Accesstoken = tokenResponse.AccessToken,
                    ExpiresIn = tokenResponse.ExpiresIn,
                    RefreshToken = tokenResponse.RefreshToken,
                    TokenType = tokenResponse.TokenType
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());

                throw new UnauthorizedAccessException("获取 Token 异常");
            }
        }

        public async Task<ReturnStatus<WeChatUserInfoResponse>> GetWeChatUser(string openId)
        {
            var user = _repository.Get(m => m.WeChatOpenID == openId);
            if (user != null)
            {
                var token = await GetToken(user.Account, user.Password);
                return ReturnStatus<WeChatUserInfoResponse>.Success(null, new WeChatUserInfoResponse
                {
                    Id = user.Id,
                    Token = token
                });
            }
            else
            {
                try
                {
                    user = new UserInfo
                    {
                        WeChatOpenID = openId,
                        Account = openId,
                        Password = EncryptHelper.AESEncrypt("#WY_YK#")
                    };
                    _repository.Add(user);
                    _unitOfWork.Commit();


                    try
                    {
                        var token = await GetToken(openId, "#WY_YK#");
                        return ReturnStatus<WeChatUserInfoResponse>.Success(null, new WeChatUserInfoResponse
                        {
                            Id = user.Id,
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
        }
    }
}
