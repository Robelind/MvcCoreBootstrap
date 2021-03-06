﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class DropdownRenderer : ControlRenderer, ITypedControlRenderer, INonTypedControlRenderer
    {
        private readonly DropdownConfig _config;

        public DropdownRenderer(DropdownConfig config, ITooltipRenderer tooltipRenderer)
        : base(config, tooltipRenderer)
        {
            _config = config;
        }

        public override IHtmlContent Render<TModel, TResult>(IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            TResult propValue = expression.Compile().Invoke(htmlHelper.ViewData.Model);

            Element = this.TagBuilderFromHtmlContent(htmlHelper.DropDownListFor(expression, Enumerable.Empty<SelectListItem>(),
                null, _config.HtmlAttributes));
            this.CommonRender(propValue?.ToString());

            return(this.DoRender(htmlHelper, expression));
        }

        public override IHtmlContent Render(IHtmlContent element, IHtmlHelper htmlHelper = null)
        {
            TagBuilder dropdown = this.TagBuilderFromHtmlContent(element);

            Element = !_config.Items.Any() && !_config.Multiple
                ? dropdown
                : this.TagBuilderFromHtmlContent(htmlHelper.DropDownList(dropdown.Attributes["id"], _config.Items, _config.HtmlAttributes));
            this.CommonRender();

            return(this.DoRender());
        }

        private void CommonRender(string propValue = null)
        {
            if(_config.Default != null && !_config.Multiple)
            {
                Element.InnerHtml.AppendHtml($"<option>{_config.Default}</option>");
            }
            foreach(var item in _config.Items)
            {
                string selected = propValue != null && item.Value == propValue ? "selected" : null;

                Element.InnerHtml.AppendHtml($"<option value=\"{item.Value}\" {selected}>{item.Text}</option>");
            }
            this.AddAttribute("multiple", _config.Multiple);
            TooltipRenderer.Render(Element, _config.Tooltip);
        }
    }
}