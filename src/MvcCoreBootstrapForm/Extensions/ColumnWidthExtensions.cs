using System;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Extensions
{
    internal static class ColumnWidthExtensions
    {
        public static string CssClass(this ColumnWidth columnWidth)
        {
            string width = Enum.GetName(typeof(ColumnWidth), columnWidth);

            return($"col-{width.Substring(0, 2)}-{width.Substring(2)}");
        }
    }
}
