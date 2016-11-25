using System;
using System.Linq;
using System.IO;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;

namespace MvcCoreBootstrapForm.Rendering
{
    internal abstract class ControlRenderer<TModel, TResult> : RenderBase
    {
        protected IHtmlContent Render(IHtmlContent htmlContent, IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            TagBuilder tag = TagBuilderFromHtmlContent(htmlContent);
            
            tag.AddCssClass("form-control");

            return(tag);
        }

        protected TagBuilder TagBuilderFromHtmlContent(IHtmlContent htmlContent)
        {
            TextWriter textWriter = new StringWriter();

            htmlContent.WriteTo(textWriter, HtmlEncoder.Default);

            string[] parts = textWriter.ToString().Split(new[] {' '});
            TagBuilder tag = new TagBuilder(parts[0].Substring(1));
            foreach(var attribute in parts.Where(p => p.Contains('=')))
            {
                string[] attrParts = attribute.Split(new[] {'='});

                tag.Attributes.Add(attrParts[0], attrParts[1].Replace("\"", ""));
            }
            tag.AddCssClass("form-control");

            return(tag);
        }
    }
}