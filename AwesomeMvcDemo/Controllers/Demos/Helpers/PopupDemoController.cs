using System.Web.Mvc;

namespace AwesomeMvcDemo.Controllers.Demos.Helpers
{
    public class PopupDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /*begin1*/
        public ActionResult Meals()
        {
            return PartialView();
        }
        /*end1*/

        /*begin2*/
        public ActionResult PopupWithParameters(string parent, int p1, string a, string b, int id)
        {
            ViewData["parent"] = parent;
            ViewData["p1"] = p1;
            ViewData["a"] = a;
            ViewData["b"] = b;
            ViewData["id"] = id;

            return PartialView();
        }
        /*end2*/

        /*begin3*/
        public ActionResult DropdownContent()
        {
            return PartialView();
        }
        /*end3*/

        public ActionResult PopupBtns()
        {
            return PartialView();
        }
    }
}