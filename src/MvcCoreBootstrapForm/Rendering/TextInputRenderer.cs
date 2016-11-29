using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class TextInputRenderer<TModel, TResult> : ControlRenderer<TModel, TResult>
    {
        public IHtmlContent Render(TextInputConfig config, IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            Element = this.TagBuilderFromHtmlContent(htmlHelper.TextBoxFor(expression, null, null));
            this.AddAttribute(Element, "placeholder", config.PlaceHolder);
            this.AddAttribute("disabled", config.Disabled);
            this.AddAttribute("readonly", config.ReadOnly);
            this.AddCssClasses(Element, config.CssClasses);

            return(this.RenderWithLabel(config, htmlHelper, expression));
        }
    }
}
