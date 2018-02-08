using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RK.Api.Common.Filters
{
    public class TokenAndUserIdValidateFilterAttribute : TypeFilterAttribute
    {
        public TokenAndUserIdValidateFilterAttribute() : base(typeof(TokenAndUserIdValidateFilter))
        {
        }

        private class TokenAndUserIdValidateFilter : IActionFilter
        {
            private IMemoryCache _cache;
            public TokenAndUserIdValidateFilter(IMemoryCache cache)
            {
                _cache = cache;
            }
            public void OnActionExecuted(ActionExecutedContext context)
            {

            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var httpContext = context.HttpContext;
                string userIdStr;
                if (httpContext.Request.Method == "GET")
                {
                    userIdStr = httpContext.Request.Query["UserId"];
                }
                else
                {
                    userIdStr = httpContext.Request.Form["UserId"];
                }
                
                if(int.TryParse(userIdStr, out int userId))
                {
                    var cacheToken = _cache.Get<string>(userId);
                    var curToken = httpContext.Request.Headers["Authorization"].ToString();
                    if (cacheToken != curToken)
                        throw new UnauthorizedAccessException($"token 不正确，cache token = {cacheToken}, current token = {curToken}, user id = {userIdStr}");
                }
            }
        }
    }
}
