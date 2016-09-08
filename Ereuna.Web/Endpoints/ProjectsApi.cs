using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public IEnumerable<ProjectSummary> GetAllProjects()
        {
            var id = UserId;

            var projects = _context.Users.First(x => x.Id == id).Projects.Select(MapProjectToSummary);

            return projects;
        }

        /// <summary>
        /// Returns the two most recent projects (based on date they were last used)
        /// </summary>
        public IEnumerable<ProjectSummary> GetRecentProjects()
        {
            var id = UserId;

            var projects = _context.Users.First(x => x.Id == id).Projects
                .OrderByDescending(x => x.LastUsed)
                .Take(2)
                .Select(MapProjectToSummary);

            return projects;
        }

        private ProjectSummary MapProjectToSummary(Project p)
        {
            if (p == null) return null;
            return new ProjectSummary
            {
                Id = p.Id, 
                Name = p.Name,
                Description = p.Description,
                Type = p.ProjectType.Title,
                LastUsed = p.LastUsed
            };

        }


    }
}