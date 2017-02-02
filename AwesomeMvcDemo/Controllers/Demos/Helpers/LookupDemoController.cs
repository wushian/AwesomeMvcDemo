using System.Web.Mvc;

using AwesomeMvcDemo.ViewModels.Input;

namespace AwesomeMvcDemo.Controllers.Demos.Helpers
{
    public class LookupDemoController : Controller
    {
        public ActionResult Index()
        {
            return View(new LookupDemoCfgInput
                            {
                                HighlightChange = true,
                                ClearButton = true,
                                Width = 750,
                                Height = 400
                            });
        }

        [HttpPost]
        public ActionResult IndexContent(LookupDemoCfgInput input)
        {
            return PartialView(input);
        }

        public ActionResult CustomSearch()
        {
            return View();
        }


        public ActionResult Misc()
        {
            return View(new LookupDemoInput());
        }
    }
}