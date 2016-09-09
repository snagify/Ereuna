using Ereuna.Web.Data;

namespace Ereuna.Web.Common.Session
{
    public interface ISessionProvider
    {
        bool IsSessionActive();
        SessionUser GetSessionUser();
        string GetSessionToken();
        void SetSessionUser(SessionUser user);
    }

    public class SessionUser
    {
        public int Id { get; private set; }

        public string LastFacebookToken { get; private set; }

        public string Token { get; private set; }

        public string First { get; private set; }

        public string Last { get; private set; }

        public string Email { get; private set; }

        public bool IsEmailVerified { get; private set; }

        public SessionUser(int id, string lastFacebookToken, string token, string first, string last, string email, bool isEmailVerified)
        {
            Id = id;
            LastFacebookToken = lastFacebookToken;
            Token = token;
            First = first;
            Last = last;
            Email = email;
            IsEmailVerified = isEmailVerified;
        }

        public SessionUser(User user)
        {
            Id = user.Id;
            LastFacebookToken = user.LastFacebookToken;
            Token = user.Token;
            First = user.First;
            Last = user.Last;
            Email = user.Email;
            IsEmailVerified = user.IsEmailVerified;
        }
    }
}