using System.Web;
using Ereuna.Web.Data;

namespace Ereuna.Web.Common.Session
{
    public class StandardSessionProvider : ISessionProvider
    {
        private static readonly string UserObjectKey = "userObjectKey";

        public bool IsSessionActive()
        {
            if (HttpContext.Current == null) return false;
            if (HttpContext.Current.Session == null) return false;
            return HttpContext.Current.Session[UserObjectKey] != null;
        }

        public User GetSessionUser()
        {
            return (User)HttpContext.Current.Session[UserObjectKey];
        }

        public string GetSessionToken()
        {
            return HttpContext.Current.Session.SessionID;
        }

        public void SetSessionUser(User user)
        {
            HttpContext.Current.Session[UserObjectKey] = user;
        }
    }
}