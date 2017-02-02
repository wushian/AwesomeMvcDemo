using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Awesome.AjaxDropdown
{
    /*begin*/
    public class MealBoundToManyController : Controller
    {
        public ActionResult GetItems(int[] parent, string mealName)
        {
            mealName = mealName ?? string.Empty;
            parent = parent ?? new int[] { };
            
            var meals = Db.Meals.Where(o => 
                (parent.Contains(o.Category.Id))
                && o.Name.ToLower().Contains(mealName.ToLower()));

            return Json(meals.Select(o => new KeyContent(o.Id, o.Name)));
        }
    }
    /*end*/
}