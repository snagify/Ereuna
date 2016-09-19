using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Ereuna.Web.Data;

// Migration commands from package manager:
// Add-Migration
// Update-Database

namespace Ereuna.Web.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EreunaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EreunaContext context)
        {
            //  This method will be called after migrating to the latest version.

            var activities = new List<ApplicationActivity>
            {
                new ApplicationActivity { Id = 1, Occurred = DateTime.Now.AddDays(-2), Title="Added login for Facebook", Description = "Yeah totally got FB working"},
                new ApplicationActivity { Id = 2, Occurred = DateTime.Now.AddDays(-1).AddMinutes(-10), Title = "Started playing around with Bootstrap", Description = "See title"},
                new ApplicationActivity { Id = 3, Occurred = DateTime.Now.AddDays(-1), Title = "Also started with EF", Description = "This should be seeded"}
            };
            context.ApplicationActivities.AddOrUpdate(activities.ToArray());
            
            var userTypes = new List<UserType>
            {
                new UserType { Id = 1, Title = "Facebook User" },
                new UserType { Id = 2, Title = "Email User" }
            };
            context.UserTypes.AddOrUpdate(userTypes.ToArray());

            var projectTypes = new List<ProjectType>
            {
                new ProjectType { Id = 1, Title = "Book or Series" },
                new ProjectType { Id = 2, Title = "Script (Movie or Theatre)" },
                new ProjectType { Id = 3, Title = "Comic" }
            };
            context.ProjectTypes.AddOrUpdate(projectTypes.ToArray());

            var characterTypes = new List<CharacterType>
            {
                new CharacterType { Id = CharacterType.Protagonist, Name = "Protagonist" },
                new CharacterType { Id = CharacterType.Antagonist, Name = "Antagonist" },
                new CharacterType { Id = CharacterType.Other, Name = "Other" }
            };
            context.CharacterTypes.AddOrUpdate(characterTypes.ToArray());


            context.SaveChanges();
        }
    }
}
