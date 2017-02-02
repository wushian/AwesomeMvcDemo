using System;
using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    /*begin*/
    public class GroupingGridController : Controller
    {
        public ActionResult GetItems(GridParams g, bool collapsed)
        {
            //passing 2 Functions MakeFooter and MakeHeader to customize header and add footer

            var builder =
                new GridModelBuilder<Lunch>(Db.Lunches.AsQueryable(), g)
                    {
                        Key = "Id",
                        Map = o => new { o.Id, o.Person, o.Food, Date = o.Date.ToShortDateString(), o.Price, o.Location, ChefName = o.Chef.FirstName + " " + o.Chef.LastName },
                        MakeFooter = MakeFooter,
                        MakeHeader = gr =>
                            {
                                //get first item in the group
                                var first = gr.Items.First();

                                //get the grouped column value(s) for the first item
                                var val = string.Join(" ", AweUtil.GetColumnValue(gr.Column, first).Select(ToStr));

                                return new GroupHeader
                                    {
                                        Content =  string.Format(" {0} : {1} ( Count = {2}, Max Price = {3} )",
                                                gr.Header,
                                                val,
                                                gr.Items.Count(),
                                                gr.Items.Max(o => o.Price)),
                                        Collapsed = collapsed
                                    };
                            }
                    };

            return Json(builder.Build());
        }

        private object MakeFooter(GroupInfo<Lunch> info)
        {
            //will add the word Total at the grid level footer (Level == 0)
            var pref = info.Level == 0 ? "Total " : "";

            return new
                    {
                        Food = pref + " count = " + info.Items.Count(),
                        Location = info.Items.Select(o => o.Location).Distinct().Count() + " distinct locations",
                        Date = pref + " max: " + info.Items.Max(o => o.Date).Date.ToShortDateString(),
                        Price = info.Items.Sum(o => o.Price),
                        ChefCount = info.Items.Select(o => o.Chef.Id).Distinct().Count() + " chefs"
                    };
        }

        private string ToStr(object o)
        {
            return o is DateTime ? ((DateTime)o).ToShortDateString() : o.ToString();
        }
    }
    /*end*/
}