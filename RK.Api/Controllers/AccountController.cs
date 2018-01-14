using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RK.Service;
using RK.Model;
using Microsoft.AspNetCore.Authorization;
using RK.Framework.Common;

namespace RK.Api.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountTypeService _accountRecordTypeService;

        public AccountController(IAccountTypeService accountRecordTypeService)
        {
            _accountRecordTypeService = accountRecordTypeService;
        }

        [HttpGet("Types")]
        public ReturnStatus<List<AccountType>> Types(int userId)
        {
            return ReturnStatus<List<AccountType>>.Success(string.Empty, _accountRecordTypeService.GetAccountRecordTypes(userId).ToList());
        }
    }
}
