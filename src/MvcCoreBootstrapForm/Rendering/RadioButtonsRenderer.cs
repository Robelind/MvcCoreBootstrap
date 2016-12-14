using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Extensions;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class RadioButtonsRenderer : ControlRenderer, IControlRenderer, IControlRenderer2
    {
        private readonly RadioButtonsConfig _config;

        public RadioButtonsRenderer(RadioButtonsConfig config)
        : base(config)
        {
            _config = config;
        }

        public IHtmlContent Render<TModel, TResult>(IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            TagBuilder label = this.Label(htmlHelper, expression);
            
            return(this.CommonRender(label, value => this.TagBuilderFromHtmlContent(htmlHelper
                        .RadioButtonFor(expression, value), false)));
        }

        public IHtmlContent Render(IHtmlContent element, IHtmlHelper htmlHelper)
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
                TagBuilder label = new TagBuilder("label");
                TagBuilder radioButton = this.TagBuilderFromHtmlContent(radioBtnFunc(radioButtonConfig.Value), false);
                TagBuilder container = !_config.Horizontal ? new TagBuilder("div") : null;

                if(radioButtonConfig.Disabled || _config.Disabled)
                {
                    container?.AddCssClass("disabled");
                    label.AddCssClass("disabled");
                    radioButton.Attributes.Add("disabled", null);
                }
                if(container != null)
                {
                    container.AddCssClass("radio");
                    container.InnerHtml.AppendHtml(label);
                    this.AddCssClasses(radioButtonConfig.CssClasses, container);
                }
                this.AddCssClass("radio-inline", _config.Horizontal, label);
                label.InnerHtml.AppendHtml(radioButton);
                if(!string.IsNullOrEmpty(radioButtonConfig.Label))
                {
                    label.InnerHtml.Append(radioButtonConfig.Label);
                }

                (widthContainer ?? group).InnerHtml.AppendHtml(container ?? label);
            }

            return(group);
        }
    }
}