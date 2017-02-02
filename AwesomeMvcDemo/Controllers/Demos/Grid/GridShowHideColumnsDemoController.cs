using System.Web.Mvc;

using AwesomeMvcDemo.ViewModels.Input;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class GridShowHideColumnsDemoController : Controller
    {
        public ActionResult Index()
        {
            return View(new GridHideColumnsDemoInput{ ShowFood = true, ShowLocation = true });
        }

        public ActionResult GridContent(GridHideColumnsDemoInput input)
        {
            return PartialView(input);
        }
    }
}