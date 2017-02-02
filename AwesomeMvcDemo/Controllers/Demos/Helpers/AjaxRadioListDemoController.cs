using System.Web.Mvc;

using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.ViewModels.Input;

namespace AwesomeMvcDemo.Controllers.Demos.Helpers
{
    public class AjaxRadioListDemoController : Controller
    {
        public ActionResult Index()
        {
            return View(new AjaxDropdownDemoInput
                {
                    ParentCategory = Db.Categories[0].Id, 
                    Category2 = Db.Categories[0].Id,
                    Category = Db.Categories[0].Id
                });
        }
    }
}