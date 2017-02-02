using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Awesome.MultiLookup
{
    public class MealsTableLayoutMultiLookupController : Controller
    {
        public ActionResult GetItems(int[] v)
        {
            v = v ?? new int[] {};
            return Json(Db.Meals.Where(f => v.Contains(f.Id)).Select( o => new KeyContent(o.Id, o.Name)));
        }

        //when using TableLayout(true) we will get a isTheadEmpty bool variable
        //which will tell us whether the table header is empty
        public ActionResult Search(string search, int page, bool isTheadEmpty)
        {
            const int PageSize = 5;
            search = (search ?? "").ToLower().Trim();
            var list = Db.Meals.Where(f => f.Name.ToLower().Contains(search));

            //viewdata will be passed to RenderView
            //in meal view there's a check for ViewData["multilookup"]
            // if it's not null the move button for the multilookup will be rendered
            ViewData["multilookup"] = true;

            var result = new AjaxListResult
            {
                Content = this.RenderView("ListItems/Meal", list.Skip((page - 1) * PageSize).Take(PageSize)),
                More = list.Count() > page * PageSize
            };

            //setting the table header with rendered html
            if (isTheadEmpty) result.Thead = this.RenderView("ListItems/MealThead");

            return Json(result);
        }

        public ActionResult Selected(int[] selected, bool isTheadEmpty)
        {
            ViewData["multilookup"] = true;
            var result = new AjaxListResult
                        {
                            Content = this.RenderView("ListItems/Meal",Db.Meals.Where(o => selected != null && selected.Contains(o.Id)))
                        };
            if (isTheadEmpty) result.Thead = this.RenderView("ListItems/MealThead");
            return Json(result);
        }
    }
}