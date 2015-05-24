using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Ereuna.Web.Data;
using Ereuna.Web.Models;

namespace Ereuna.Web.Controllers
{
    public class ActivityController : ApiController
    {
        private readonly EreunaContext _context;

        public ActivityController(EreunaContext context)
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