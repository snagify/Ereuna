using System;

namespace Ereuna.Web.Data
{
    public class Idea
    {
        public virtual Project Project { get; set; }

        public int Id { get; set; } 

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime WhenAdded { get; set; }

        public int Importance { get; set; } // The higher the better (more important)
    }
}