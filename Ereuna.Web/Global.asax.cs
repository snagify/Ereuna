using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;

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

            DependencyResolver.SetResolver(new AutofacDependencyResolver(BuildContainer()));
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(EreunaWebApplication).Assembly);
            // builder.RegisterModule<AutofacWebTypesModule>();  http://docs.autofac.org/en/latest/integration/mvc.html

            // OWIN:  http://docs.autofac.org/en/latest/integration/owin.html

            return builder.Build();
        }

    }
}