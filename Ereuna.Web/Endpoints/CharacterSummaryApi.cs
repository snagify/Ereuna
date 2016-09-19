using System;
using System.Linq;
using System.Web.Http;
using Ereuna.Web.Common.Api;
using Ereuna.Web.Common.Session;
using Ereuna.Web.Data;

namespace Ereuna.Web.Endpoints
{
    [TokenAuthorizationFilter]
    public class CharacterSummaryApi : SecureApiEndpoint
    {
        public CharacterSummaryApi(EreunaContext context) : base(context) { }

        public IHttpActionResult Get(int id)
        {
            if (id <=0) return BadRequest("Must specify a valid project Id to get characters for");

            var chars = GetProject(id).Characters.Select(Map);
            return Ok(chars);
        }

        public IHttpActionResult Post(CharacterSummary character)
        {
            if (character == null) return BadRequest("Must provide character data to create");
            //if (string.IsNullOrEmpty(character.name)) return BadRequest("Must provide a name of the character to create");
            //if (character.projectId == null) return BadRequest("Must advise which project id to create this character in");
            var projectId = (int) character.ProjectId;
            if (projectId <= 0) return BadRequest("Must advise which project id to create this character in");

            var typeId = character.TypeId;
            //if (character.TypeId != null)
            //{
            //    typeId = (int) character.typeId;
            //    if (typeId <= 0) return BadRequest("When providing a character type, it must be a number between 1 and 3");
            //}
            
            var project = GetProject(projectId);
            if (project != null)
            {
                var newChar = new Character
                {
                    CharacterType = _context.CharacterTypes.FirstOrDefault(x => x.Id == typeId),
                    Name = character.Name,
                    Description = character.Description,
                    WhenAdded = DateTime.Now,
                    WhenUpdated = DateTime.Now
                };

                project.Characters.Add(newChar);

                _context.SaveChanges();

                return Ok(Map(newChar));
            }

            return NotFound();
        }



        private static CharacterSummary Map(Character character)
        {
            return new CharacterSummary
            {
                Id = character.Id,
                Name = character.Name,
                Description = character.Description,
                TypeId = character.CharacterType.Id,
                TypeName = character.CharacterType.Name,
                ProjectId = character.Project.Id
            };
        }
    }

    public class CharacterSummary
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}