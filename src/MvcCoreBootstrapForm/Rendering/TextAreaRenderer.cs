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
            Element = this.TagBuilderFromHtmlContent(htmlHelper.TextAreaFor(expression, config.Rows));
            this.AddAttribute(Element, "rows", config.Rows.ToString());
            this.AddAttribute("disabled", config.Disabled);
            this.AddAttribute("readonly", config.ReadOnly);
            this.AddCssClasses(Element, config.CssClasses);

             return(this.RenderWithLabel(config, htmlHelper, expression));
       }
   }
}