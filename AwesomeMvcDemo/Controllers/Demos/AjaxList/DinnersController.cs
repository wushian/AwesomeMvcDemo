using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.ViewModels.Input;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Demos.AjaxList
{
    /*begin*/
    public class DinnersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDinners(string search, bool isTheadEmpty, int? pageSize, int? pivot)
        {
            var ps = pageSize ?? 7;
            search = (search ?? "").ToLower().Trim();

            var list = Db.Dinners.Where(o => o.Name.ToLower().Contains(search))
                         .OrderByDescending(o => o.Id);

            var items = (pivot.HasValue ? list.Where(o => o.Id <= pivot.Value) : list).Take(ps + 1).ToList();
            var isMore = items.Count > ps;
            
            var result = new AjaxListResult
            {
                Content = this.RenderPartialView("ListItems/Dinner", items.Take(ps)),
                More = isMore
            };

            if (isMore)
            {
                result.Pivot = items.Last().Id;
            }

            if (isTheadEmpty) result.Thead = this.RenderPartialView("ListItems/DinnerThead");
            return Json(result);
        }

        public ActionResult Delete(int id)
        {
            var dinner = Db.Get<Dinner>(id);

            return PartialView(new DeleteConfirmInput
            {
                Id = id,
                Message = string.Format("Are you sure you want to delete dinner <b>{0}</b> ?", dinner.Name)
            });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            Db.Delete<Dinner>(input.Id);
            return Json(new { input.Id });
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(DinnerInput input)
        {
            if (!ModelState.IsValid) return View(input);

            var dinner = Db.Insert(new Dinner
            {
                Name = input.Name,
                Date = input.Date.Value,
                Chef = Db.Get<Chef>(input.Chef),
                Meals = Db.Meals.Where(o => input.Meals.Contains(o.Id)),
                BonusMeal = Db.Get<Meal>(input.BonusMealId)
            });

            return Json(new { Content = this.RenderPartialView("ListItems/Dinner", new[] { dinner }) });
        }

        public ActionResult Edit(int id)
        {
            var dinner = Db.Get<Dinner>(id);

            var input = new DinnerInput
            {
                Id = dinner.Id,
                Name = dinner.Name,
                Chef = dinner.Chef.Id,
                Date = dinner.Date,
                Meals = dinner.Meals.Select(o => o.Id),
                BonusMealId = dinner.BonusMeal.Id
            };

            return PartialView("create", input);
        }

        [HttpPost]
        public ActionResult Edit(DinnerInput input)
        {
            if (!ModelState.IsValid) return View("create", input);
            var dinner = Db.Get<Dinner>(input.Id);

            dinner.Name = input.Name;
            dinner.Date = input.Date.Value;
            dinner.Chef = Db.Get<Chef>(input.Chef);
            dinner.Meals = Db.Meals.Where(m => input.Meals.Contains(m.Id));
            Db.Update(dinner);

            return Json(new { dinner.Id, Content = this.RenderPartialView("ListItems/Dinner", new[] { dinner }) });
        }
    }
    /*end*/
}