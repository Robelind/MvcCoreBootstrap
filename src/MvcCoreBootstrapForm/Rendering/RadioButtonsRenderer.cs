using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class RadioButtonsRenderer<TModel, TResult> : ControlRenderer<TModel, TResult>
    {
        private readonly RadioButtonsConfig<TModel, TResult> _config;

        public RadioButtonsRenderer(RadioButtonsConfig<TModel, TResult> config, IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        : base(config, htmlHelper, expression)
        {
            _config = config;
        }

        public IHtmlContent Render()
        {
            TagBuilder group = new TagBuilder("div");
            TagBuilder groupLabel = this.Label();
            ColumnWidths columnWidths = HtmlHelper.ViewBag.MvcBootStrapFormColumnWidths as ColumnWidths;
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
            foreach(RadioButtonConfig<TModel, TResult> radioButtonConfig in _config.RadioButtons)
            {
                TagBuilder label = new TagBuilder("label");
                TagBuilder radioButton = this.TagBuilderFromHtmlContent(HtmlHelper
                        .RadioButtonFor(radioButtonConfig.Expression, radioButtonConfig.Value), false);
                TagBuilder container = new TagBuilder("div");

                if(radioButtonConfig.Disabled || _config.Disabled)
                {
                    container.AddCssClass("disabled");
                    radioButton.Attributes.Add("disabled", null);
                }
                container.AddCssClass(_config.Horizontal ? "radio-inline" : "radio");
                container.InnerHtml.AppendHtml(label);
                this.AddCssClasses(_config.CssClasses, container);
                label.InnerHtml.AppendHtml(radioButton);
                //label.AddCssClass("control-label");
                if(!string.IsNullOrEmpty(radioButtonConfig.Label))
                {
                    label.InnerHtml.Append(radioButtonConfig.Label);
                }

                (widthContainer ?? group).InnerHtml.AppendHtml(container);
            }

            return(group);
        }
    }
}