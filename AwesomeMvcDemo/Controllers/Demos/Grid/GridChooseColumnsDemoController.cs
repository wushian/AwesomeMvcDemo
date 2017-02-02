using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class GridChooseColumnsDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /*begin*/
        public ActionResult GetItems(GridParams g, string[] selectedColumns, bool? choosingColumns)
        {
            //when setting columns from here we don't get the grid defaults, so we have to specify Sortable, Groupable etc.
            var columns = new[]
                {
                    new Column { Bind = "Id", Width = 70, Order = 1 },
                    new Column { Bind = "Person", Sortable = true, Groupable = true, GroupRemovable = true, Order = 2 },
                    new Column { Bind = "Food", Sortable = true, Groupable = true, GroupRemovable = true, Order = 3 },
                    new Column { Bind = "Location", Sortable = true, Groupable = true, GroupRemovable = true, Order = 4 },
                    new Column { Bind = "Date", Sortable = true, Groupable = true, GroupRemovable = true, Width = 120, Order = 5 },
                    new Column { Bind = "Price", Sortable = true, Groupable = true, GroupRemovable = true, Width = 100, Order = 6 }
                };

            var baseColumns = new[] { "Id", "Person" };

            //first load
            if (g.Columns.Length == 0)
            {
                g.Columns = columns;
            }

            if (choosingColumns.HasValue)
            {
                selectedColumns = selectedColumns ?? new string[] { };

                //make sure we always have Id and Person columns
                selectedColumns = selectedColumns.Union(baseColumns).ToArray();

                var currectColumns = g.Columns.ToList();

                //remove unselected columns
                currectColumns = currectColumns.Where(o => selectedColumns.Contains(o.Bind)).ToList();

                //add missing columns
                var missingColumns = selectedColumns.Except(currectColumns.Select(o => o.Bind)).ToArray();

                currectColumns.AddRange(columns.Where(o => missingColumns.Contains(o.Bind)));

                g.Columns = currectColumns.ToArray();
            }

            var gridModel = new GridModelBuilder<Lunch>(Db.Lunches.AsQueryable(), g)
            {
                Map = o => new { o.Id, o.Person, o.Food, o.Location, o.Price, Date = o.Date.ToShortDateString() },
                //used to populate the checkboxlist
                Tag = new
                {
                    columns = columns.Select(o => o.Bind).Except(baseColumns).ToArray(),
                    selectedColumns = g.Columns.Select(o => o.Bind).Except(baseColumns).ToArray()
                }
            }.Build();

            return Json(gridModel);
        }

        public ActionResult GetColumnsItems(string[] columns)
        {
            columns = columns ?? new string[] { };

            return Json(columns.Select(o => new KeyContent(o, o)));
        }
        /*end*/
    }
}