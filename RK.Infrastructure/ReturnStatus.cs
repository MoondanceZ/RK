using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Infrastructure
{
    public class ReturnStatus
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public static ReturnStatus Error(string message)
        {
            return new ReturnStatus
            {
                IsSuccess = false,
                Message = message
            };
        }

        public static ReturnStatus Success(string message)
        {
            return new ReturnStatus
            {
                IsSuccess = true,
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
                Message = message,
                Data = data
            };
        }

        public new static ReturnStatus<T> Error(string message)
        {
            return new ReturnStatus<T>
            {
                IsSuccess = false,
                Message = message
            };
        }
    }

    public class ReturnPage<T> where T : class, new()
    {
        public bool IsSuccess { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }
        public string Message { get; set; }

        public static ReturnPage<T> Success(int pageIndex, int pageSize, int totalCount, IEnumerable<T> data, string message = null)
        {
            return new ReturnPage<T>
            {
                IsSuccess = true,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = data,
                Message = message
            };
        }

        public static ReturnPage<T> Error(int pageIndex, int pageSize, string errorMessage)
        {
            return new ReturnPage<T>
            {
                IsSuccess = false,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Message = errorMessage
            };
        }
    }
}
