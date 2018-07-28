using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RK.Infrastructure;
using RK.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RK.Api.Common.Middleware
{
    public class ErrorWrappingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorWrappingMiddleware> _logger;

        public ErrorWrappingMiddleware(RequestDelegate next, ILogger<ErrorWrappingMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(ex.HResult), ex, ex.ToString());

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var errorStr = "操作异常";

            if (exception is UnauthorizedAccessException)
            {
                code = HttpStatusCode.Unauthorized;
                errorStr = exception.Message;
            }
            else if (exception is WeChatException || exception is ApiException)
            {
                errorStr = exception.Message;
            }
            //else if (exception is NotImplementedException)
            //    code = HttpStatusCode.NotImplemented;
            //else if (exception is ArgumentException)
            //    code = HttpStatusCode.BadRequest;

            context.Response.ContentType = "application/json;charset=utf-8";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(JsonHelper.Serialize(ReturnStatus.Error(errorStr)));
        }
    }
}
