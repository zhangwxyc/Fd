using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ConsoleApplication1.Startup))]

namespace ConsoleApplication1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use((context, next) =>
                {
                    long bodyLength = context.Request.Body.Length;
                    return next();
                });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync(" Authentication... ");
                await next();
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Authorization... ");
                await next();
            });

            app.Use(async (context, next) =>
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
