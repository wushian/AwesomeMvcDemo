using System.Threading.Tasks;
using System.Web.Mvc;
using AwesomeMvcDemo.ViewModels.Input;

namespace AwesomeMvcDemo.Controllers.Demos.Helpers
{
    public class FormDemoController : Controller
    {
        public ActionResult Index(FormDemoInput input)
        {
            ViewData["word"] = input.Word;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string word)
        {
            return RedirectToAction("Index", new { Word = word });
        }

/*begin*/
        [HttpPost]
        public ActionResult AskName(AskNameInput input)
        {
            if (input.Delay)
            {
                Task.Delay(2000).Wait();
            }

            if (!ModelState.IsValid) return View(input);
            
            return Json(new { Name = input.FName + " " + input.LName });
        }
/*end*/

        [HttpPost]
        public ActionResult FillForm(SayInput input)
        {
            return Content("You said " + input.SaySomething);
        }
    }
}