using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Extensions;

namespace MvcCoreBootstrapForm.Rendering
{
    internal abstract class ControlRenderer<TModel, TResult> : RenderBase
    {
        protected readonly ControlConfig Config;
        protected readonly IHtmlHelper<TModel> HtmlHelper;
        protected readonly Expression<Func<TModel, TResult>> Expression;

        protected ControlRenderer(ControlConfig config, IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            Config = config;
            HtmlHelper = htmlHelper;
            Expression = expression;
        }

        protected IHtmlContent DoRender()
        {
            Element.AddCssClass("form-control");
            this.AddAttribute("disabled", Config.Disabled);
            this.AddCssClasses(Config.CssClasses);

            return(this.RenderWithLabel());
        }

        protected IHtmlContent RenderWithLabel()
        {
            TagBuilder element = Element;
            ColumnWidths columnWidths = HtmlHelper.ViewBag.MvcBootStrapFormColumnWidths as ColumnWidths;
            TagBuilder label = this.Label();
            TagBuilder group = null;

            if(label != null)
            {
                group = new TagBuilder("div");
                this.AddCssClass(columnWidths?.LeftColumn.CssClass(), columnWidths != null, label); // TODO
                group.AddCssClass("form-group");
                group.InnerHtml.AppendHtml(label);
                element = group;
            }

            if(columnWidths != null)
            {
                TagBuilder widthContainer = new TagBuilder("div");

                widthContainer.AddCssClass(columnWidths.RightColumn.CssClass());
                widthContainer.InnerHtml.AppendHtml(Element);
                group?.InnerHtml.AppendHtml(widthContainer);
            }
            else
            {
                group?.InnerHtml.AppendHtml(Element);
            }

            return(element);
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

            //this.AddCssClass("form-control", formControl, tag);

            return(tag);
        }

        protected TagBuilder Label()
        {
            TagBuilder label = null;

            if(Config.AutoLabel || !string.IsNullOrEmpty(Config.Label))
            {
                label = this.TagBuilderFromHtmlContent(HtmlHelper.LabelFor(Expression, null, null), false);
                if(!string.IsNullOrEmpty(Config.Label))
                {
                    label.InnerHtml.Clear();
                    label.InnerHtml.Append(Config.Label);
                }
                label.AddCssClass("control-label");
            }

            return(label);
        }

        protected string ValidationJs { get; } =
            @"$('#{0}').closest('form').bind('invalid-form.validate', function () {{
            $('#{0}').show();
            if ($('#{0} ul').children().length > 1) {{
                $('#{0} ul').show();
                $('#{0} span').hide();
            }} else {{
                $('#{0} span').html($('#{0} ul li').first().text());
                $('#{0} span').show();
                $('#{0} ul').hide();
            }}
        }});";
    }
}