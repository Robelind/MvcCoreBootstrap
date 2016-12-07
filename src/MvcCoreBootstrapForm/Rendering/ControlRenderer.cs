using System;
using System.Diagnostics;
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

        protected IHtmlContent DoRender(TagBuilder element = null)
        {
            TagBuilder group = new TagBuilder("div");
            TagBuilder label = this.Label();
            TagBuilder widthContainer = this.ColumnWidths(label);
            TagBuilder elementContainer = widthContainer ?? group;

            Element.AddCssClass("form-control");
            this.AddAttribute("disabled", Config.Disabled);
            this.AddCssClasses(Config.CssClasses);
            group.AddCssClass("form-group");
            this.AddInner(label, group);
            this.AddInner(widthContainer, group);
            elementContainer.InnerHtml.AppendHtml(element ?? Element);

            return(group);
        }

        protected TagBuilder ColumnWidths(TagBuilder label)
        {
            TagBuilder widthContainer = null;
            ColumnWidths columnWidths = HtmlHelper.ViewBag.MvcBootStrapFormColumnWidths as ColumnWidths;

            if(columnWidths != null)
            {
                widthContainer = new TagBuilder("div");
                widthContainer.AddCssClass(columnWidths.RightColumn.CssClass());
                this.AddCssClass(columnWidths.LeftColumn.CssClass(), label != null, label);
            }

            return(widthContainer);
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
                FormSetup formSetup = HtmlHelper.ViewBag.FormSetup as FormSetup;

                Debug.Assert(formSetup != null);
                label = this.TagBuilderFromHtmlContent(HtmlHelper.LabelFor(Expression, Config.Label, null), false);
                this.AddCssClass("control-label", formSetup.Horizontal, label);
            }

            return(label);
        }

        protected void AddInner(TagBuilder inner, TagBuilder element = null)
        {
            if(inner != null)
            {
                (element ?? Element).InnerHtml.AppendHtml(inner);
            }
        }
    }
}