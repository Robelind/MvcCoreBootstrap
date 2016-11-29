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
            TagBuilder label = !string.IsNullOrEmpty(config.Label) || config.AutoLabel
                ? this.TagBuilderFromHtmlContent(htmlHelper.LabelFor(expression, null, null), false)
                : null;
            TResult propValue = expression.Compile().Invoke(htmlHelper.ViewData.Model);

            Element = this.TagBuilderFromHtmlContent(htmlHelper.DropDownListFor(expression, Enumerable.Empty<SelectListItem>()));
            this.BaseConfig(config);
            if(!string.IsNullOrEmpty(config.Label))
            {
                label.InnerHtml.Clear();
                label.InnerHtml.Append(config.Label);
            }
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

            return(this.RenderInGroup(label != null ? new [] {label, Element} : new [] {Element}));
        }
   }
}