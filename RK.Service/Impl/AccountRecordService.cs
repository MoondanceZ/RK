using RK.Model;
using RK.Model.Dto.Request;
using RK.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using RK.Framework.Database;
using RK.Infrastructure;
using Microsoft.Extensions.Logging;
using NLog;
using RK.Model.Dto.Reponse;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using RK.Infrastructure.Exceptions;

namespace RK.Service.Impl
{
    public class AccountRecordService : BaseService<AccountRecord, IAccountRecordRepository>, IAccountRecordService
    {
        private readonly ILogger<AccountRecordService> _logger;
        public AccountRecordService(IUnitOfWork unitOfWork, IAccountRecordRepository repository, IHttpContextAccessor httpConetext, ILogger<AccountRecordService> logger) : base(unitOfWork, repository, httpConetext)
        {
            _logger = logger;
        }

        public ReturnStatus<AccountResponse> Create(AccountRequest request)
        {
            CheckCurrentUserValid(request.UserId);
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
                    Amount = record.Amount.ToString("F2"),
                    Remark = record.Remark,
                    Type = record.Type,
                    UserId = record.UserInfoId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Create AccountRecord Error: {ex}");
                throw new ApiException("添加失败");
            }
        }

        public ReturnStatus<AccountResponse> Get(int id)
        {
            try
            {
                var record = _repository.GetAllLazy().Include(m => m.AccountType).Where(m => m.Id == id).FirstOrDefault();
                if (record == null)
                    throw new ApiException("该记录不存在");
                CheckCurrentUserValid(record.UserInfoId);

                return ReturnStatus<AccountResponse>.Success(null, new AccountResponse
                {
                    Id = record.Id,
                    AccountDate = record.AccountDate,
                    AccountTypeId = record.AccountTypeId,
                    Amount = record.Amount.ToString("F2"),
                    Remark = record.Remark,
                    Type = record.Type,
                    TypeCode = record.AccountType.Code,
                    TypeName = record.AccountType.Name,
                    UserId = record.UserInfoId,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Query AccountRecord Error: {ex}");
                throw new ApiException(ex.Message);
            }
        }

        public ReturnPage<DateAccountResponse> GetList(AccountPageListRequest request)
        {
            CheckCurrentUserValid(request.UserId);

            var records = _repository.GetAllLazy()
                .Include(m => m.AccountType)
                .Where(m => m.UserInfoId == request.UserId)
                .OrderByDescending(m => m.AccountDate).ThenByDescending(m => m.Id)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).ToList();

            if (records != null && records.Any())
            {
                var groupData = records.GroupBy(m => m.AccountDate);
                var dateList = groupData.Select(m => m.Key).ToList();
                var monthRecords = _repository.GetMany(m => m.UserInfoId == request.UserId && Convert.ToDateTime(m.AccountDate) >= DateTime.Now.AddDays(1 - DateTime.Now.Day).Date && Convert.ToDateTime(m.AccountDate) < DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).Date).Select(m => new
                {
                    m.UserInfoId,
                    m.Type,
                    m.Amount
                });

                var dateRecords = _repository.GetMany(m => m.UserInfoId == request.UserId && dateList.Contains(m.AccountDate));
                List<DateAccountResponse> listAccountResponseDate = new List<DateAccountResponse>();
                foreach (var item in groupData)
                {
                    listAccountResponseDate.Add(new DateAccountResponse
                    {
                        Date = item.Key,
                        DateAmount = dateRecords.Where(m => m.AccountDate == item.Key && m.Type == 1).Sum(m => m.Amount).ToString("F2"),
                        MonthExpend = monthRecords.Where(m => m.Type == 1).Sum(m => m.Amount).ToString("F2"),
                        MonthIncome = monthRecords.Where(m => m.Type == 0).Sum(m => m.Amount).ToString("F2"),
                        AccountRecords = item.Select(m =>
                        {
                            return new AccountResponse
                            {
                                AccountDate = m.AccountDate,
                                AccountTypeId = m.AccountTypeId,
                                Amount = m.Amount.ToString("F2"),
                                Id = m.Id,
                                Remark = m.Remark,
                                Type = m.Type,
                                TypeCode = m.AccountType.Code,
                                TypeName = m.AccountType.Name,
                                UserId = m.UserInfoId
                            };
                        }).ToList()
                    });
                }
                return ReturnPage<DateAccountResponse>.Success(request.PageIndex, request.PageSize, 0, listAccountResponseDate);
            }
            return ReturnPage<DateAccountResponse>.Success(request.PageIndex, request.PageSize, 0, new List<DateAccountResponse>(), "没有更多的记录啦");
        }

        public ReturnStatus Update(int id, AccountRequest request)
        {
            CheckCurrentUserValid(request.UserId);

            var record = _repository.Get(m => m.Id == id && m.UserInfoId == request.UserId);
            if (record == null)
                throw new ApiException("更新失败，记录不存在");
            try
            {
                record.AccountDate = request.AccountDate;
                record.AccountTypeId = request.AccountTypeId;
                record.Amount = request.Amount;
                record.Remark = request.Remark;
                record.Status = 1;
                record.Type = request.Type;
                record.UserInfoId = request.UserId;
                _repository.Update(record);
                _unitOfWork.Commit();

                return ReturnStatus.Success("更新成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Update AccountRecord Error: {ex}");
                throw new ApiException("更新失败");
            }
        }

        public ReturnStatus Delete(int id)
        {

            var record = _repository.Get(m => m.Id == id);
            if (record == null)
                throw new ApiException("删除失败，记录不存在");
            CheckCurrentUserValid(record.UserInfoId);
            try
            {
                _repository.Delete(record);
                _unitOfWork.Commit();
                return ReturnStatus.Success("删除成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Delete AccountRecord Error: {ex}");
                throw new ApiException("删除失败");
            }
        }

        public ReturnStatus<AccountMonthInfoResponse> GetInfo(int userId)
        {
            CheckCurrentUserValid(userId);
            var startDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            var endDate = DateTime.Now.AddDays(1).Date;
            var lastMonth = DateTime.Now.AddMonths(-1);
            var lastMonthStartDate = lastMonth.AddDays(1 - lastMonth.Day).Date;
            var lastMonthEndDate = lastMonth.AddDays(1).Date;

            //本月收入和支出
            var sumIncome = _repository.GetSumIncome(userId, startDate, endDate);
            var sumExpend = _repository.GetSumExpend(userId, startDate, endDate);

            //上月收入和支出
            var sumLastMonthIncome = _repository.GetSumIncome(userId, lastMonthStartDate, lastMonthEndDate);
            var sumLastMonthExpend = _repository.GetSumExpend(userId, lastMonthStartDate, lastMonthEndDate);

            //上月总支出
            var totalLastMonthExpend = _repository.GetSumExpend(userId, lastMonthStartDate, startDate);

            var lastMonthTop3Expend = _repository.GetLastMonthTop3Expend(userId, lastMonthStartDate, lastMonthEndDate).ToList();
            var listLastMonthTop3Expend = new List<AccountMonthInfoResponse.LastMonthExpendResponse>();
            for (int i = 0; i < 3; i++)
            {
                if (lastMonthTop3Expend != null && lastMonthTop3Expend.Count >= i + 1)
                {
                    var curTempExpend = lastMonthTop3Expend[i];
                    var expendAmount = curTempExpend.Amount;
                    listLastMonthTop3Expend.Add(new AccountMonthInfoResponse.LastMonthExpendResponse
                    {
                        Expend = expendAmount.ToString("F2"),
                        ExpendPercent = (expendAmount / totalLastMonthExpend * 100).ToString("F2"),
                        TypeCode = curTempExpend.AccountType.Code,
                        TypeName = curTempExpend.AccountType.Name
                    });
                }
                else
                    listLastMonthTop3Expend.Add(null);
            }

            return ReturnStatus<AccountMonthInfoResponse>.Success(null, new AccountMonthInfoResponse
            {
                StartDate = startDate.ToString("MM月dd日"),
                EndDate = DateTime.Now.ToString("MM月dd日"),
                Expend = sumExpend.ToString("F2"),
                Income = sumIncome.ToString("F2"),
                AvgExpendPerDay = (sumExpend/DateTime.Now.Day).ToString("F2"),
                LastMonthExpend = sumLastMonthExpend.ToString("F2"),
                LastMonthTop3Expend = listLastMonthTop3Expend
            });
        }
    }
}
