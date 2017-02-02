using System;
using System.Collections.Generic;
using System.Web.Mvc;

using AwesomeMvcDemo.Models;

namespace AwesomeMvcDemo.Controllers.Demos.Grid
{
    public class MailboxDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                Db.Delete<Message>(id);    
            }
            return Json(new { });
        }

        public ActionResult ReadMessage(int id)
        {
            var msg = Db.Get<Message>(id);
            msg.IsRead = true;
            Db.Update(msg);

            return PartialView(msg);
        }

        [HttpPost]
        public ActionResult MarkRead(int[] ids)
        {
            foreach (var id in ids)
            {
                var msg = Db.Get<Message>(id);
                msg.IsRead = true;
                Db.Update(msg);
            }

            return Json(new { });
        }

        [HttpPost]
        public ActionResult MarkUnread(int[] ids)
        {
            foreach (var id in ids)
            {
                var msg = Db.Get<Message>(id);
                msg.IsRead = false;
                Db.Update(msg);
            }

            return Json(new { });
        }
    }
}