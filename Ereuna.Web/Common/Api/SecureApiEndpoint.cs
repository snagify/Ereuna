using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace Ereuna.Web.Common.Api
{
    public class SecureApiEndpoint : ApiEndpoint
    {
        protected int UserId
        {
            get
            {
                var identity = ((GenericIdentity)(HttpContext.Current.User.Identity));
                if (identity == null) throw new Exception("Not authorized");
                var idClaim = identity.Claims.FirstOrDefault(x => x.Type == "Id");
                if (idClaim == null) throw new Exception("Not authorized");
                var id = idClaim.Value;
                return int.Parse(id);
            }
        }
    }
}