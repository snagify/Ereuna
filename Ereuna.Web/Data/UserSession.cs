using System;
using System.ComponentModel.DataAnnotations;

namespace Ereuna.Web.Data
{
    public class UserSession
    {
        public virtual User User { get; set; }

        [Key]
        public int Id { get; set; }
        public string SessionToken { get; set; }
        public DateTime SessionStarted { get; set; }
        public bool IsSessionOpen { get; set; }
    }
}
