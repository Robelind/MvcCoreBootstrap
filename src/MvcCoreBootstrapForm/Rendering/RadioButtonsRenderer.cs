using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Extensions;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class RadioButtonsRenderer : ControlRenderer, ITypedControlRenderer, INonTypedControlRenderer
    {
        private readonly RadioButtonsConfig _config;

        public RadioButtonsRenderer(RadioButtonsConfig config, ITooltipRenderer tooltipRenderer)
        : base(config, tooltipRenderer)
        {
            _config = config;
        }

        public override IHtmlContent Render<TModel, TResult>(IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            TagBuilder label = this.Label(htmlHelper, expression);
            
            return(this.CommonRender(label, value => this.TagBuilderFromHtmlContent(htmlHelper
                        .RadioButtonFor(expression, value), false)));
        }

        public override IHtmlContent Render(IHtmlContent element, IHtmlHelper htmlHelper)
        {
            TagBuilder radioBtnBase = this.TagBuilderFromHtmlContent(element, false);
            TagBuilder label = this.Label();
            
            return(this.CommonRender(label, value => htmlHelper.RadioButton(radioBtnBase.Attributes["id"], value)));
        }

        private IHtmlContent CommonRender(TagBuilder groupLabel, Func<object, IHtmlContent> radioBtnFunc)
        {
            TagBuilder group = new TagBuilder("div");
            ColumnWidths columnWidths = _config.ColumnWidths;
            TagBuilder widthContainer = null;

            group.AddCssClass("form-group");
            group.InnerHtml.AppendHtml(groupLabel);
            if(columnWidths != null)
            {
                widthContainer = new TagBuilder("div");
                widthContainer.AddCssClass(columnWidths.RightColumn.CssClass());
                group.InnerHtml.AppendHtml(widthContainer);
            }
            this.AddCssClass(columnWidths?.LeftColumn.CssClass(), columnWidths != null && groupLabel != null, groupLabel);
            foreach(RadioButtonConfig radioButtonConfig in _config.RadioButtons)
            {
                TagBuilder container = new TagBuilder("div");
                TagBuilder radioButton = this.TagBuilderFromHtmlContent(radioBtnFunc(radioButtonConfig.Value), false);
                TagBuilder label = new TagBuilder("label");

                this.AddAttribute("disabled", _config.Disabled, radioButton);
                radioButton.AddCssClass("form-check-input");
                label.AddCssClass("form-check-label");
                container.AddCssClass("form-check");
                container.InnerHtml.AppendHtml(radioButton);
                container.InnerHtml.AppendHtml(label);
                this.AddCssClasses(radioButtonConfig.CssClasses, container);
                this.AddCssClass("form-check-inline", _config.Horizontal, container);

                if(!string.IsNullOrEmpty(radioButtonConfig.Label))
                {
                    label.InnerHtml.Append(radioButtonConfig.Label);
                }

                (widthContainer ?? group).InnerHtml.AppendHtml(container);
                TooltipRenderer.Render(radioButton, radioButtonConfig.Tooltip);
            }

            return (group);
        }
    }
}