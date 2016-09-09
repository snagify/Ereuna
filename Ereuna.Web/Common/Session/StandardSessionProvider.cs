using System.Web;

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

        public SessionUser GetSessionUser()
        {
            return (SessionUser)HttpContext.Current.Session[UserObjectKey];
        }

        public string GetSessionToken()
        {
            return HttpContext.Current.Session.SessionID;
        }

        public void SetSessionUser(SessionUser user)
        {
            HttpContext.Current.Session[UserObjectKey] = user;
        }
    }
}