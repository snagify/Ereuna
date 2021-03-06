using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Ereuna.Web.Common.Session
{
    public class TokenAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        public ISessionProvider SessionProvider { get; set; }
        
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authorizeHeader = actionContext.Request.Headers.Authorization;
            if (authorizeHeader != null
                && authorizeHeader.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase)
                && string.IsNullOrEmpty(authorizeHeader.Parameter) == false)
            {
                var sessionToken = authorizeHeader.Parameter;

                if (string.IsNullOrWhiteSpace(sessionToken))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    actionContext.Response.Content = new StringContent("You are not authorised; please login");
                    return;
                }

                if (SessionProvider.IsSessionActive() == false)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    actionContext.Response.Content = new StringContent("Session has expired; please login");
                    
                    return;
                }

                var user = SessionProvider.GetSessionUser();
                if (user == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    actionContext.Response.Content = new StringContent("Session has expired; please login");

                    return;
                }

                var role = "user";
                // TODO Implement check for admin here

                var identity = new GenericIdentity(user.Id.ToString());
                identity.AddClaim(new Claim("Name", user.First + ' ' + user.Last));
                identity.AddClaim(new Claim("Id", user.Id.ToString()));
                identity.AddClaim(new Claim("SessionToken", sessionToken));

                var principal = new GenericPrincipal(identity, (new[] { role }));
                Thread.CurrentPrincipal = principal;
                HttpContext.Current.User = principal;

                return;
            }

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

            actionContext.Response.Content = new StringContent("You are not authorised; please login");
        }


    }
}