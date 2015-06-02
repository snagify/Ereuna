using System.Linq;
using System.Web.Http;
using Ereuna.Web.Data;
using Ereuna.Web.Models;

namespace Ereuna.Web.Controllers
{
    public class LoginController : ApiController
    {
        private readonly EreunaContext _context;

        public LoginController(EreunaContext context)
        {
            _context = context;
        }

        [HttpPost] public IHttpActionResult Post([FromBody] FacebookAccessToken token)
        {
            var existingUser =_context.Users.FirstOrDefault(x => x.FacebookUserId == token.UserId);

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

            return Ok();
        }

        

    }
}