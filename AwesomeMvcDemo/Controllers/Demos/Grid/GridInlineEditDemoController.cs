using System;
using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.ViewModels.Input;
using Omu.Awem.Utils;
using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    /*begin*/
    public class GridInlineEditDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConditionalDemo()
        {
            return View();
        }

        public ActionResult MultiEditorsDemo()
        {
            return View();
        }

        private object MapToGridModel(Dinner o)
        {
            return new
                {
                    o.Id,
                    o.Name,
                    Date = o.Date.ToShortDateString(),
                    ChefName = o.Chef.FirstName + " " + o.Chef.LastName,
                    Meals = string.Join(", ", o.Meals.Select(m => m.Name)),
                    BonusMeal = o.BonusMeal.Name,
                    o.Organic,
                    DispOrganic = o.Organic ? "Yes" : "",

                    // below properties used for inline editing only
                    MealsIds = o.Meals.Select(m => m.Id).ToArray(),
                    ChefId = o.Chef.Id,
                    BonusMealId = o.BonusMeal.Id,

                    // for conditional demo
                    Editable = o.Meals.Count() > 1,
                    DateReadOnly = o.Date.Year < 2012
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = Db.Dinners.Where(o => o.Name.ToLower().Contains(search)).AsQueryable();
            var model = new GridModelBuilder<Dinner>(items, g)
                    {
                        Key = "Id", // needed for api select, update, tree, nesting, EF
                        GetItem = () => Db.Get<Dinner>(Convert.ToInt32(g.Key)), // called by the grid.api.update
                        Map = MapToGridModel,
                    }.Build();

            return Json(model);
        }

        [HttpPost]
        public ActionResult Create(DinnerInput input)
        {
            if (ModelState.IsValid)
            {
                var dinner = new Dinner
                    {
                        Name = input.Name,
                        Date = input.Date.Value,
                        Chef = Db.Get<Chef>(input.Chef),
                        Meals = input.Meals.Select(mid => Db.Get<Meal>(mid)),
                        BonusMeal = Db.Get<Meal>(input.BonusMealId),
                        Organic = input.Organic ?? false
                    };

                Db.Insert(dinner);

                return Json(new { Item = MapToGridModel(dinner) });
            }

            return Json(ModelState.GetErrorsInline());
        }

        [HttpPost]
        public ActionResult Edit(DinnerInput input)
        {
            if (ModelState.IsValid)
            {
                var dinner = Db.Get<Dinner>(input.Id);
                dinner.Name = input.Name;
                dinner.Date = input.Date.Value;
                dinner.Chef = Db.Get<Chef>(input.Chef);

                dinner.Meals = input.Meals.Select(mid => Db.Get<Meal>(mid));

                dinner.BonusMeal = Db.Get<Meal>(input.BonusMealId);
                dinner.Organic = input.Organic ?? false;
                Db.Update(dinner);

                return Json(new { });
            }

            return Json(ModelState.GetErrorsInline());
        }

        public ActionResult Delete(int id, string gridId)
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

        public ActionResult Popup()
        {
            return PartialView();
        }
    }
    /*end*/
}