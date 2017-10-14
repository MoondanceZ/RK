using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RK.Service;
using RK.Framework.Database.UnitOfWork;
using RK.Model;

namespace RK.Web.Controllers
{
    [Route("api/[controller]")]
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
            return new List<UserInfo>
            {
                new UserInfo
                {
                    Account = "Sb",
                    Password ="123456"
                }
            };
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
