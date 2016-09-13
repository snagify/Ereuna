using System.Collections.Generic;

namespace Ereuna.Web.Data
{
    public class User
    {
        public virtual UserType UserType { get; set; }

        public virtual List<UserSession> UserSessions { get; set; }

        public virtual List<Project> Projects { get; set; }

        public int Id { get; set; } 

        public string FacebookUserId { get; set; }

        public string LastFacebookToken { get; set; }

        public string Token { get; set; }

        public string First { get; set; }

        public string Last { get; set; }
        
        public string Email { get; set; }

        public bool IsEmailVerified { get; set; }

    }

}