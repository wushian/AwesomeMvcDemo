using System;
using System.Web.Mvc;

namespace AwesomeMvcDemo.Controllers.Demos.Generic
{
    public class ErrorHandlingDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowPopup()
        {
            throw new Exception("Popup example error message");
        }

        public ActionResult ShowPopupForm()
        {
            throw new Exception("PopupForm example error message");
        }
    }
}