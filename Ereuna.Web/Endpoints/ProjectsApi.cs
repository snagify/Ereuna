using System;
using System.Collections.Generic;
using Ereuna.Web.Common.Api;
using Ereuna.Web.Common.Session;
using Ereuna.Web.Data;
using Ereuna.Web.Models;

namespace Ereuna.Web.Endpoints
{
    [TokenAuthorizationFilter]
    public class ProjectsApi : SecureApiEndpoint
    {
        private readonly EreunaContext _context;

        public ProjectsApi(EreunaContext context)
        {
            _context = context;
        }

        [TokenAuthorizationFilter]
        public IEnumerable<ProjectSummary> GetAllProjectSummaries()
        {
            var id = UserId;

            return new List<ProjectSummary>
            {
                new ProjectSummary { Id= 1, Name= "Dancing with the Dead", Type= "Books", LastUsed = DateTime.Now.AddDays(-2).AddHours(-4).AddMinutes(14), Description = "Some desc"},
                new ProjectSummary { Id= 2, Name= "Salmon Man", Type= "Comics", LastUsed = DateTime.Now.AddDays(-1).AddHours(-13).AddMinutes(54), Description = "Some desc" },
                new ProjectSummary { Id= 3, Name= "Dude= Wheres My Car - The Stage Show", Type= "Scripts", LastUsed = DateTime.Now.AddDays(-23).AddHours(1).AddMinutes(-14), Description = "Some desc" }
            };
        }

    }
}