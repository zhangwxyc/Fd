using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Mvc;
using Autofac.Integration.Mvc;

namespace WebApplication4
{
    public  class WebApp
    {
        public static void Run()
        {
            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            // Configure the container  
            //builder.ConfigureWebApi(configuration);
            // Register API controllers using assembly scanning. 
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterType<DefaultCommandBus>().As<ICommandBus>()
            //    .InstancePerApiRequest();
            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>()
            //    .InstancePerApiRequest();
            //builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>()
            //    .InstancePerApiRequest();
            //builder.RegisterAssemblyTypes(typeof(CategoryRepository)
            //    .Assembly).Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces().InstancePerApiRequest();
            //var services = Assembly.Load("EFMVC.Domain");
            //builder.RegisterAssemblyTypes(services)
            //    .AsClosedTypesOf(typeof(ICommandHandler<>))
            //    .InstancePerApiRequest();
            //builder.RegisterAssemblyTypes(services)
            //    .AsClosedTypesOf(typeof(IValidationHandler<>))
            //    .InstancePerApiRequest();
            var container = builder.Build();
            // Set the WebApi dependency resolver.  
            //var resolver = new AutofacWebApiDependencyResolver(container);
            //configuration.DependencyResolver.(resolver);
            DependencyResolver.SetResolver(new AutofacWebApiDependencyResolver(container));
        }
    }
}