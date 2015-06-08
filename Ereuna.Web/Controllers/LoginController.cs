using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using Ereuna.Web.Data;
using Ereuna.Web.Models;
using Newtonsoft.Json;

namespace Ereuna.Web.Controllers
{
    /// <summary>
    /// The Facebook login process is a bit complex. I'll document it here for my own sanity as well.
    /// First, we auth the user via facebook. This could be a login, or they could be returning and we just validate them.
    /// Either way we end up with an access token. We submit this to the server.
    /// We then verify the token via facebook again; this ensures the call to the API was from our app and it is valid for that user id.
    /// We can then create a session around that user id and return a guid token for all future calls going forward (until session ends).
    /// </summary>
    public class LoginController : ApiController
    {
        private readonly EreunaContext _context;

        public LoginController(EreunaContext context)
        {
            _context = context;
        }

        [HttpPost] public IHttpActionResult Post([FromBody] FacebookAccessToken token)
        {
            if (IsTokenValid(token.AccessToken, token.UserId))
            {
                DoLogin(token);
                return Ok();
            }

            return Unauthorized();
        }

        private void DoLogin(FacebookAccessToken token)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.FacebookUserId == token.UserId);

            if (existingUser != null)
            {
                existingUser.LastFacebookToken = existingUser.Token;
                existingUser.Token = token.AccessToken;
            }
            else
            {
                var newUser = new Data.User
                {
                    UserType = _context.UserTypes.FirstOrDefault(x => x.Id == UserType.FacebookUser),
                    FacebookUserId = token.UserId,
                    Email = token.Email,
                    First = token.FirstName,
                    Last = token.LastName,
                    IsEmailVerified = true,
                    Token = token.AccessToken
                };

                _context.Users.Add(newUser);
            }

            _context.SaveChanges();
        }

        private bool IsTokenValid(string accessToken, string userId)
        {
            var apiTokenUrl = string.Format("https://graph.facebook.com/me?access_token={0}", accessToken);

            var request = WebRequest.Create(apiTokenUrl);
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            using (var dataStream = response.GetResponseStream())
            using (var reader = new StreamReader(dataStream))
            {
                var responseString = reader.ReadToEnd();
                var facebookToken = JsonConvert.DeserializeObject<FacebookUserToken>(responseString);
                if (facebookToken.Id == userId && facebookToken.Verified)
                {
                    return true;
                }

            }
            return false;
        }
        

    }
}