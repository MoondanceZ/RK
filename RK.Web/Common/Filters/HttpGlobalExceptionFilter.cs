using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RK.Web.Common.Filters
{
    public class HttpGlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        public HttpGlobalExceptionFilter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            return Task.Run(() =>
            {
                //var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);
                var logger = LogManager.GetCurrentClassLogger();
                logger.Error(context.Exception, context.Exception.Message);
                //写入日志
                //logger.LogError(context.Exception, context.Exception.StackTrace);

                var response = context.HttpContext.Response;
                if (context.Exception is UnauthorizedAccessException)
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                else if (context.Exception is NotImplementedException)
                    response.StatusCode = (int)HttpStatusCode.NotImplemented;
                else if (context.Exception is ArgumentException)
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                else
                    response.StatusCode = (int)HttpStatusCode.BadRequest;

                context.Result = new ObjectResult("异常出现啦");
                context.ExceptionHandled = true;
            });
        }
    }
}
