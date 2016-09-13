using System;
using System.Collections.Generic;

namespace Ereuna.Web.Data
{
    public class Project
    {
        public virtual ProjectType ProjectType { get; set; }
        
        public virtual User User { get; set; }
        //public int UserId { get; set; }

        public int Id { get; set; }
        public DateTime LastUsed { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public virtual List<Idea> Ideas { get; set; }
    }
}