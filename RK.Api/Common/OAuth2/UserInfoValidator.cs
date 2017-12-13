using IdentityServer4.Models;
using IdentityServer4.Validation;
using RK.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RK.Api.Common.OAuth2
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
            var userInfo = _userInfoService.GetUserByAccountAndPassword(context.UserName, context.Password);
            if (userInfo != null)
            {
                context.Result = new GrantValidationResult();
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            }
            return Task.FromResult(0);
        }
    }
}
