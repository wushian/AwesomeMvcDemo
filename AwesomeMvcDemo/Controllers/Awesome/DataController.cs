using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.Utils;
using AwesomeMvcDemo.ViewModels.Display;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Awesome
{
    public class DataController : Controller
    {
        /*begin*/
        public ActionResult GetCategories()
        {
            return Json(Db.Categories.Select(o => new KeyContent(o.Id, o.Name)));
        }

        public ActionResult GetMeals(int[] categories)
        {
            categories = categories ?? new int[] { };

            var items = Db.Meals.Where(o => categories.Contains(o.Category.Id))
                .Select(o => new KeyContent(o.Id, o.Name));
            
            return Json(items);
        }
        /*end*/

        public ActionResult GetAllMeals()
        {
            var items = Db.Meals.Select(o => new KeyContent(o.Id, o.Name));
            return Json(items);
        }

        /*beginrso*/
        public ActionResult GetMealsInit(int? v)
        {
            var items = Db.Meals.Take(3).ToList();
            var selected = Db.Meals.SingleOrDefault(o => o.Id == v);

            if (selected != null && !items.Contains(selected))
            {
                items.Add(selected);
            }

            return Json(items.Select(o => new KeyContent(o.Id, o.Name)));
        }

        public ActionResult SearchMeals(string term)
        {
            var items = Db.Meals.Where(o => o.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0).Take(10).Select(o => new KeyContent(o.Id, o.Name));
            return Json(items);
        }
        /*endrso*/

        public ActionResult GetFruits()
        {
            var url = Url.Content(DemoUtils.FruitsUrl);
            var items = Db.Meals.Where(o => o.Category == Db.Categories.First())
                .Select(o => new FruitDisplay(o.Id, o.Name, url + o.Name + ".jpg"));
            return Json(items);
        }

        public ActionResult GetCountries()
        {
            var items = new List<KeyContent> { new KeyContent(string.Empty, "any country") };
            items.AddRange(Db.Countries.Select(o => new KeyContent(o.Id, o.Name)));

            return Json(items);
        }

        public ActionResult GetMealsSetValue(int[] categories)
        {
            categories = categories ?? new int[] { };

            var items = Db.Meals.Where(o => categories.Contains(o.Category.Id)).ToList();

            object value = null;
            if (items.Any())
            {
                value = new[] { items.Skip(1).First().Id };
            }

            return Json(new AweItems
            {
                Items = items.Select(o => new KeyContent(o.Id, o.Name)),
                Value = value
            });
        }

        /*beginsv*/
        public ActionResult GetMealsSetValue2(int[] categories)
        {
            categories = categories ?? new int[] { };

            var items = Db.Meals.Where(o => categories.Contains(o.Category.Id)).ToList();

            object value = null;
            if (items.Any())
            {
                value = items.Skip(1).First().Id;
            }

            return Json(new AweItems
            {
                Items = items.Select(o => new KeyContent(o.Id, o.Name)),
                Value = value
            });
        }
        /*endsv*/

        /*beginmlc*/
        public ActionResult GetNumbers(int[] parent)
        {
            parent = parent ?? new int[] { };

            var items = new[] { 3, 7, 10 };

            return Json(items.Select(o => o + parent.Sum()).Select(o => new KeyContent(o, o.ToString(CultureInfo.InvariantCulture))));
        }

        public ActionResult GetWords(string parent)
        {
            var items = new[] { "The", "brown", "cow", "is eating", "green", "grass" };

            return Json(items.Select(o => parent + " " + o).Select(o => new KeyContent(o, o)));
        }
        /*endmlc*/

        public ActionResult GetCategoriesFirstOption()
        {
            var list = new List<KeyContent> { new KeyContent("", "please select") };
            list.AddRange(Db.Categories.Select(o => new KeyContent(o.Id, o.Name)));
            return Json(list);
        }

        public ActionResult GetMealsList()
        {
            return Json(Db.Meals);
        }

        public ActionResult MealsGrid(GridParams g)
        {
            return Json(new GridModelBuilder<Meal>(Db.Meals.AsQueryable(), g) { Key = "Id" }.Build());
        }

        public ActionResult GetMenuNodes()
        {
            var menuNodes =
                MySiteMap.Items.Select(
                    o =>
                    new
                    {
                        o.Id,
                        o.Name,
                        o.Keywords,
                        ParentId = o.Parent != null ? o.Parent.Id : 0,
                        Url = o.Action != null ? Url.Action(o.Action, o.Controller) + o.Anchor : null,
                        o.Action,
                        o.Controller,
                        o.Collapsed
                    });

            return Json(menuNodes);
        }

        /*beginenum*/
        public ActionResult GetWeatherEnumItems()
        {
            var type = typeof(WeatherType);
            var items = Enum.GetValues(type).Cast<int>().Select(o => new KeyContent(o, SplitByCapLetter(Enum.GetName(type, o)))).ToList();

            // remove if not needed or if used with odropdown/ajaxradiolist
            items.Insert(0, new KeyContent(string.Empty, "please select"));

            return Json(items);
        }

        /// <summary>
        /// from HiThere to Hi There
        /// </summary>
        private string SplitByCapLetter(string s)
        {
            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            return r.Replace(s, " ");
        }
        /*endenum*/
    }
}
