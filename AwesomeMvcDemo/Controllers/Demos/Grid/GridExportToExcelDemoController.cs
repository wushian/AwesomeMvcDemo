using System.IO;
using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.Utils;

using NPOI.HSSF.UserModel;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    /*begin*/
    public class GridExportToExcelDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetItems(GridParams g)
        {
            return Json(BuildGridModel(g).ToDto());
        }

        private GridModel<Lunch> BuildGridModel(GridParams g)
        {
            return new GridModelBuilder<Lunch>(Db.Lunches.AsQueryable(), g)
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
                },
                MakeFooter = gi =>
                    {
                        if (gi.Level == 0)
                        {
                            return new
                            {
                                Id = "Total",
                                Price = "Min: " + gi.Items.Min(o => o.Price),
                                Date = "Max: " + gi.Items.Max(o => o.Date).ToShortDateString()
                            };
                        }

                        return new
                        {
                            Price = "Min: " + gi.Items.Min(o => o.Price),
                            Date = "Max: " + gi.Items.Max(o => o.Date).ToShortDateString()
                        };
                    }
            }.BuildModel();
        }

        [HttpPost]
        public ActionResult ExportGridToExcel(GridParams g, bool? allPages)
        {
            if (allPages.HasValue && allPages.Value)
            {
                g.Paging = false;
            }

            var gridModel = BuildGridModel(g);

            var columns = new[] { "Id", "Person", "Food", "Date", "Price", "CountryName", "ChefName", "Location" };
            var headers = new[] { "Id", "Person", "Food", "Date", "Price", "Country", "Chef", "Location" };

            var workbook = GridExcelBuilder.Build(gridModel, columns, headers);

            var stream = new MemoryStream();
            workbook.Write(stream);
            stream.Close();

            return File(stream.ToArray(), "application/vnd.ms-excel", "lunches.xls");
        }

        /// <summary>
        /// Demonstrates the simplest way of creating an excel workbook 
        /// it exports all the lunches records, without taking into count any sorting/paging that is done on the client side
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExportAllToExcel()
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("sheet1");

            var items = Db.Lunches.Select(
                o => new
                {
                    o.Id,
                    o.Person,
                    o.Food,
                    o.Location,
                    CountryName = o.Country.Name,
                    ChefName = o.Chef.FirstName + " " + o.Chef.LastName
                }).ToList();

            var columns = new[] { "Id", "Person", "Food", "CountryName", "ChefName", "Location" };
            var headers = new[] { "Id", "Person", "Food", "Country", "Chef", "Location" };

            var headerRow = sheet.CreateRow(0);

            //create header
            for (int i = 0; i < columns.Length; i++)
            {
                var cell = headerRow.CreateCell(i);
                cell.SetCellValue(headers[i]);
            }

            //fill content 
            for (int i = 0; i < items.Count; i++)
            {
                var rowIndex = i + 1;
                var row = sheet.CreateRow(rowIndex);

                for (int j = 0; j < columns.Length; j++)
                {
                    var cell = row.CreateCell(j);
                    var o = items[i];
                    cell.SetCellValue(o.GetType().GetProperty(columns[j]).GetValue(o, null).ToString());
                }
            }

            var stream = new MemoryStream();
            workbook.Write(stream);
            stream.Close();

            return File(stream.ToArray(), "application/vnd.ms-excel", "lunches.xls");
        }
    }
    /*end*/
}