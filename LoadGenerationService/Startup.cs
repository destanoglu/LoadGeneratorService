using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using LoadGeneratorService.LoadGenerator;
using LoadGeneratorService.Middleware.BasicLogging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoadGeneratorService
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

            var builder = new ContainerBuilder();

            builder.Populate(services);
            builder.RegisterType<PrimeFinder>().As<ILoad>();
            builder.RegisterType<BackgroundLoadExecutor>().As<IBackgroundLoadExecutor>().SingleInstance();
            builder.Register(c => new BacgroundLoadGenerator(c.Resolve<IBackgroundLoadExecutor>()))
                .As<IBackgroundLoadGenerator>();

            var container = builder.Build();


            var loadExecutor = container.Resolve<IBackgroundLoadExecutor>();
            loadExecutor.Start();

            var loadGenerator = container.Resolve<IBackgroundLoadGenerator>();
            loadGenerator.SleepInterval = 500;
            loadGenerator.Start();
            
            return new AutofacServiceProvider(container);
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseLogging();

            app.UseMvc();
        }
    }
}
