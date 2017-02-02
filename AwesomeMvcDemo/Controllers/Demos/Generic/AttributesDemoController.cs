using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.ViewModels.Input;

namespace AwesomeMvcDemo.Controllers.Demos.Generic
{
    public class AttributesDemoController : Controller
    {
        public ActionResult Index()
        {
            return View(new AttributesDemoInput { ParentCategory = Db.Categories.First().Id });
        }

        [HttpPost]
        public ActionResult Index(AttributesDemoInput input)
        {
            return View(input);
        }
    }
}