using System.Web.Mvc;

namespace Ereuna.Web.Controllers
{
    public class DefaultController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }
        
    }
}