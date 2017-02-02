using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Awesome.MultiLookup
{
    public class MealsCustomItemMultiLookupController : Controller
    {
        public ActionResult SearchForm()
        {
            return PartialView();
        }

        public ActionResult GetItems(IEnumerable<int> v)
        {
            return Json(Db.Meals.Where(o => v != null && v.Contains(o.Id))
                          .Select(f => new KeyContent(f.Id, f.Name)));
        }

        public ActionResult Search(string search, int[] selected, int page)
        {
            const int PageSize = 10;
            selected = selected ?? new int[] { };
            search = (search ?? "").ToLower().Trim();

            var items = Db.Meals.Where(o => o.Name.ToLower().Contains(search)
                                            && (!selected.Contains(o.Id)));
            
            //instead of setting the Items property we set the Content with rendered html
            return Json(new AjaxListResult
                {
                    Content = this.RenderView("items", items.Skip((page - 1) * PageSize).Take(PageSize)),
                    More = items.Count() > page * PageSize
                });
        }

        public ActionResult Selected(IEnumerable<int> selected)
        {
            return Json(new AjaxListResult
                {
                    Content = this.RenderView("items", Db.Meals.Where(o => selected != null && selected.Contains(o.Id)))
                });
        }

        //used by the details button, in items view
        public ActionResult Details(int id)
        {
            return PartialView(Db.Get<Meal>(id));
        }
    }
}