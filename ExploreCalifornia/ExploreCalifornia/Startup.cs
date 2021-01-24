using ExploreCalifornia.Injections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton Singletons are used for application-wide shared values or classes that are too expensive resource-wise
            services.AddSingleton<Settings>(x => new Settings
            {
                DeveloperExceptions = configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions")
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Settings settings)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else if (env.IsProduction())
            //{ 
            //    app.UseExceptionHandler("/error.html");
            //}

            //bool potato = configuration.GetValue<bool>("Environment:Development");
            if (configuration.GetValue<bool>("Environment:Development")) // configuration["EnableDeveloperExceptions"] == "True"
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error.html");

            }

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("ERROR");
                await next();
            });

            app.UseFileServer(); //Use folder wwwroot

            // Register middleware that responds to all calls
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello California Course Example!");
            //});


            //app.use(async (context, next) =>
            //{
            //    await context.response.writeasync("app.use: hello world");

            //    await next();
            //});

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.StartsWith("/raquel"))
                {
                    await context.Response.WriteAsync("\r\napp.Use: Hello Cookie");
                }
                await next();
            });

            app.Run(async (context) =>
            {
                if (context.Request.Path.Value.StartsWith("/california"))
                {
                    await context.Response.WriteAsync("\r\nHello California Course Example!");
                }
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello California Course Example!");
            //    });
            //});
        }
    }
}
