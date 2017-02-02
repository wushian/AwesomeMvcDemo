using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class GridClientDataDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLunches()
        {
            return Json(Db.Lunches.Select(o => new { o.Id, o.Person, o.Food,o.Location, o.Country, o.Chef, o.Date, DateStr = o.Date.ToShortDateString() }));
        }
    }
}