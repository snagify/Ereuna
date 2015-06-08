using System;

namespace Ereuna.Web.Models
{
    public class ProjectSummary
    {
        public int Id { get; set; } 
        public DateTime LastUsed { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Type { get; set; }
    }
}