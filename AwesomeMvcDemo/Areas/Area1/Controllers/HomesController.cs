using System.Web.Mvc;

namespace AwesomeMvcDemo.Areas.Area1.Controllers
{
    public class HomesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string name)
        {
            ViewData["result"] = name;
            return View();
        }
    }
}
