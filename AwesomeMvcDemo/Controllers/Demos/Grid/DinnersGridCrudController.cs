using System;
using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.ViewModels.Input;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    /*begin*/
    /// <summary>
    /// Note: If your Key Property is not "Id" (but "MyId"), you need to change the Key = "MyId" in GridGetItems and it in the view
    /// change the Bind = "MyId" (if you're showing the id column), 
    /// and for the action columns you can either specify the MyId property (e.g. GridUtils.EditFormatForGrid("DinnersGrid", "MyId"));
    /// or in MapToGridModel additionally to o.MyId add another property Id = o.MyId
    /// parameters for Edit, Delete controller Actions need to remain called "id", that's how they are set in GridUtils.cs (params:{{ id: );
    /// Edit and Delete post actions must return an object with property "Id" - in utils.js itemEdited and itemDeleted funcs expect it this way;
    /// </summary>
    public class DinnersGridCrudController : Controller
    {
        private static object MapToGridModel(Dinner o)
        {
            return
                new
                {
                    o.Id,
                    o.Name,
                    Date = o.Date.ToShortDateString(),
                    ChefName = o.Chef.FirstName + " " + o.Chef.LastName,
                    Meals = string.Join(", ", o.Meals.Select(m => m.Name))
                };
        }

        public ActionResult GridGetItems(GridParams g, string search)
        {
            search = (search ?? "").ToLower();
            var items = Db.Dinners.Where(o => o.Name.ToLower().Contains(search)).AsQueryable();

            return Json(new GridModelBuilder<Dinner>(items, g)
                {
                    Key = "Id", // needed for api select, update, tree, nesting, EF
                    GetItem = () => Db.Get<Dinner>(Convert.ToInt32(g.Key)), // called by the grid.api.update ( edit popupform success js func )
                    Map = MapToGridModel
                }.Build());
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(DinnerInput input)
        {
            if (!ModelState.IsValid) return PartialView(input);

            var dinner = Db.Insert(new Dinner
                {
                    Name = input.Name,
                    Date = input.Date.Value,
                    Chef = Db.Get<Chef>(input.Chef),
                    Meals = input.Meals.Select(mid => Db.Get<Meal>(mid)),
                    BonusMeal = Db.Get<Meal>(input.BonusMealId)
                });

            return Json(MapToGridModel(dinner)); // returning grid model, used in grid.api.renderRow
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

            return PartialView("Create", input);
        }

        [HttpPost]
        public ActionResult Edit(DinnerInput input)
        {
            if (!ModelState.IsValid) return PartialView("Create", input);
            var dinner = Db.Get<Dinner>(input.Id);

            dinner.Name = input.Name;
            dinner.Date = input.Date.Value;
            dinner.Chef = Db.Get<Chef>(input.Chef);
            dinner.Meals = input.Meals.Select(mid => Db.Get<Meal>(mid));
            dinner.BonusMeal = Db.Get<Meal>(input.BonusMealId);
            Db.Update(dinner);
            
            // returning the key to call grid.api.update
            return Json(new { Id = dinner.Id }); 
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
            return Json(new { Id = input.Id });
        }
    }
    /*end*/
}