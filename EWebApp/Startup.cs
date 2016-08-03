
using EntryPoint;
using Microsoft.Owin;
using Owin;
using System.Web;

[assembly: OwinStartupAttribute(typeof(EWebApp.Startup))]
namespace EWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);




            //System.Web.Http.HttpConfiguration httpConfiguration = new System.Web.Http.HttpConfiguration();
            //WebApiConfig.Register(httpConfiguration);
            //try
            //{
            //    HttpMessageHandlerOptions options = WebApiAppBuilderExtensions.CreateOptions(builder, httpServer, configuration);
            //    result = app.UseMessageHandler(options);
            //}
            //catch
            //{
            //    httpServer.Dispose();
            //    throw;
            //}
        }
    }
}
