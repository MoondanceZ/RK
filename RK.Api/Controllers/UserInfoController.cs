using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RK.Service;
using RK.Model;
using NLog;
using Microsoft.AspNetCore.Authorization;
using RK.Framework.Common;
using RK.Model.Dto.Request;

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
        // GET api/userInfo
        [HttpGet]
        public IEnumerable<UserInfo> Get()
        {           
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

        [HttpPost("Login")]
        public ReturnStatus<UserInfo> Login([FromBody]UserLoginRequest request)
        {
            return _userInfoService.Login(request);
        }
    }
}
