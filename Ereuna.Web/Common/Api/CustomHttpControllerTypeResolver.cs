using System;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Ereuna.Web.Common.Api
{
    /// <summary>
    /// Used to replace the checks that enforce a web api endpoint to have the word 'Controller' in it.
    /// Instead requires 'Api' and my own ApiEndpoint base.
    /// Followed: http://www.strathweb.com/2013/02/but-i-dont-want-to-call-web-api-controllers-controller/
    /// </summary>
    public class CustomHttpControllerTypeResolver : DefaultHttpControllerTypeResolver
    {
        public CustomHttpControllerTypeResolver() : base(IsApiEndpoint) { }


        internal static readonly Type ApiEndpointType = typeof(ApiEndpoint);
        internal static readonly Type HttpControllerType = typeof(IHttpController);

        internal static bool IsApiEndpoint(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var name = type.Name;
            var ass = type.Assembly.FullName;


            var result =
                type.IsClass &&
                type.IsVisible &&
                !type.IsAbstract &&
                ApiEndpointType.IsAssignableFrom(type) &&
                HttpControllerType.IsAssignableFrom(type);

            return result;
        }

    }
}