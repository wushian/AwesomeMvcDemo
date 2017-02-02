using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Awesome.Grid
{
    /*begin*/
    public class LunchGridController : Controller
    {
        public ActionResult GetItems(GridParams g, string person, string food, int? country)
        {
            food = (food ?? "").ToLower();
            person = (person ?? "").ToLower();

            var list = Db.Lunches
                .Where(o => o.Food.ToLower().Contains(food) && o.Person.ToLower().Contains(person))
                .AsQueryable();

            if (country.HasValue) list = list.Where(o => o.Country.Id == country);

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
                        ChefName = o.Chef.FirstName + " " + o.Chef.LastName
                    }
                }.Build());
        }
    }
    /*end*/
}