using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RK.Service;
using RK.Model;
using NLog;
using Microsoft.AspNetCore.Authorization;

namespace RK.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserInfoController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }
        // GET api/userInfo
        [HttpGet]
        public IEnumerable<UserInfo> Get()
        {
            //Logger.Info("普通信息日志-----------");
            //Logger.Debug("调试日志-----------");
            //Logger.Error("错误日志-----------");
            //Logger.Fatal("异常日志-----------");
            //Logger.Warn("警告日志-----------");
            //Logger.Trace("跟踪日志-----------");
            //Logger.Log(NLog.LogLevel.Warn, "Log日志------------------");
            //throw new Exception("错误");
            //int a = 0;
            //var b = 100 / a;
            //_userInfoService.AddUserInfo(new UserInfo
            //{
            //    Account = "12333"
            //});
            return _userInfoService.ListAll();
        }

        // GET api/userInfo/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/userInfo
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/userInfo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/userInfo/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
