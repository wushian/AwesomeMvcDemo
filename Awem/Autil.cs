using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Omu.Awem
{
    internal static class Autil
    {
        internal static string Serialize(object input)
        {
            return Json.Encode(input);
        }

        internal static CultureInfo CurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture;
        }
    }
}
