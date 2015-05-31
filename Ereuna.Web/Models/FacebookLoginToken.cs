using System;

namespace Ereuna.Web.Models
{
    public class FacebookAccessToken
    {

        public string AccessToken { get; set; } 

        public DateTime ExpiresIn { get; set; }

        public string SignedRequest { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}