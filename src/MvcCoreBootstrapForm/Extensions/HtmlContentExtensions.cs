using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace MvcCoreBootstrapForm.Extensions
{
    internal static class HtmlContentExtensions
    {
        public static string AsString(this IHtmlContent htmlContent)
        {
            StringWriter writer = new StringWriter();

            htmlContent.WriteTo(writer, HtmlEncoder.Default);

            return(writer.ToString());
        }
    }
}
