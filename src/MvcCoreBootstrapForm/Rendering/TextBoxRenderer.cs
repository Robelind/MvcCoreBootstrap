using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class TextBoxRenderer<TModel, TResult> : ControlRenderer<TModel, TResult>
    {
        public IHtmlContent Render(TextInputConfig config, IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            TagBuilder label = !string.IsNullOrEmpty(config.Label) || config.AutoLabel
                ? this.TagBuilderFromHtmlContent(htmlHelper.LabelFor(expression, null, null), false)
                : null;

            Element = this.TagBuilderFromHtmlContent(htmlHelper.TextBoxFor(expression, null, null));
            if(!string.IsNullOrEmpty(config.Label))
            {
                label.InnerHtml.Clear();
                label.InnerHtml.Append(config.Label);
            }

            this.AddAttribute(Element, "placeholder", config.PlaceHolder);
            this.AddAttribute("disabled", config.Disabled);
            this.AddAttribute("readonly", config.ReadOnly);
            this.AddCssClasses(Element, config.CssClasses);

            return(this.RenderInGroup(label != null ? new [] {label, Element} : new [] {Element}));
        }
    }
}
