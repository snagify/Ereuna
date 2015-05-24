using System;

namespace Ereuna.Web.Data
{
    public class ApplicationActivity
    {
        public int Id { get; set; } 
        public DateTime Occurred { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}