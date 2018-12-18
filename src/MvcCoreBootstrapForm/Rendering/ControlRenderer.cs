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
    internal abstract class ControlRenderer : RenderBase
    {
        protected readonly ControlConfig Config;

        protected ControlRenderer(ControlConfig config)
        {
            Config = config;
        }

        public virtual IHtmlContent Render<TModel, TResult>(IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            TagBuilder label = this.Label(htmlHelper, expression);
            FormConfig formConfig = htmlHelper.ViewBag.FormConfig as FormConfig;

            Debug.Assert(formConfig != null);
            Element = this.TagBuilderFromHtmlContent(this.HtmlElement(htmlHelper, expression));
            Debug.Assert(Element != null);
            this.Render();
            
            return(this.CommonRender(null, label, !formConfig.Inline));
        }

        public virtual IHtmlContent Render(IHtmlContent element, IHtmlHelper htmlHelper)
        {
            FormConfig formConfig = htmlHelper.ViewBag.FormConfig as FormConfig;

            Debug.Assert(formConfig != null);
            Element = this.TagBuilderFromHtmlContent(element);
            this.Render();

            return(this.CommonRender(null, this.Label(), !formConfig.Inline));
        }

        protected IHtmlContent DoRender<TModel, TResult>(IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, TagBuilder element = null)
        {
            TagBuilder label = this.Label(htmlHelper, expression);
            FormConfig formConfig = htmlHelper.ViewBag.FormConfig as FormConfig;

            Debug.Assert(formConfig != null);

            return(this.CommonRender(element, label, !formConfig.Inline));
        }

        protected IHtmlContent DoRender(TagBuilder element = null)
        {
            return(this.CommonRender(element, this.Label(), true));
        }

        protected virtual IHtmlContent HtmlElement<TModel, TResult>(IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression) {return(null);}
        protected virtual void Render() {}
        protected virtual TagBuilder Container() { return(null); }

        private IHtmlContent CommonRender(TagBuilder element, TagBuilder label, bool formGroup)
        {
            Element.AddCssClass("form-control");
            this.AddAttribute("disabled", Config.Disabled);
            this.AddCssClasses(Config.CssClasses);
            element = this.Container() ?? Element;

            if(formGroup)
            {
                TagBuilder group = new TagBuilder("div");
                TagBuilder widthContainer = this.ColumnWidths(label);
                TagBuilder elementContainer = widthContainer ?? group;

                group.AddCssClass("form-group");
                this.AddCssClass("row", Config.ColumnWidths != null, group);
                this.AddInner(label, group);
                this.AddInner(widthContainer, group);
                elementContainer.InnerHtml.AppendHtml(element);
                element = group;
            }

            return(element);
        }

        protected TagBuilder ColumnWidths(TagBuilder label)
        {
            TagBuilder widthContainer = null;
            ColumnWidths columnWidths = Config.ColumnWidths;

            if(Config.ColumnWidths != null)
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

            element = element.Substring(index + 1);
            if(element.Contains("/>"))
            {
                element = element.Substring(0, element.IndexOf("/>")).Trim();
            }
            else
            {
                // Element has a inner value.
                index = element.IndexOf('>');
                Debug.Assert(index != -1);
                tag.InnerHtml.Append(element.Substring(index + 1, element.IndexOf('<') - (index + 1)));
                element = element.Substring(0, index).Trim();
            }

            // Parse out the individual attributes.
            while(element.Length > 0)
            {
                int index2 = element.IndexOf('"', element.IndexOf('"') + 1);

                index = element.IndexOf('=');
                tag.Attributes.Add(element.Substring(0, index), element.Substring(index + 2, index2 - (index + 2)));
                element = element.Substring(index2 + 1);
            }

            return(tag);
        }

        protected TagBuilder Label<TModel, TResult>(IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, string cssClass = null)
        {
            TagBuilder label = null;

            if(Config.AutoLabel || !string.IsNullOrEmpty(Config.Label))
            {
                label = this.TagBuilderFromHtmlContent(htmlHelper.LabelFor(expression, Config.Label, null), false);
                //this.AddCssClass("col-form-label", Config.ColumnWidths != null, label);
                label.AddCssClass(cssClass ?? "col-form-label");
            }

            return (label);
        }

        protected TagBuilder Label(string cssClass = null)
        {
            TagBuilder label = null;

            if(!string.IsNullOrEmpty(Config.Label))
            {
                label = new TagBuilder("label");
                label.AddCssClass(cssClass ?? "col-form-label");
                label.InnerHtml.Append(Config.Label);
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