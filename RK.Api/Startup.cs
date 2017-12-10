using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac;
using System.Reflection;
using RK.Framework.Common;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;
using RK.Framework.Database;
using NLog.Extensions.Logging;
using NLog.Web;
using RK.Api.Common.Middleware;
using RK.Framework.Database.Impl;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using RK.Api.Common.OAuth2;

namespace RK.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //RSA：证书长度2048以上，否则抛异常
            //配置AccessToken的加密证书
            var rsa = new RSACryptoServiceProvider();
            //从配置文件获取加密证书
            rsa.ImportCspBlob(Convert.FromBase64String(Configuration["SigningCredential"]));
            //IdentityServer4授权服务配置
            services.AddIdentityServer()
                .AddSigningCredential(new RsaSecurityKey(rsa))  //设置加密证书
                
                //.AddTemporarySigningCredential()    //测试的时候可使用临时的证书
                .AddInMemoryScopes(OAuth2Config.GetScopes())
                .AddInMemoryClients(OAuth2Config.GetClients())

                //如果是client credentials模式那么就不需要设置验证User了
                .AddResourceOwnerValidator<MyUserValidator>() //User验证接口
                                                              //.AddInMemoryUsers(OAuth2Config.GetUsers())    //将固定的Users加入到内存中
                ;

            services.AddMvc(options =>
            {
                //options.Filters.Add<HttpGlobalExceptionFilter>();
            });

            ////添加跨域
            //services.AddCors();

            // Add Autofac
            #region  Add Autofac
            var builder = new ContainerBuilder();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().AsImplementedInterfaces().InstancePerLifetimeScope();

            var repos = Assembly.Load("RK.Repository");
            builder.RegisterAssemblyTypes(repos, repos)
                    .Where(t => t.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces().InstancePerLifetimeScope();

            var srvs = Assembly.Load("RK.Service");
            builder.RegisterAssemblyTypes(srvs, srvs)
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Populate(services);

            var container = builder.Build();

            IocContainer.SetContainer(container);

            return new AutofacServiceProvider(container);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(TokenProviderMiddleware));
            app.UseMiddleware(typeof(ErrorWrappingMiddleware));
            app.UseMvc();

            loggerFactory.AddNLog();  //添加Nlog
            env.ConfigureNLog("NLog.config");
        }
    }
}
