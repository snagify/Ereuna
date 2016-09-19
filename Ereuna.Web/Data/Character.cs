using System;

namespace Ereuna.Web.Data
{
    public class Character
    {
        public virtual Project Project { get; set; }
        public virtual CharacterType CharacterType { get; set; }

        public int Id { get; set; } 

        public string Name { get; set; }

        public string Description { get; set; }

        
        public DateTime WhenAdded { get; set; }

        public DateTime WhenUpdated { get; set; }

    }
}