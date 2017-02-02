using System.Linq;
using System.Web.Mvc;

namespace Omu.Awem.Utils
{
    /// <summary>
    /// controller extensions utilities methods
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// get modelstate errors in the format required for the inline edit grid mod
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static object GetErrorsInline(this ModelStateDictionary modelState)
        {
            var errorList = modelState.Where(o => o.Value.Errors.Count > 0)
                                .ToDictionary(
                                kvp => kvp.Key,
                                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());

            return new { e = errorList };
        }
    }
}