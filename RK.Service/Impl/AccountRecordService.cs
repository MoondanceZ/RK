using RK.Model;
using RK.Model.Dto.Request;
using RK.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Framework.Database;
using RK.Framework.Common;
using Microsoft.Extensions.Logging;
using NLog;
using RK.Model.Dto.Reponse;

namespace RK.Service.Impl
{
    public class AccountRecordService : BaseService<AccountRecord, IAccountRecordRepository>, IAccountRecordService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public AccountRecordService(IUnitOfWork unitOfWork, IAccountRecordRepository repository) : base(unitOfWork, repository)
        {
        }

        public ReturnStatus<AccountResponse> Create(AccountRequest request)
        {
            try
            {
                var record = new AccountRecord
                {
                    AccountDate = request.AccountDate,
                    AccountTypeId = request.AccountTypeId,
                    Amount = request.Amount,
                    Remark = request.Remark,
                    Status = 1,
                    Type = request.Type,
                    UserInfoId = request.UserId
                };
                _repository.Add(record);
                _unitOfWork.Commit();
                return ReturnStatus<AccountResponse>.Success("添加成功", new AccountResponse
                {
                    Id = record.Id,
                    AccountDate = record.AccountDate,
                    AccountTypeId = record.AccountTypeId,
                    Amount = record.Amount,
                    Remark = record.Remark,
                    Type = record.Type,
                    UserId = record.UserInfoId
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Create AccountRecord Error");
                return ReturnStatus<AccountResponse>.Error("添加失败");
            }
        }

        public ReturnStatus<AccountResponse> Update(UpdateAccountRequest request)
        {
            try
            {
                var record = _repository.Get(m => m.Id == request.Id);
                if (record == null)
                    return ReturnStatus<AccountResponse>.Error("更新失败，记录不存在");
                record.AccountDate = request.AccountDate;
                record.AccountTypeId = request.AccountTypeId;
                record.Amount = request.Amount;
                record.Remark = request.Remark;
                record.Status = 1;
                record.Type = request.Type;
                record.UserInfoId = request.UserId;
                _repository.Update(record);
                _unitOfWork.Commit();

                return ReturnStatus<AccountResponse>.Success("更新成功", new AccountResponse
                {
                    Id = record.Id,
                    AccountDate = record.AccountDate,
                    AccountTypeId = record.AccountTypeId,
                    Amount = record.Amount,
                    Remark = record.Remark,
                    Type = record.Type,
                    UserId = record.UserInfoId
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Update AccountRecord Error");
                return ReturnStatus<AccountResponse>.Error("更新失败");
            }
        }
    }
}
