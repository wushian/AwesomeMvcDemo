using System.Linq;
using System.Web.Mvc;
using AwesomeMvcDemo.Models;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class GridCrudDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ClientData()
        {
            return View();
        }

        public ActionResult GetItem(int id)
        {
            var item = Db.Dinners.Where(o => o.Id == id).Select(o => new
            {
                o.Id,
                o.Name,
                o.Date,
                o.Chef,
                DateStr = o.Date.ToShortDateString(),
                ChefName = o.Chef.FirstName + " " + o.Chef.LastName,
                Meals = string.Join(", ", o.Meals.Select(m => m.Name))
            });

            return Json(item);
        }
    }
}