﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RK.Service;
using Microsoft.AspNetCore.Authorization;
using RK.Infrastructure;
using RK.Model.Dto.Request;
using RK.Model.Dto.Reponse;

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

        // GET api/userInfo/5
        [HttpGet("{id}")]
        public ReturnStatus<UserInfoResponse> Get(int id)
        {
            return _userInfoService.Get(id);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public ReturnStatus<UserSignUpResponse> Post([FromBody]UserSignUpRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ReturnStatus<UserSignUpResponse>.Error("请求参数有误");
            }
            return _userInfoService.Create(request);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        [HttpPut("{id}")]
        public ReturnStatus Put(int id, [FromBody]UpdateUserRequest request)
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
