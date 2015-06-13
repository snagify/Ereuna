using System;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Ereuna.Web.Common.Api;
using Ereuna.Web.Modules;

namespace Ereuna.Web
{
    public class EreunaWebApplication : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BuildContainer();
        }

        private bool IsValidApiEndpoint(Type t)
        {
            return 
                typeof (IHttpController).IsAssignableFrom(t) && 
                typeof (ApiEndpoint) != t &&
                typeof (SecureApiEndpoint) != t;
        }


        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterControllers(typeof(EreunaWebApplication).Assembly);

            var config = GlobalConfiguration.Configuration;

            //Cannot use the in-built builder.RegisterApiControllers because it has a hardcoded check for "Controllers" at end of files
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(IsValidApiEndpoint);
            
            builder.RegisterWebApiFilterProvider(config);


            builder.RegisterModule(new DataModule());

            // builder.RegisterModule<AutofacWebTypesModule>();  http://docs.autofac.org/en/latest/integration/mvc.html

            // OWIN:  http://docs.autofac.org/en/latest/integration/owin.html

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }

    }
}