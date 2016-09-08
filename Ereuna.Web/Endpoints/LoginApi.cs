using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Ereuna.Web.Common;
using Ereuna.Web.Common.Api;
using Ereuna.Web.Common.Session;
using Ereuna.Web.Data;
using Ereuna.Web.Models;
using Newtonsoft.Json;

namespace Ereuna.Web.Endpoints
{
    /// <summary>
    /// The Facebook login process is a bit complex. I'll document it here for my own sanity as well.
    /// First, we auth the user via facebook. This could be a login, or they could be returning and we just validate them.
    /// Either way we end up with an access token. We submit this to the server.
    /// We then verify the token via facebook again; this ensures the call to the API was from our app and it is valid for that user id.
    /// We can then create a session around that user id and return a guid token for all future calls going forward (until session ends).
    /// </summary>
    public class LoginApi : ApiEndpoint
    {
        private readonly EreunaContext _context;
        private readonly ISessionProvider _sessionProvider;
        private readonly IFacebookApi _facebookApi;

        public LoginApi(EreunaContext context, ISessionProvider sessionProvider, IFacebookApi facebookApi)
        {
            _context = context;
            _sessionProvider = sessionProvider;
            _facebookApi = facebookApi;
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] FacebookAccessToken token)
        {
            if (_facebookApi.IsFacebookUserTokenValid(token.AccessToken, token.UserId))
            {
                var sessionToken = DoLogin(token);
                return Ok(sessionToken);
            }

            return Unauthorized();
        }

        private string DoLogin(FacebookAccessToken token)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.FacebookUserId == token.UserId);

            if (existingUser != null)
            {
                existingUser.LastFacebookToken = existingUser.Token;
                existingUser.Token = token.AccessToken;
            }
            else
            {
                existingUser = new User
                {
                    UserType = _context.UserTypes.FirstOrDefault(x => x.Id == UserType.FacebookUser),
                    FacebookUserId = token.UserId,
                    Email = token.Email,
                    First = token.FirstName,
                    Last = token.LastName,
                    IsEmailVerified = true,
                    Token = token.AccessToken
                };

                _context.Users.Add(existingUser);
            }

            _sessionProvider.SetSessionUser(existingUser);

            var userSession = new UserSession
            {
                IsSessionOpen = true,
                SessionStarted = DateTime.Now,
                SessionToken = _sessionProvider.GetSessionToken(),
                User = existingUser
            };
            _context.UserSessions.Add(userSession);

            _context.SaveChanges();

            return _sessionProvider.GetSessionToken();
        }


    }



}