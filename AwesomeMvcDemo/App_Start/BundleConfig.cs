using System.Web.Optimization;

namespace AwesomeMvcDemo.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundle/Scripts/js").Include(
                "~/Scripts/AwesomeMvc.js",
                "~/Scripts/awem.js",
                "~/Scripts/utils.js",
                "~/Scripts/Site.js")
                );
        }
    }
}