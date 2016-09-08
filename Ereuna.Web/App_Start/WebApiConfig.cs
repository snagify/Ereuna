using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using Ereuna.Web.Common.Api;

namespace Ereuna.Web
{
    public static class WebApiConfig
    {
        public static string UrlPrefix => "api";
        public static string UrlPrefixRelative => "~/" + UrlPrefix;

        public static void Register(HttpConfiguration config)
        {
            config.EnableSystemDiagnosticsTracing();

            // Web API configuration and services
            config.Services.Replace(typeof(IHttpControllerTypeResolver), new CustomHttpControllerTypeResolver());
            HttpControllerAmmendments.SuffixOverride("Api");

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute("DefaultApiAction", UrlPrefix + "/{controller}/{action}");
            config.Routes.MapHttpRoute("DefaultApiId", UrlPrefix + "/{controller}/{id}", new { id = RouteParameter.Optional });
            //config.Routes.MapHttpRoute("DefaultApiActionId", UrlPrefix + "/{controller}/{action}/{id}");
            //config.Routes.MapHttpRoute("DefaultApiGet", UrlPrefix + "/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            //config.Routes.MapHttpRoute("DefaultApiPost", UrlPrefix + "/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

        }
    }
}
