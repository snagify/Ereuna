using System.Data.Entity;

namespace Ereuna.Web.Data
{
    public class EreunaContext : DbContext
    {
        public EreunaContext() : base("Ereuna") { }

        public DbSet<User> Users { get; set; }

        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<UserSession> UserSessions { get; set; }

        public DbSet<ApplicationActivity> ApplicationActivities { get; set; }

        public DbSet<ProjectType> ProjectTypes { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Idea> Ideas { get; set; }

        public DbSet<Character> Characters{ get; set; }

        public DbSet<CharacterType> CharacterTypes { get; set; }
    }
}