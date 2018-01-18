using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using RK.IdentityServer4.OAuth2;
using RK.Infrastructure;
using Autofac;
using RK.Framework.Database.Impl;
using System.Reflection;
using RK.Framework.Database;
using Autofac.Extensions.DependencyInjection;

namespace RK.IdentityServer4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region 跨域
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(Configuration["CorsApi"])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            #endregion

            services.AddMvc();
            //RSA：证书长度2048以上，否则抛异常
            //配置AccessToken的加密证书
            var rsa = new RSACryptoServiceProvider();
            //从配置文件获取加密证书
            rsa.ImportCspBlob(Convert.FromBase64String(Configuration["SigningCredential"]));
            //IdentityServer4授权服务配置
            services.AddIdentityServer()
                .AddSigningCredential(new RsaSecurityKey(rsa))  //设置加密证书

                //.AddTemporarySigningCredential()    //测试的时候可使用临时的证书
                .AddInMemoryIdentityResources(OAuth2Config.GetIdentityResources())
                .AddInMemoryApiResources(OAuth2Config.GetApiResources())
                .AddInMemoryClients(OAuth2Config.GetClients())
                .AddTestUsers(OAuth2Config.TestUsers().ToList())

                //如果是client credentials模式那么就不需要设置验证User了
                .AddResourceOwnerValidator<UserInfoValidator>();

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //跨域访问
            app.UseCors("default");

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }
    }
}
