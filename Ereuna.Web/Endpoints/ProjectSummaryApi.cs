﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using Ereuna.Web.Common.Api;
using Ereuna.Web.Common.Session;
using Ereuna.Web.Data;
using Ereuna.Web.Models;

namespace Ereuna.Web.Endpoints
{
    [TokenAuthorizationFilter]
    public class ProjectSummaryApi : SecureApiEndpoint
    {
        public ProjectSummaryApi(EreunaContext context) : base(context) { }
        
        public IEnumerable<ProjectSummary> GetAllProjects()
        {
            var id = UserId;

            var projects = _context.Users.First(x => x.Id == id).Projects.OrderByDescending(x => x.LastUsed).Select(Map);

            return projects;
        }
        
        public IHttpActionResult Get(int projectId)
        {
            var project = GetProject(projectId);

            if (project == null) return NotFound();
            return Ok(Map(project));
        }

        public int PostProject(dynamic project)
        {
            var ptid = (int)project.projectTypeId;

            var pt = _context.ProjectTypes.FirstOrDefault(x => x.Id == ptid);
            var user = _context.Users.FirstOrDefault(x => x.Id == UserId);

            var p = new Project
            {
                Name = project.name,
                Description = project.description,
                ProjectType = pt,
                LastUsed = DateTime.Now,
                User = user
            };
            _context.Projects.Add(p);
            _context.SaveChanges();

            return p.Id;
        }

        public IHttpActionResult Put(ProjectSummary project)
        {
            if (string.IsNullOrEmpty(project.Name)) return BadRequest("Missing field: Project Name");
            
            var dbProject = GetProject(project.Id);
            if (dbProject == null) return NotFound();

            dbProject.Name = project.Name;
            dbProject.Description = project.Description;

            _context.Projects.AddOrUpdate(dbProject);
            _context.SaveChanges();

            return Ok();
        }

        private static ProjectSummary Map(Project p)
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

    public class ProjectSummary
    {
        public int Id { get; set; }
        public DateTime LastUsed { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Type { get; set; }
    }
}