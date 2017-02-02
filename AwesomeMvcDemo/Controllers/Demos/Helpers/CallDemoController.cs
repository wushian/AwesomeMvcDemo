using System.Web.Mvc;

namespace AwesomeMvcDemo.Controllers.Demos.Helpers
{
    public class CallDemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string SayHi()
        {
            return "server says hi";
        }

        public string SayName(string name, int id)
        {
            return "my name is " + name + "; id = " + id;
        }
    }
}