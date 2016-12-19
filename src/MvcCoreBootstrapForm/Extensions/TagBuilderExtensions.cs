using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcCoreBootstrapForm.Extensions
{
    internal static class TagBuilderExtensions
    {
        public static string AsString(this TagBuilder tagBuilder)
        {
            StringWriter writer = new StringWriter();

            tagBuilder.WriteTo(writer, HtmlEncoder.Default);

            return(writer.ToString());
        }
    }
}
