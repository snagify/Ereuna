using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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
        private readonly ISessionProvider _sessionProvider;

        public ProjectsApi(EreunaContext context, ISessionProvider sessionProvider)
        {
            _context = context;
            _sessionProvider = sessionProvider;
        }
        
        public IEnumerable<ProjectSummary> GetAllProjects()
        {
            var id = UserId;

            var projects = _context.Users.First(x => x.Id == id).Projects.OrderByDescending(x => x.LastUsed).Select(MapProjectToSummary);

            return projects;
        }
        
        public IHttpActionResult GetProject(int projectId)
        {
            var id = UserId;

            var project = _context.Users.First(x => x.Id == id).Projects.FirstOrDefault(x => x.Id == projectId);

            if (project == null) return NotFound();
            return Ok(project);
        }

        public int PostProject(dynamic project)
        {
            var ptid = (int)project.projectTypeId;

            var pt = _context.ProjectTypes.FirstOrDefault(x => x.Id == ptid);
            var SessionUser = _sessionProvider.GetSessionUser();
            var user = _context.Users.FirstOrDefault(x => x.Id == SessionUser.Id);

            var p = new Project { Name = project.name, Description = project.description, ProjectType = pt, LastUsed = DateTime.Now, User = user };
            _context.Projects.Add(p);
            _context.SaveChanges();

            return p.Id;
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