using Omu.AwesomeMvc;

namespace Omu.Awem.Helpers
{
    /// <summary>
    /// extensions for angular js
    /// </summary>
    public static class AngularExtensions
    {
        /// <summary>
        /// set angular ng-model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="h"></param>
        /// <param name="scopeProperty"></param>
        /// <returns></returns>
        public static AjaxRadioList<T> NgModel<T>(this AjaxRadioList<T> h, string scopeProperty)
        {
            // angular can't handle hidden type inputs, using text input with display:none instead
            h.HtmlAttributes(null, new { type = "text", ng_model = scopeProperty, style = "display:none;" });
            return h;
        }

        /// <summary>
        /// set angular ng-model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="h"></param>
        /// <param name="scopeProperty"></param>
        /// <returns></returns>
        public static AjaxDropdown<T> NgModel<T>(this AjaxDropdown<T> h, string scopeProperty)
        {
            h.HtmlAttributes(null, new { type = "text", ng_model = scopeProperty, style = "display:none;" });
            return h;
        }

        /// <summary>
        /// set angular ng-model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="h"></param>
        /// <param name="scopeProperty"></param>
        /// <returns></returns>
        public static AjaxCheckboxList<T> NgModel<T>(this AjaxCheckboxList<T> h, string scopeProperty)
        {
            h.HtmlAttributes(null, new { type = "text", ng_model = scopeProperty, style = "display:none;" });
            return h;
        }
    }
}