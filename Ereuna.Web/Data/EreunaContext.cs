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
    }
}