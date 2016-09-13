using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Ereuna.Web.Data;

namespace Ereuna.Web.Common.Api
{
    public class SecureApiEndpoint : ApiEndpoint
    {
        internal readonly EreunaContext _context;

        internal SecureApiEndpoint(EreunaContext context)
        {
            _context = context;
        }

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

        internal Project GetProject(int id)
        {
            var project = _context.Projects.FirstOrDefault(x => x.Id == id && x.User.Id == UserId);
            return project;
        }
    }
}