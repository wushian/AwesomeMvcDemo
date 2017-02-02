using System.Collections.Generic;

namespace AwesomeMvcDemo.Models
{
    public class DemoSettings
    {
        public static readonly string[] Themes = new[]
        {
            "wui",
            "bui",
            "met",
            "gui",
            "gui2",
            "gui3",
            "start",
            "black-cab"
        };

        public static readonly List<string> Popups = new List<string> { "awesome", "jQueryUI", "bootstrap", "inline" };

        public const string DefaultTheme = "wui";

        public const string DefaultPopup = "awesome";

        public const string MobileDefaultPopup = "awesome";

        public const string MobileDefaultTheme = "wui";

        public const string CookieName = "awedemset50";
    }
}