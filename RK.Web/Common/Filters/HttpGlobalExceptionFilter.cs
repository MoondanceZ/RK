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
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public Task OnExceptionAsync(ExceptionContext context)
        {
            return Task.Run(() =>
            {
                //写入日志
                Logger.Error(new EventId(context.Exception.HResult));

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
