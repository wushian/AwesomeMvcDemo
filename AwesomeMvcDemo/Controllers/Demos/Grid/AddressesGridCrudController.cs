using System;
using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.ViewModels.Input;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class AddressesGridCrudController : Controller
    {
        public ActionResult GridGetItems(GridParams g, int restaurantId)
        {
            var items = Db.RestaurantAddresses.Where(o => o.RestaurantId == restaurantId).AsQueryable();
            var model = new GridModelBuilder<RestaurantAddress>(items, g)
                {
                    Key = "Id",
                    GetItem = () => Db.Get<RestaurantAddress>(Convert.ToInt32(g.Key))
                }.Build();
            return Json(model);
        }

        public ActionResult Create(int restaurantId)
        {
            return PartialView(new RestaurantAddressInput { RestaurantId = restaurantId });
        }

        [HttpPost]
        public ActionResult Create(RestaurantAddressInput input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(input);
            }

            var address = Db.Insert(new RestaurantAddress { Line1 = input.Line1, Line2 = input.Line2, RestaurantId = input.RestaurantId });

            return Json(address); // use MapToGridModel like in Grid Crud Demo when grid uses Map
        }

        public ActionResult Edit(int id)
        {
            var address = Db.Get<RestaurantAddress>(id);

            return PartialView(
                "Create",
                new RestaurantAddressInput
                    {
                        Line1 = address.Line1,
                        Line2 = address.Line2,
                        RestaurantId = address.RestaurantId
                    });
        }

        [HttpPost]
        public ActionResult Edit(RestaurantAddressInput input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Create", input);
            }

            var address = Db.Get<RestaurantAddress>(input.Id);
            address.Line1 = input.Line1;
            address.Line2 = input.Line2;
            return Json(new { input.Id });
        }

        public ActionResult Delete(int id)
        {
            var address = Db.Get<RestaurantAddress>(id);

            return PartialView(new DeleteConfirmInput
                {
                    Id = id,
                    Message = string.Format("Are you sure you want to delete restaurant address <b>{0} {1}</b> ?", address.Line1, address.Line2)
                });
        }

        [HttpPost]
        public ActionResult Delete(DeleteConfirmInput input)
        {
            Db.Delete<RestaurantAddress>(input.Id);
            return Json(new { input.Id });
        }
    }
}