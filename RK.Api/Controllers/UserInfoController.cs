using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RK.Service;
using Microsoft.AspNetCore.Authorization;
using RK.Infrastructure;
using RK.Model.Dto.Request;
using Response = RK.Model.Dto.Reponse;
using IdentityModel.Client;

namespace RK.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UserInfoController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ReturnStatus<Response.UserSignInResponse>> Login([FromBody]UserSignInRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ReturnStatus<Response.UserSignInResponse>.Error("请求参数有误");
            }           

            return await _userInfoService.LoginAsync(request);
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnStatus<Response.UserSignInResponse>> Post([FromBody]UserSignUpRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ReturnStatus<Response.UserSignInResponse>.Error("请求参数有误");
            }
            return await _userInfoService.CreateAsync(request);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        [HttpPut]
        public ReturnStatus Put([FromBody]UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ReturnStatus.Error("请求参数有误");
            }
            return _userInfoService.Update(request);
        }

        // DELETE api/userInfo/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
