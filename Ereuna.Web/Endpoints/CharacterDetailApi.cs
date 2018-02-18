using System.Linq;
using System.Web.Http;
using Ereuna.Web.Common.Api;
using Ereuna.Web.Common.Session;
using Ereuna.Web.Data;

namespace Ereuna.Web.Endpoints
{
    [TokenAuthorizationFilter]
    public class CharacterDetailApi : SecureApiEndpoint
    {
        public CharacterDetailApi(EreunaContext context) : base(context) { }

        public IHttpActionResult Get(int characterid, int projectid)
        {
            if (projectid <= 0) return BadRequest("Must specify a valid character Id to get characters for");

            var character = GetProject(projectid).Characters.FirstOrDefault(x => x.Id == characterid);

            return Ok(new CharacterDetail(character));
        }

        
    }

    public class CharacterDetail
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int ImageId { get; set; }

        public CharacterDetail() { }

        public CharacterDetail(Character character)
        {
            Id = character.Id;
            Name = character.Name;
            Description = character.Description;
            TypeId = character.CharacterType.Id;
            TypeName = character.CharacterType.Name;
            ProjectId = character.Project.Id;
        }
    }
}