using System;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace Ereuna.Web.Common.Api
{
    public class HttpControllerAmmendments
    {
        /// <summary>
        /// Web Api controllers automatically use the word 'Controller' to determine cacheable controllers, however we want it to not use
        /// the word Controller because these are APIs, not MVC apps. This goes with the CustomHttpControllerTypeResolver.
        /// </summary>
        public static void SuffixOverride(string newSuffix)
        {
            if (newSuffix == null)
            {
                throw new ArgumentException();
            }

            var suffix = typeof(DefaultHttpControllerSelector).GetField("ControllerSuffix", BindingFlags.Static | BindingFlags.Public);
            suffix?.SetValue(null, newSuffix);
        } 

    }
}