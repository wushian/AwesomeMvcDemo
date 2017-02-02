using System;
using System.Linq;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.Controllers.Awesome.AjaxList
{
    public class DinnersListAjaxListController : Controller
    {
        public ActionResult Search(DateTime? date, int page)
        {
            const int PageSize = 5;
            if(!date.HasValue) date = DateTime.Now;
            var list = Db.Dinners.Where(o => o.Date <= date)
                         .OrderByDescending(o => o.Id);

            var r = new AjaxListResult
                {
                    Items = list.Skip((page - 1) * PageSize).Take(PageSize).Select(o => new KeyContent(o.Id, string.Format("{0} {1}", o.Name, o.Date.ToShortDateString()))),
                    More = list.Count() > page * PageSize
                };

            return Json(r);
        }
    }
}