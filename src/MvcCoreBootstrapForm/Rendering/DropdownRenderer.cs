using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal interface IDropdownRenderer
    {
        IHtmlContent Render();
    }

    internal class DropdownRenderer<TModel, TResult> : ControlRenderer<TModel, TResult>, IDropdownRenderer
    {
        private readonly DropdownConfig _config;

        public DropdownRenderer(DropdownConfig config, IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        : base(config, htmlHelper, expression)
        {
            _config = config;
        }

        public IHtmlContent Render()
        {
            TResult propValue = Expression.Compile().Invoke(HtmlHelper.ViewData.Model);

            Element = this.TagBuilderFromHtmlContent(HtmlHelper.DropDownListFor(Expression, Enumerable.Empty<SelectListItem>()));
            if(_config.NoInitialSelection && !_config.Multiple)
            {
                Element.InnerHtml.AppendHtml("<option></option>");
            }
            foreach(var item in _config.Items)
            {
                string selected = item.Value == propValue.ToString() ? "selected" : null;

                Element.InnerHtml.AppendHtml($"<option value=\"{item.Value}\" {selected}>{item.Text}</option>");
            }
            this.AddAttribute("multiple", _config.Multiple);

            return(this.DoRender());
        }
    }
}