using System.Web.Mvc;

using AwesomeMvcDemo.ViewModels.Input;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class RtlDemoController : Controller
    {
        public ActionResult Index()
        {
            return View(new UnobstrusiveInput());
        }

        [HttpPost]
        public ActionResult Index(UnobstrusiveInput input)
        {
            return View(input);
        }
    }
}