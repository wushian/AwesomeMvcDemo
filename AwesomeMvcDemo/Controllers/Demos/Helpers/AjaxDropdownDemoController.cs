using System.Web.Mvc;

using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.ViewModels.Input;

namespace AwesomeMvcDemo.Controllers.Demos.Helpers
{
    public class AjaxDropdownDemoController : Controller
    {
        public ActionResult Index()
        {
            return View(new AjaxDropdownDemoInput {Category = Db.Categories[0].Id, Category1 = Db.Categories[0].Id });
        }
    }
}