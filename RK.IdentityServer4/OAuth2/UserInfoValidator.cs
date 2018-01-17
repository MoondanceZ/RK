using IdentityServer4.Models;
using IdentityServer4.Validation;
using RK.Infrastructure;
using RK.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RK.IdentityServer4.OAuth2
{
    public class UserInfoValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserInfoService _userInfoService;
        public UserInfoValidator(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            return Task.Run(() =>
            {
                try
                {
                    //get your user model from db (by username - in my case its email)
                    var userInfo = _userInfoService.GetUserByAccount(context.UserName);
                    if (userInfo != null)
                    {
                        //check if password match - remember to hash password if stored as hash in db
                        if (userInfo.Password == EncryptHelper.AESDecrypt(context.Password))
                        {
                            //set the result
                            context.Result = new GrantValidationResult(subject: context.UserName, authenticationMethod: "Resource Owner Password");
                        }
                        else
                        {
                            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                        }
                    }
                    else
                    {
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                    }
                }
                catch (Exception)
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
                }
            });
        }
    }
}
