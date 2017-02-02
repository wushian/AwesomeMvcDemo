using System.Web.Mvc;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class PurchasesCrudDemoController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToActionPermanent("Index", "GridCrudDemo");
        }
    }
}