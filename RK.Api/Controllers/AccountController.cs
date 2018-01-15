using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RK.Service;
using RK.Model;
using Microsoft.AspNetCore.Authorization;
using RK.Framework.Common;
using RK.Model.Dto.Request;
using RK.Model.Dto.Reponse;

namespace RK.Api.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountTypeService _accountTypeService;
        private readonly IAccountRecordService _accountRecordService;

        public AccountController(IAccountTypeService accountRecordTypeService, IAccountRecordService accountRecordService)
        {
            _accountTypeService = accountRecordTypeService;
            _accountRecordService = accountRecordService;
        }

        [HttpGet("Types")]
        public ReturnStatus<List<AccountType>> Types(int userId)
        {
            return ReturnStatus<List<AccountType>>.Success(string.Empty, _accountTypeService.GetAccountRecordTypes(userId).ToList());
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReturnStatus<AccountResponse> Post(AccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ReturnStatus<AccountResponse>.Error("请求参数有误");
            }
            return _accountRecordService.Create(request);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public ReturnStatus<AccountResponse> Put(UpdateAccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ReturnStatus<AccountResponse>.Error("请求参数有误");
            }
            return _accountRecordService.Update(request);
        }
    }
}
