using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Framework.Common
{
    public class ReturnStatus
    {
        public bool IsSuccess { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }

        public static ReturnStatus Error(string message, string errorCode = null)
        {
            return new ReturnStatus
            {
                IsSuccess = false,
                ErrorCode = errorCode,
                Message = message
            };
        }

        public static ReturnStatus Success(string message)
        {
            return new ReturnStatus
            {
                IsSuccess = true,
                ErrorCode = null,
                Message = message,
            };
        }
    }
    public class ReturnStatus<T> : ReturnStatus where T : class, new()
    {
        public T Data { get; set; }
        public static ReturnStatus<T> Success(string message, T data)
        {
            return new ReturnStatus<T>
            {
                IsSuccess = true,
                ErrorCode = null,
                Message = message,
                Data = data
            };
        }

        public new static ReturnStatus<T> Error(string message, string errorCode = null)
        {
            return new ReturnStatus<T>
            {
                IsSuccess = false,
                Message = message,
                ErrorCode = errorCode
            };
        }
    }

    public class ReturnPage<T> where T : class, new()
    {
        public bool Status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public T Data { get; set; }

        public static ReturnPage<T> Success(int pageIndex, int pageSize, int totalCount, T data)
        {
            return new ReturnPage<T>
            {
                Status = true,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = data
            };
        }
    }
}
