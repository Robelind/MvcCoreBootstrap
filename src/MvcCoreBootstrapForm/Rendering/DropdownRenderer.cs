using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal interface IDropdownRenderer<TModel, TResult>
    {
        IHtmlContent Render(DropdownConfig config, IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression);
    }

    internal class DropdownRenderer<TModel, TResult> : ControlRenderer<TModel, TResult>, IDropdownRenderer<TModel, TResult>
    {
        public IHtmlContent Render(DropdownConfig config, IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            Element = this.TagBuilderFromHtmlContent(htmlHelper.DropDownListFor(expression, Enumerable.Empty<SelectListItem>()));
            this.BaseConfig(config);
            foreach(var item in config.Items)
            {
                Element.InnerHtml.AppendHtml($"<option value=\"{item.Value}\">{item.Text}</option>");
            }
            this.AddCssClasses(Element, config.CssClasses);

            return(Element);
        }

   }
}