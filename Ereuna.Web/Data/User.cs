﻿namespace Ereuna.Web.Data
{
    public class User
    {
        public virtual UserType UserType { get; set; }

        public int UserId { get; set; } 

        public int FacebookUserId { get; set; }

        public string LastFacebookToken { get; set; }

        public string Token { get; set; }

        public string First { get; set; }

        public string Last { get; set; }
        
        public string Email { get; set; }

        public bool IsEmailVerified { get; set; }

    }
}