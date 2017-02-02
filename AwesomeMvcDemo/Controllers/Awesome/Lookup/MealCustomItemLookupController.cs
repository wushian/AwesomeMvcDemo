using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Awesome.Lookup
{
    public class MealCustomItemLookupController : Controller
    {
        public ActionResult SearchForm()
        {
            return PartialView();
        }

        public ActionResult GetItem(int? v)
        {
            var o = Db.Meals.SingleOrDefault(f => f.Id == v) ?? new Meal();

            return Json(new KeyContent(o.Id, o.Name));
        }

        public ActionResult Search(string search, int page)
        {
            const int PageSize = 10;
            search = (search ?? "").ToLower().Trim();
            var list = Db.Meals.Where(f => f.Name.ToLower().Contains(search));
            return Json(new AjaxListResult
                {
                    Content = this.RenderView("items", list.Skip((page - 1) * PageSize).Take(PageSize)),
                    More = list.Count() > page * PageSize
                });
        }

        public ActionResult Details(int id)
        {
            return PartialView(Db.Get<Meal>(id) ?? new Meal());
        }
    }
}