using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Awesome.Autocomplete
{
    public class CategoryAutocompleteController : Controller
    {
        public ActionResult GetItems(string v)
        {
            v = (v ?? "").ToLower().Trim();
            return Json(Db.Categories.Where(o => o.Name.ToLower().Contains(v))
                          .Select(o => new KeyContent(o.Id, o.Name)));
        }
    }
}