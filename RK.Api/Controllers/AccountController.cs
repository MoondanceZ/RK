using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RK.Service;
using RK.Model;
using Microsoft.AspNetCore.Authorization;
using RK.Infrastructure;
using RK.Model.Dto.Request;
using RK.Model.Dto.Reponse;

namespace RK.Api.Controllers
{
    //[Authorize]
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

        [HttpGet("Types/{userId}")]
        public ReturnStatus<List<AccountType>> Types(int userId)
        {
            return _accountTypeService.GetAccountRecordTypes(userId);
        }

        [HttpGet("{id}")]
        public ReturnStatus<AccountResponse> Get(int id)
        {
            return _accountRecordService.Get(id);
        }

        [HttpGet("List")]
        public ReturnPage<DateAccountResponse> GetList(AccountPageListRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ReturnPage<DateAccountResponse>.Error(request.PageIndex, request.PageSize, "请求参数有误");
            }
            return _accountRecordService.GetList(request);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ReturnStatus<AccountResponse> Post([FromBody]AccountRequest request)
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
        [HttpPut("{id}")]
        public ReturnStatus Put(int id, [FromBody]AccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ReturnStatus.Error("请求参数有误");
            }
            return _accountRecordService.Update(id, request);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ReturnStatus Delete(int id)
        {
            return _accountRecordService.Delete(id);
        }
    }
}
