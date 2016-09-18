using System;
using System.Linq;
using System.Web.Http;
using Ereuna.Web.Common.Api;
using Ereuna.Web.Common.Session;
using Ereuna.Web.Data;

namespace Ereuna.Web.Endpoints
{
    [TokenAuthorizationFilter]
    public class IdeaApi : SecureApiEndpoint
    {
        public IdeaApi(EreunaContext context) : base(context) { }

        public IHttpActionResult GetIdeasForProject(int id)
        {
            var ideas = GetProject(id)
                .Ideas
                .OrderBy(x => x.Importance)
                .Select(Map)
                .ToArray();

            return Ok(ideas);
        }

        public IHttpActionResult GetIdea(int projectId, int id)
        {
            var idea = GetProject(id).Ideas.FirstOrDefault(x => x.Id == id);
            return Ok(idea);
        }

        public IHttpActionResult Post(Idea idea)
        {
            if (idea == null) return BadRequest("Need more information about the idea");

            if (string.IsNullOrEmpty(idea.Title)) return BadRequest("Idea needs a Title");

            var project = GetProject(idea.ProjectId);
            var dbIdea = new Data.Idea
            {
                Title = idea.Title,
                Description = idea.Description,
                WhenAdded = DateTime.Now,
                Importance = idea.Importance
            };

            project.Ideas.Add(dbIdea);
            _context.SaveChanges();

            return Ok(Map(dbIdea));
        }

        public IHttpActionResult Put(Idea idea)
        {
            if (idea == null) return BadRequest("Need more information about the idea to update");

            if (string.IsNullOrEmpty(idea.Title)) return BadRequest("Idea needs a Title");

            var dbIdea = _context.Ideas.FirstOrDefault(x => x.Id == idea.Id);

            dbIdea.Title = idea.Title;
            dbIdea.Description = idea.Description;
            dbIdea.Importance = idea.Importance;

            _context.SaveChanges();

            return Ok(Map(dbIdea));
        }

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest("Need a valid Idea Id to delete");
            
            var dbIdea = _context.Ideas.FirstOrDefault(x => x.Id == id);
            _context.Ideas.Remove(dbIdea);
            _context.SaveChanges();

            return Ok();
        }

        private static Idea Map(Data.Idea idea)
        {
            return new Idea
            {
                Id = idea.Id,
                Title = idea.Title,
                Description = idea.Description,
                Importance = idea.Importance,
                WhenAdded = idea.WhenAdded,
                ProjectId = idea.Project.Id
            };
        }
    }


    public class Idea
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime WhenAdded { get; set; }

        public int Importance { get; set; }
    }
}