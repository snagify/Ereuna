using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Ereuna.Web.Data;

namespace Ereuna.Web.Common.Session
{
    public class TokenAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        public EreunaContext Context { get; set; }

        private UserSession GetUserSession(string sessionToken)
        {
            return Context.UserSessions.FirstOrDefault(x => x.SessionToken == sessionToken);
        }

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

                var userSession = GetUserSession(sessionToken);
                if (userSession == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    actionContext.Response.Content = new StringContent("You are not authorised; please login");

                    // TODO Log here invalid token, also maybe implement invalid token counter and then lockout user

                    return;
                }

                if (userSession.IsSessionOpen == false)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    actionContext.Response.Content = new StringContent("Session has expired; please login");
                    
                    return;
                }

                var user = userSession.User;
                if (user == null)
                {
                    // TODO Log this, something went wrong, user should exist if usersession exists!

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