using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Awesome.MultiLookup
{
    public class CategoriesMultiLookupController : Controller
    {
        public ActionResult GetItems(IEnumerable<int> v)
        {
            return Json(Db.Categories.Where(o => v != null && v.Contains(o.Id))
                          .Select(f => new KeyContent(f.Id, f.Name)));
        }

        public ActionResult Search(string search, IEnumerable<int> selected)
        {
            search = (search ?? "").ToLower();
            return Json(new AjaxListResult
                {
                    Items = Db.Categories.Where(o => o.Name.ToLower().Contains(search) && (selected == null || !selected.Contains(o.Id)))
                              .Select(o => new KeyContent(o.Id, o.Name))
                });
        }

        public ActionResult Selected(IEnumerable<int> selected)
        {
            return Json(
                new AjaxListResult
                    {
                        Items = Db.Categories.Where(o => selected != null && selected.Contains(o.Id))
                                  .Select(o => new KeyContent(o.Id, o.Name))
                    });

        }
    }
}