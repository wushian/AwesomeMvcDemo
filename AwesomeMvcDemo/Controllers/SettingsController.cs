using AwesomeMvcDemo.Models;
using AwesomeMvcDemo.Utils;
using AwesomeMvcDemo.ViewModels.Input.Settings;
using Omu.AwesomeMvc;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AwesomeMvcDemo.Controllers
{
    public class SettingsController : Controller
    {
        public ActionResult Index()
        {
            var settings = ReadSettings(HttpContext.ApplicationInstance.Request);

            var vm = new SettingsInput
                {
                    Themes = DemoSettings.Themes.Select(theme => new KeyContent(theme, theme)),
                    SelectedTheme = settings.Theme,
                    Popups = DemoSettings.Popups.Select(o => new KeyContent(o, o)),
                    SelectedPopup = settings.PopupMod
                };

            return PartialView(vm);
        }

        public static SettingsVal ReadSettings(HttpRequest request)
        {
            var settings = new SettingsVal();

            if (MobileUtils.IsMobileOrTablet())
            {
                settings.Theme = DemoSettings.MobileDefaultTheme;
                settings.PopupMod = DemoSettings.MobileDefaultPopup;
            }

            if (request.Cookies[DemoSettings.CookieName] != null)
            {
                var vals = request.Cookies[DemoSettings.CookieName].Value.Split('|');
                if (vals.Length == 2)
                {
                    settings.Theme = vals[0];
                    settings.PopupMod = vals[1];
                }
            }

            if (string.IsNullOrWhiteSpace(settings.Theme))
            {
                settings.Theme = DemoSettings.DefaultTheme;
            }

            if (string.IsNullOrWhiteSpace(settings.PopupMod))
            {
                settings.PopupMod = DemoSettings.DefaultPopup;
            }

            return settings;
        }
    }
}