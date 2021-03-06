﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RK.Infrastructure;
using RK.Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RK.Api.Common.Middleware
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly IUserInfoService _userInfoService;
        public TokenProviderMiddleware(RequestDelegate next, IOptions<TokenProviderOptions> options, IUserInfoService userInfoService, IAuthenticationSchemeProvider schemes)
        {
            _next = next;
            _options = options.Value;
            this._userInfoService = userInfoService;
            Schemes = schemes;
        }
        public IAuthenticationSchemeProvider Schemes { get; set; }

        /// <summary>
        /// invoke the middleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            //
            context.Features.Set<IAuthenticationFeature>(new AuthenticationFeature
            {
                OriginalPath = context.Request.Path,
                OriginalPathBase = context.Request.PathBase
            });
            //获取默认Scheme（或者AuthorizeAttribute指定的Scheme）的AuthenticationHandler
            var handlers = context.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
            foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
            {
                var handler = await handlers.GetHandlerAsync(context, scheme.Name) as IAuthenticationRequestHandler;
                if (handler != null && await handler.HandleRequestAsync())
                {
                    return;
                }
            }
            var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate != null)
            {
                var result = await context.AuthenticateAsync(defaultAuthenticate.Name);
                if (result?.Principal != null)
                {
                    context.User = result.Principal;
                }
            }
            //


            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                await _next(context);
                return;
            }
            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            //if (!context.Request.Method.Equals("POST")
            //   || !context.Request.HasFormContentType)
            //{
            //    await ReturnBadRequest(context);
            //    return;
            //}

            await GenerateAuthorizedResult(context);
        }

        /// <summary>
        /// 验证结果并得到token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task GenerateAuthorizedResult(HttpContext context)
        {
            var username = "";
            var password = "";
            if(context.Request.Method.Equals("GET"))
            {
                username = context.Request.Query["username"];
                password = context.Request.Query["password"];
            }
            else
            {
                username = context.Request.Form["username"];
                password = context.Request.Form["password"];
            }

            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                await ReturnBadRequest(context);
                return;
            }

            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(GetJwt(username));
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var isValidated = _userInfoService.AuthUser(username, password);

            if (isValidated)
            {
                return Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(username, "Token"), new Claim[] { }));

            }
            return Task.FromResult<ClaimsIdentity>(null);
        }

        /// <summary>
        /// return the bad request (200)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task ReturnBadRequest(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync(JsonHelper.Serialize(ReturnStatus.Error("认证失败")));
        }

        /// <summary>
        /// get the jwt
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private string GetJwt(string username)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(),
                          ClaimValueTypes.Integer64),
                ////用户名
                //new Claim(ClaimTypes.Name, username),
                ////角色
                //new Claim(ClaimTypes.Role, "a")
            };

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                Status = true,
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds,
                token_type = "Bearer"
            };
            return JsonHelper.Serialize(response);
        }

    }

    public class TokenProviderOptions
    {
        /// <summary>
        /// 请求路径
        /// </summary>
        public string Path { get; set; } = "/Token";

        public string Issuer { get; set; }

        public string Audience { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5000);

        public SigningCredentials SigningCredentials { get; set; }
    }
}
