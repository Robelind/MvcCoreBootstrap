using System;
using System.Collections.Generic;
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

        protected IHtmlContent RenderInGroup(IEnumerable<TagBuilder> elements)
        {
            TagBuilder group = new TagBuilder("div");
            
            group.AddCssClass("form-group");
            foreach(TagBuilder element in elements)
            {
                group.InnerHtml.AppendHtml(element);
            }

            return(group);
        }

        protected TagBuilder TagBuilderFromHtmlContent(IHtmlContent htmlContent, bool formControl = true)
        {
            TextWriter textWriter = new StringWriter();

            htmlContent.WriteTo(textWriter, HtmlEncoder.Default);

            string element = textWriter.ToString();
            int index = element.IndexOf(' ');
            TagBuilder tag = new TagBuilder(element.Substring(1, index - 1));

            element = element.Substring(index + 1, element.IndexOf('/') - (index + 1)).Trim();
            index = element.IndexOf('>');
            if(index != -1)
            {
                // Element has a text value.
                tag.InnerHtml.Append(element.Substring(index + 1, element.IndexOf('<') - (index + 1)));
                element = element.Substring(0, index);
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
