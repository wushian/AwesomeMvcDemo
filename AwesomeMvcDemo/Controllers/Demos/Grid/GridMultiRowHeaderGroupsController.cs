using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class GridMultiRowHeaderGroupsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GridGetItems(GridParams g)
        {
            var list = Db.Lunches.AsQueryable();

            return Json(new GridModelBuilder<Lunch>(list, g)
            {
                Key = "Id", // needed for Entity Framework | nesting | tree | api
                Map = o => new
                {
                    o.Id,
                    o.Person,
                    o.Food,
                    o.Location,
                    o.Price,
                    Date = o.Date.ToShortDateString(),
                    CountryName = o.Country.Name,
                    ChefFName = o.Chef.FirstName,
                    ChefLName = o.Chef.LastName
                }
            }.Build());
        }
    }
}