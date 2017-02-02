using System.Web.Mvc;

using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.ViewModels.Input;

namespace AwesomeMvcDemo.Controllers.Demos.Helpers
{
    public class AjaxCheckboxListDemoController : Controller
    {
        public ActionResult Index()
        {
            return View(new AjaxCheckboxListDemoInput
                {
                    CategoryData = Db.Categories[0].Id,
                    ParentCategory = new[] { Db.Categories[0].Id }
                });
        }
    }
}