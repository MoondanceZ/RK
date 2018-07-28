using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using RK.Infrastructure;
using RK.Web.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using RK.Service;

namespace RK.Web.Controllers
{
    public class ConnectController : Controller
    {
        static readonly string AppId = ConfigHelper.ConfigurationBuilder.GetSection("QQ:AppId").Value;
        static readonly string AppKey = ConfigHelper.ConfigurationBuilder.GetSection("QQ:AppKey").Value;
        static readonly string RedirectUri = ConfigHelper.ConfigurationBuilder.GetSection("QQ:RedirectUri").Value;

        private readonly ILogger<ConnectController> _logger;
        private readonly IUserInfoService _userInfoService;
        public ConnectController(ILogger<ConnectController> logger, IUserInfoService userInfoService)
        {
            _logger = logger;
            _userInfoService = userInfoService;
        }

        public IActionResult QQ_Login()
        {
            var qcState = DateTime.Now.ToString("yyyyMMddHHmmssffff") + StringHelper.RndomStr(8);
            HttpContext.Session.SetString("QC_State", qcState);

            var apiUrl = "https://graph.qq.com/oauth2.0/authorize" + $"?response_type=code&client_id={AppId}&redirect_uri={RedirectUri}&scope=get_user_info&state={qcState}";
            return Redirect(apiUrl);
        }

        public async Task<IActionResult> QQ_Callback(string code, string state)
        {
            if (!String.IsNullOrWhiteSpace(code) && !String.IsNullOrWhiteSpace(state))
            {
                if (HttpContext.Session.GetString("QC_State") == state)
                {
                    //通过Authorization Code获取Access Token
                    var getTokenUrl = "https://graph.qq.com/oauth2.0/token" + $"?grant_type=authorization_code&client_id={AppId}&client_secret={AppKey}&code={code}&redirect_uri={RedirectUri}";
                    using (HttpClient tokenClient = new HttpClient())
                    {
                        var tokenResult = await tokenClient.GetStringAsync(getTokenUrl);
                        //ViewBag.Result = tokenResult;
                        tokenResult = tokenResult.Replace("callback(", "").Replace(");", "");

                        QcToken qcToken;
                        try
                        {
                            qcToken = JsonHelper.Deserialize<QcToken>(tokenResult);
                        }
                        catch (Exception)
                        {
                            qcToken = new QcToken(tokenResult);
                        }

                        //使用Access Token来获取用户的OpenID
                        var getOpenIdUrl = "https://graph.qq.com/oauth2.0/me?access_token=" + qcToken.access_token;
                        using (HttpClient openIdClient = new HttpClient())
                        {
                            var openIdResult = await openIdClient.GetStringAsync(getOpenIdUrl);
                            openIdResult = openIdResult.Replace("callback(", "").Replace(");", "");
                            var qcOpenId = JsonHelper.Deserialize<QcOpenId>(openIdResult);
                            if (String.IsNullOrWhiteSpace(qcOpenId.error) && String.IsNullOrWhiteSpace(qcOpenId.error_description))
                            {
                                //判断数据库中是否存在此openId帐号
                                var user = _userInfoService.GetQQUser(qcOpenId.openid);
                                if (user != null)
                                {
                                    ViewBag.UserLogin = await _userInfoService.LoginAsync(new Model.Dto.Request.UserSignInRequest
                                    {
                                        Account = user.Account,
                                        Password = EncryptHelper.AESDecrypt(user.Password)
                                    });                                    
                                }
                                else
                                {
                                    using (HttpClient infoClient = new HttpClient())
                                    {
                                        //使用Access Token以及OpenID来访问和修改用户数据
                                        var userInfoUrl = "https://graph.qq.com/user/get_user_info?" + $"oauth_consumer_key={AppId}&access_token={qcToken.access_token}&openid={qcOpenId.openid}";
                                        var infoResult = await infoClient.GetStringAsync(userInfoUrl);
                                        infoResult = infoResult.Replace("callback(", "").Replace(");", "");
                                        var qqUser = JsonHelper.Deserialize<QcUser>(infoResult);
                                        if (String.IsNullOrWhiteSpace(qqUser.error) && String.IsNullOrWhiteSpace(qqUser.error_description) && qqUser.ret == 0)
                                        {
                                            ViewBag.UserLogin = await _userInfoService.CreateAsync(new Model.Dto.Request.UserSignUpRequest
                                            {
                                                Account = _userInfoService.GetRandomAccount("QQ"),
                                                AvatarUrl = await FileHelper.DownAsync(qqUser.figureurl_qq_2.Replace(@"\/", "/"), "jpg"),
                                                Nmae = qqUser.nickname,
                                                Password = StringHelper.RndomStr(8),
                                                QQOpenId = qcOpenId.openid
                                            });
                                        }
                                        else
                                        {
                                            _logger.LogInformation($"访问和修改用户数据 Error: {qcOpenId.error}");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                _logger.LogInformation($"获取用户的OpenID Error: {qcOpenId.error}");
                            }
                        }
                    }
                }
            }

            return View();
        }
    }
}
