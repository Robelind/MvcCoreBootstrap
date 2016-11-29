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
            TResult propValue = expression.Compile().Invoke(htmlHelper.ViewData.Model);

            Element = this.TagBuilderFromHtmlContent(htmlHelper.DropDownListFor(expression, Enumerable.Empty<SelectListItem>()));
            this.BaseConfig(config);
            if(config.NoInitialSelection && !config.Multiple)
            {
                Element.InnerHtml.AppendHtml("<option></option>");
            }
            foreach(var item in config.Items)
            {
                string selected = item.Value == propValue.ToString() ? "selected" : null;

                Element.InnerHtml.AppendHtml($"<option value=\"{item.Value}\" {selected}>{item.Text}</option>");
            }
            this.AddAttribute("multiple", config.Multiple);
            this.AddAttribute("disabled", config.Disabled);
            this.AddCssClasses(Element, config.CssClasses);

            return(this.RenderWithLabel(config, htmlHelper, expression));
        }
   }
}