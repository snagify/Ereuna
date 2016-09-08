using System.Collections.Generic;
using System.Linq;
using Ereuna.Web.Common.Api;
using Ereuna.Web.Data;
using Ereuna.Web.Models;

namespace Ereuna.Web.home.APIs
{
    public class ActivityApi : ApiEndpoint
    {
        private readonly EreunaContext _context;

        public ActivityApi(EreunaContext context)
        {
            _context = context;
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            var activities = _context.ApplicationActivities
                .OrderByDescending(x => x.Occurred)
                .Take(5)
                .Select(x => new Activity {Id = x.Id, Occurred = x.Occurred, Title = x.Title})
                .ToArray();

            return activities;

        }

    }
}