using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal interface ITextAreaRenderer<TModel, TResult>
    {
        IHtmlContent Render(TextAreaConfig config, IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression);
    }

    internal class TextAreaRenderer<TModel, TResult> : ControlRenderer<TModel, TResult>, ITextAreaRenderer<TModel, TResult>
    {
        public IHtmlContent Render(TextAreaConfig config, IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            TagBuilder label = !string.IsNullOrEmpty(config.Label) || config.AutoLabel
                ? this.TagBuilderFromHtmlContent(htmlHelper.LabelFor(expression, null, null), false)
                : null;

            Element = this.TagBuilderFromHtmlContent(htmlHelper.TextAreaFor(expression, config.Rows));
            if(!string.IsNullOrEmpty(config.Label))
            {
                label.InnerHtml.Clear();
                label.InnerHtml.Append(config.Label);
            }

            this.AddAttribute(Element, "rows", config.Rows.ToString());
            this.AddAttribute("disabled", config.Disabled);
            this.AddAttribute("readonly", config.ReadOnly);
            this.AddCssClasses(Element, config.CssClasses);

            return(this.RenderInGroup(label != null ? new [] {label, Element} : new [] {Element}));
        }
   }
}