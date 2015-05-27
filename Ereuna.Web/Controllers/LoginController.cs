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
            //_context.Users.FirstOrDefault(x => x.FacebookUserId == token.UserId);


            //_context.Users.Where(x => x.UserId)

            return Ok();
        }

    }
}