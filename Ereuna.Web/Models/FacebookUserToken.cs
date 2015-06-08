using System;

namespace Ereuna.Web.Models
{
    public class FacebookUserToken
    {
        public string Id { get; set; } 
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Link { get; set; }
        public string Locale { get; set; }
        public string Name { get; set; }
        public string Timezone { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool Verified { get; set; }
    }
}