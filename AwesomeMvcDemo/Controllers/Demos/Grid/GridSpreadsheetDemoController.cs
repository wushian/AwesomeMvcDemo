using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.ViewModels;
using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class GridSpreadsheetDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Autocomplete()
        {
            return View();
        }

        /*begin*/
        public ActionResult GridGetItems(GridParams g)
        {
            return Json(new GridModelBuilder<Spreadsheet>(Db.Spreadsheets.OrderByDescending(o => o.Id).AsQueryable(), g).Build());
        }

        public ActionResult Add()
        {
            return Json(new Spreadsheet());
        }

        public ActionResult Save(int id, string name, string value)
        {
            var row = id == 0 ? Db.Insert(new Spreadsheet()) : Db.Get<Spreadsheet>(id);

            //this is an inmemory object with a real Db you would use UPDATE Spreadsheets SET {name}={val} where id={id}
            typeof(Spreadsheet).GetProperty(name).SetValue(row, value, null);

            return Json(row);
        }
        /*end*/

        /*begin2*/
        private static IList<List<string>> data = new List<List<string>>{
                    new List<string> { "Id", "Name", "Meal" },
                    new List<string> { "1", "Vincenzo", "Pizza" },
                    new List<string> { "2", "Ella", "French toast" },
                    new List<string> { "3", "Zazzles", "Pizza" },
                    new List<string> { "4", "Evan", "Banana" }
                };

        private static int nextid = 5;

        public ActionResult AddColumn()
        {
            var header = data.First();
            var name = "col" + header.Count;
            header.Add(name);

            for (var i = 1; i < data.Count; i++)
            {
                data[i].Add(string.Empty);
            }

            return Json(new { });
        }

        public ActionResult SaveArr(int id, int col, string value)
        {
            if (id == 0)
            {
                id = nextid;
                nextid++;

                var newRow = new List<string> { id.ToString() };
                for (var i = 1; i < data.Last().Count; i++)
                {
                    newRow.Add(i == col ? value : string.Empty);
                }

                data.Add(newRow);
                return Json(new { Id = id });
            }

            var exRow = data.SingleOrDefault(o => o[0] == id.ToString());

            if (exRow == null) throw new Exception("edited row doesn't exist anymore");

            exRow[col] = value;
            return Json(new { Id = id });
        }

        public ActionResult MultiColGrid(GridParams g)
        {
            g.Paging = false;

            var items = new List<GridArrayRow>();
            for (var i = 1; i < data.Count; i++)
            {
                items.Add(new GridArrayRow { Id = data[i][0], Values = data[i].ToArray() });
            }

            // reinit columns at start or when new column has been added
            if (g.Columns.Length == 0 || g.Columns.Length != data[0].Count - 1)
            {
                var columns = new List<Column>();

                for (var i = 1; i < data[0].Count; i++)
                {
                    columns.Add(new Column { Header = data[0][i], Bind = i.ToString(), ClientFormatFunc = "txtVal(" + i + ")", Resizable = true });
                }

                g.Columns = columns.ToArray();
            }

            var model = new GridModelBuilder<GridArrayRow>(items.AsQueryable(), g).Build();
            return Json(model);
        }
        /*end2*/
    }
}