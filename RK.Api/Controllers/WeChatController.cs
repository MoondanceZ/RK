﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RK.Infrastructure;
using System.Net.Http;
using RK.Model.Dto.Reponse;
using Microsoft.Extensions.Caching.Memory;
using RK.Service;
using RK.Infrastructure.Exceptions;

namespace RK.Api.Controllers
{

    [AllowAnonymous]
    [Route("[controller]")]
    public class WeChatController : Controller
    {
        private static string apiUrl;
        private static string appId;
        private static string appSecret;


        private IUserInfoService _userInfoService;
        public WeChatController(IUserInfoService userInfoService)
        {
            apiUrl = ConfigHelper.ConfigurationBuilder.GetSection("WeChat:ApiUrl").Value;
            appId = ConfigHelper.ConfigurationBuilder.GetSection("WeChat:AppId").Value;
            appSecret = ConfigHelper.ConfigurationBuilder.GetSection("WeChat:AppSecret").Value;

            _userInfoService = userInfoService;
        }

        [HttpGet("Login")]
        public async Task<ReturnStatus<WeChatUserInfoResponse>> Login(string code)
        {
            var requestUrl = apiUrl + $"sns/jscode2session?appid={appId}&secret={appSecret}&js_code={code}&grant_type=authorization_code";
            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetStringAsync(requestUrl);

                var weChatResponse = JsonHelper.Deserialize<WeChatOpenIdResponse>(result);
                if (!String.IsNullOrWhiteSpace(weChatResponse.SessionKey) && !String.IsNullOrWhiteSpace(weChatResponse.OpenId))
                {
                    return await _userInfoService.GetWeChatUser(weChatResponse.OpenId);
                }
                else
                    throw new WeChatException(weChatResponse.ErrMsg);
            }
        }
    }
}