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
using RK.Framework.Database.UnitOfWork;
using System.Reflection;
using RK.Framework.Common;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;
using RK.Framework.Database;

namespace RK.Web
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
            services.AddMvc();

            ////添加跨域
            //services.AddCors();

            // Add Autofac
            var builder = new ContainerBuilder();
            builder.RegisterType<DatabaseFactory>().As<IDesignTimeDbContextFactory<RkDbContext>>().AsImplementedInterfaces().InstancePerLifetimeScope();
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
