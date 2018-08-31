using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RK.Api.Common.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorWrapping(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorWrappingMiddleware>();
        }
    }
}
