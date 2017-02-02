using System;
using Omu.AwesomeMvc;

namespace Omu.Awem.Helpers
{
    /// <summary>
    /// extensions for inline helpers
    /// </summary>
    public static class AweHelpersExtensions
    {
        /// <summary>
        /// get format for grid inline edit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="valueProp">grid model property to get value from</param>
        /// <returns></returns>
        public static string AsInline<T>(this T helper, string valueProp = null) where T : IAwesomeHelper<object>
        {
            helper.Prefix("#Prefix");

            if (valueProp != null)
            {
                helper.Svalue(".(" + valueProp + ")");
            }
            else
            {
                helper.Svalue(".(" + helper.Awe.Prop + ")");
            }

            return helper + string.Format("<div class=\"awe-gvalidmsg {0}\"></div>", helper.Awe.Prop);
        }

        /// <summary>
        /// Set parent for grid inline editing editor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="parent"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static T ParentInline<T>(this T helper, string parent, string alias = "parent") where T : IAwesomeHelperParent<object>
        {
            helper.Parent("#Prefix" + parent, alias);
            return helper;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="text"></param>
        /// <param name="click"></param>
        /// <param name="setCfg"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Button<T>(this T helper, string text, string click, Action<PopupButtonCfg> setCfg) where T : PopupBase<object, T>
        {
            var cfg = new PopupButtonCfg();

            if (setCfg != null)
            {
                setCfg(cfg);
            }

            return helper.Button(text, click, cfg.ToTag());
        }
    }
}