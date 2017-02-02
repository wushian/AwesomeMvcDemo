using System.Linq;
using System.Web.Mvc;
using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.ViewModels.Input;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class GridDemoController : Controller
    {
        public ActionResult Index()
        {
            var o = new GridDemoCfgInput
                {
                    Groupable = true, 
                    Sortable = true, 
                    Height = 350, 
                    MinHeight = 0, 
                    PageSize = 15, 
                    ShowGroupedColumn = true, 
                    LoadOnParentChange = true, 
                    Resizable = true,
                    Reorderable = true,
                    DataFunc = "getGridData",
                    Page1OnSort = false
                };

            return View(o);
        }

        public ActionResult IndexContent(GridDemoCfgInput input)
        {
            if (!input.UseRemoteData)
            {
                input.DataFunc = "getGridData";
            }

            return Json(new
            {
                Content = this.RenderPartialView("IndexContent", input),
                Cfg = input
            });
        }

        public ActionResult Selection()
        {
            return View();
        }

        public ActionResult RTLSupport()
        {
            return View();
        }

        public ActionResult CustomFormat()
        {
            return View();
        }

        public ActionResult CustomQuerying()
        {
            return View();
        }

        public ActionResult ClientSideApi()
        {
            return View();
        }

        public ActionResult Sorting()
        {
            var o = new GridDemoSortingCfgInput
            {
                Sortable = true,
                PageSize = 15,
                PersonSortable = true,
                PersonSort = Sort.Asc,
                PersonRank = 1,
                FoodSortable = true,
                FoodSort = Sort.None,
                FoodRank = 2,
            };
            return View(o);
        }

        public ActionResult SortingContent(GridDemoSortingCfgInput input)
        {
            return PartialView(input);
        }

        public ActionResult Grouping()
        {
            var o = new GridDemoGroupingCfgInput
            {
                Groupable = true,
                ShowGroupedColumn = false,
                ShowGroupBar = true,
                PersonGrouped = true,
                PersonRem = true,
                PersonGroupable = true,
                PersonRank = 1,
                FoodGrouped = false,
                FoodRem = true,
                FoodGroupable = true,
                PageSize = 15
            };
            return View(o);
        }

        public ActionResult GroupingContent(GridDemoGroupingCfgInput input)
        {
            return PartialView(input);
        }
    }
}