﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ereuna.Web.Common.Api;
using Ereuna.Web.Common.Session;
using Ereuna.Web.Data;

namespace Ereuna.Web.Endpoints
{
    [TokenAuthorizationFilter]
    public class RecentProjectsApi : SecureApiEndpoint
    {
        public RecentProjectsApi(EreunaContext context) : base(context) { }

        /// <summary>
        /// Returns the two most recent projects (based on date they were last used)
        /// </summary>
        public IEnumerable<RecentProject> GetAllRecentProjects()
        {
            var projects = _context.Projects.Where(x => x.User.Id == UserId)
                .OrderByDescending(x => x.LastUsed)
                .Take(2)
                .Select(Map);

            return projects;
        }
        

        private static RecentProject Map(Project p)
        {
            if (p == null) return null;
            return new RecentProject
            {
                ProjectId = p.Id, 
                Name = p.Name,
                Description = p.Description,
                ProjectType = p.ProjectType.Title,
                LastUsed = p.LastUsed,
                NumberIdeas = 0
            };
        }
    }

    public class RecentProject
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectType { get; set; }
        public DateTime LastUsed { get; set; }

        public int NumberIdeas { get; set; }
        
    }
}