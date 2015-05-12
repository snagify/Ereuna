using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ereuna.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
                        
            DependencyResolver.SetResolver(new AutofacDependencyResolver(BuildContainer()));
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            // builder.RegisterModule<AutofacWebTypesModule>();  http://docs.autofac.org/en/latest/integration/mvc.html

            // OWIN:  http://docs.autofac.org/en/latest/integration/owin.html

            return builder.Build();
        }


        
    }
}
