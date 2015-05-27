using System;

namespace Ereuna.Web.Models
{
    public class FacebookAccessToken
    {

        public string AccessToken { get; set; } 

        public DateTime ExpiresIn { get; set; }

        public string SignedRequest { get; set; }

        public string UserId { get; set; }
    }
}