using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcCoreBootstrapForm.Rendering
{
    internal interface IHtmlParser
    {
        TagBuilder TagBuilderFromHtmlContent(IHtmlContent htmlContent, bool formControl = true);
    }

    internal class HtmlParser : IHtmlParser
    {
        public TagBuilder TagBuilderFromHtmlContent(IHtmlContent htmlContent, bool formControl = true)
        {
            TextWriter textWriter = new StringWriter();

            htmlContent.WriteTo(textWriter, HtmlEncoder.Default);

            string element = textWriter.ToString();
            int index = element.IndexOf(' ');
            TagBuilder tag = new TagBuilder(element.Substring(1, index - 1));

            element = element.Substring(index + 1);
            index = element.IndexOf("</");
            if(index != -1)
            {
                int index2 = element.IndexOf('>') + 1;

                // Element has a text value.
                tag.InnerHtml.Append(element.Substring(index2, index - index2));
                element = element.Substring(0, index2 - 1).Trim();
            }
            else
            {
                element = element.Replace("/>", "").Replace(">", "").Trim();
            }

            // Parse out the individual attributes.
            while(element.Length > 0)
            {
                int index2 = element.IndexOf('"', element.IndexOf('"') + 1);

                index = element.IndexOf('=');
                tag.Attributes.Add(element.Substring(0, index), element.Substring(index + 2, index2 - (index + 2)));
                element = element.Substring(index2 + 1);
            }

            if(formControl)
            {
                tag.AddCssClass("form-control");
            }

            return(tag);
        }
    }
}
