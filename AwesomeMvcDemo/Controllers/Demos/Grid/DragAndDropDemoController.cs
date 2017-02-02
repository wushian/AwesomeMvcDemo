using System.Linq;
using System.Web.Mvc;
using AwesomeMvcDemo.Models;
using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class DragAndDropDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /*begin*/
        public ActionResult MealsGridSrc(GridParams g, int[] selected)
        {
            selected = selected ?? new int[] {};
            var items = Db.Meals.Where(o => !selected.Contains(o.Id)).AsQueryable();
            return Json(new GridModelBuilder<Meal>(items, g) { Key = "Id" }.Build());
        }

        public ActionResult MealsGridSel(GridParams g, int[] selected)
        {
            selected = selected ?? new int[] { };
            var items = Db.Meals.Where(o => selected.Contains(o.Id)).AsQueryable();
            return Json(new GridModelBuilder<Meal>(items, g) { Key = "Id" }.Build());
        }
        /*end*/
    }
}