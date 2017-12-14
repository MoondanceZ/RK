using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using RK.Api.Common.Middleware;
using RK.Framework.Common;
using RK.Framework.Database;
using RK.Framework.Database.Impl;
using System;
using System.Linq;
using System.Reflection;

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
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:13381";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "rk";
                });
            services.AddMvc();

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
            
            app.UseAuthentication();

            //app.UseMiddleware(typeof(TokenProviderMiddleware));
            app.UseMiddleware(typeof(ErrorWrappingMiddleware));
            app.UseMvc();
            app.UseMvcWithDefaultRoute();

            loggerFactory.AddNLog();  //添加Nlog
            env.ConfigureNLog("NLog.config");
        }
    }
}
