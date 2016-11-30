using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class RadioButtonsRenderer<TModel, TResult> : ControlRenderer<TModel, bool>
    {
        public IHtmlContent Render(RadioButtonsConfig<TModel, TResult> config, IHtmlHelper<TModel> htmlHelper)
        {
            TagBuilder group = new TagBuilder("div");
            TagBuilder groupLabel = new TagBuilder("label");
            ColumnWidths columnWidths = htmlHelper.ViewBag.MvcBootStrapFormColumnWidths as ColumnWidths;
            TagBuilder widthContainer = null;

            group.AddCssClass("form-group");
            group.InnerHtml.AppendHtml(groupLabel);
            if(columnWidths != null)
            {
                widthContainer = new TagBuilder("div");
                widthContainer.AddCssClass(columnWidths.RightColumn.CssClass());
                group.InnerHtml.AppendHtml(widthContainer);
            }
            groupLabel.AddCssClass("control-label");
            this.AddCssClass(columnWidths?.LeftColumn.CssClass(), columnWidths != null, groupLabel);
            groupLabel.InnerHtml.Append("Label"); // TODO
            foreach(RadioButtonConfig<TModel, TResult> radioButtonConfig in config.RadioButtons)
            {
                TagBuilder label = new TagBuilder("label");
                TagBuilder radioButton = this.TagBuilderFromHtmlContent(htmlHelper
                        .RadioButtonFor(radioButtonConfig.Expression, radioButtonConfig.Value), false);
                TagBuilder container = new TagBuilder("div");

                if(radioButtonConfig.Disabled)
                {
                    container.AddCssClass("disabled");
                    radioButton.Attributes.Add("disabled", null);
                }
                container.AddCssClass(config.Horizontal ? "radio-inline" : "radio");
                container.InnerHtml.AppendHtml(label);
                this.AddCssClasses(container, config.CssClasses);
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