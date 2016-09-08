using Ereuna.Web.Data;

namespace Ereuna.Web.Common.Session
{
    public interface ISessionProvider
    {
        bool IsSessionActive();
        User GetSessionUser();
        string GetSessionToken();
        void SetSessionUser(User user);
    }
}