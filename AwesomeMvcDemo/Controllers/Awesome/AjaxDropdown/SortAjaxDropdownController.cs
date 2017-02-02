using System.Web.Mvc;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Awesome.AjaxDropdown
{
    public class SortAjaxDropdownController : Controller
    {
        public ActionResult GetItems()
        {
            var options = new[]
                              {
                                  new KeyContent(Sort.None, "None"),
                                  new KeyContent(Sort.Asc, "Asc"),
                                  new KeyContent(Sort.Desc, "Desc")
                              };
            return Json(options);
        }
    }
}