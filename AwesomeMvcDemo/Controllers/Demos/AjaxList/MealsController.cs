using System.Web.Mvc;

namespace AwesomeMvcDemo.Controllers.Demos.AjaxList
{
    public class MealsController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToActionPermanent("Index", "Dinners");
        }
    }
}