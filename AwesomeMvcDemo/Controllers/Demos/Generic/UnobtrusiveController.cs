using System.Web.Mvc;

using AwesomeMvcDemo.ViewModels.Input;

namespace AwesomeMvcDemo.Controllers.Demos.Generic
{
    public class UnobtrusiveController : Controller
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