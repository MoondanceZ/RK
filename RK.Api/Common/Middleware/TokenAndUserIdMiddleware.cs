using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace RK.Api.Common.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TokenAndUserIdMiddleware
    {
        private readonly RequestDelegate _next;
        private IMemoryCache _cache;
        private readonly ILogger<TokenAndUserIdMiddleware> _logger;

        public TokenAndUserIdMiddleware(RequestDelegate next, IMemoryCache cache, ILogger<TokenAndUserIdMiddleware> logger)
        {
            _next = next;
            _cache = cache;
            _logger = logger;
        }

        public Task Invoke(HttpContext httpContext)
        {
            try
            {
                var userId = string.Empty;
                if(httpContext.Request.Method== "GET")
                {
                    userId = httpContext.Request.Query["UserId"];
                }
                else
                {
                    userId = httpContext.Request.Form["UserId"];
                }
                    
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    var cacheToken = _cache.Get<string>(userId);
                    var curToken = httpContext.Request.Headers["Authorization"].ToString();
                    if (cacheToken != curToken)
                        throw new Exception($"token 不正确，cache token = {cacheToken}, current token = {curToken}, user id = {userId}");
                }
                return _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw new UnauthorizedAccessException();
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TokenAndUserIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenAndUserIdMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenAndUserIdMiddleware>();
        }
    }
}
