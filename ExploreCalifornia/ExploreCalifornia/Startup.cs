using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseFileServer(); //Use folder wwwroot

            // Register middleware that responds to all calls
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello California Course Example!");
            //});

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("app.Use: Hello World");

                await next();
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.StartsWith("/raquel"))
                {
                    await context.Response.WriteAsync("\r\napp.Use: Hello Raquel");
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
