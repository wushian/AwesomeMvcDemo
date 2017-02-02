using System.Web.Mvc;

using AwesomeMvcDemo.Models;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class GridNestingDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /*begin1*/
        public ActionResult LunchDetail(int key)
        {
            var lunch = Db.Get<Lunch>(key);
            return PartialView(lunch);
        }
        /*end1*/

        /*begin*/
        public ActionResult MealGrid(int key)
        {
            ViewData["Id"] = key;
            return PartialView();
        }
        /*end*/
    }
}