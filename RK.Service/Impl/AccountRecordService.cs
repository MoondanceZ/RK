﻿using RK.Model;
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
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public ReturnStatus<AccountResponse> Get(int id)
        {
            try
            {
                var record = _repository.DbSet.Include(m => m.AccountType).Where(m => m.Id == id).FirstOrDefault();
                if (record == null)
                    return ReturnStatus<AccountResponse>.Error("该记录不存在");

                return ReturnStatus<AccountResponse>.Success(null, new AccountResponse
                {
                    Id = record.Id,
                    AccountDate = record.AccountDate,
                    AccountTypeId = record.AccountTypeId,
                    Amount = record.Amount,
                    Remark = record.Remark,
                    Type = record.Type,
                    TypeCode = record.AccountType.Code,
                    TypeName = record.AccountType.Name,
                    UserId = record.UserInfoId,
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Query AccountRecord Error");
                return ReturnStatus<AccountResponse>.Error("查询失败");
            }
        }

        public ReturnPage<AccountResponse> GetList(int pageIndex, int pageSize, int userId)
        {
            var records = _repository.DbSet
                .Include(m => m.AccountType)
                .Where(m => m.UserInfoId == userId)
                .OrderByDescending(m => m.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();

            if (records != null && records.Any())
            {
                return ReturnPage<AccountResponse>.Success(pageIndex, pageSize, 0, records.Select(m =>
                {
                    return new AccountResponse
                    {
                        AccountDate = m.AccountDate,
                        AccountTypeId = m.AccountTypeId,
                        Amount = m.Amount,
                        Id = m.Id,
                        Remark = m.Remark,
                        Type = m.Type,
                        TypeCode = m.AccountType.Code,
                        TypeName = m.AccountType.Name,
                        UserId = m.UserInfoId
                    };
                }).ToList());
            }
            return ReturnPage<AccountResponse>.Error(pageIndex, pageSize, "没有更多的记录");
        }

        public ReturnStatus<AccountResponse> Update(int Id, AccountRequest request)
        {
            try
            {
                var record = _repository.Get(m => m.Id == Id);
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