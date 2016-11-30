using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class CheckBoxRenderer<TModel> : ControlRenderer<TModel, bool>
    {
        public IHtmlContent Render(CheckBoxConfig config, IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression)
        {
            TagBuilder container = config.Inline ? null : new TagBuilder("div");
            TagBuilder label = new TagBuilder("label");
            TagBuilder checkBox = this.TagBuilderFromHtmlContent(htmlHelper.CheckBoxFor(expression, null), false);
            TagBuilder propLabel = this.Label(config, htmlHelper, expression);
            ColumnWidths columnWidths = htmlHelper.ViewBag.MvcBootStrapFormColumnWidths as ColumnWidths;
            TagBuilder element = container ?? label;

            if(container != null)
            {
                if(config.Disabled)
                {
                    container.AddCssClass("disabled");
                    checkBox.Attributes.Add("disabled", null);
                }
                container.AddCssClass("checkbox");
                container.InnerHtml.AppendHtml(label);
            }
            //label.AddCssClass("control-label");
            label.InnerHtml.AppendHtml(checkBox);
            label.InnerHtml.AppendHtml(propLabel?.InnerHtml);
            this.AddCssClass("checkbox-inline", config.Inline, label);
            this.AddCssClasses(container, config.CssClasses);

            if(columnWidths != null)
            {
                TagBuilder group = new TagBuilder("div");
                TagBuilder widthContainer = new TagBuilder("div");
                string[] colClassParts = columnWidths.LeftColumn.CssClass().Split(new[] {'-'});

                //  Set an offset column class, since check boxes doesn't have labels to the left.
                widthContainer.AddCssClass($"col-{colClassParts[1]}-offset-{colClassParts[2]}");
                widthContainer.AddCssClass(columnWidths.RightColumn.CssClass());
                group.AddCssClass("form-group");
                widthContainer.InnerHtml.AppendHtml(container ?? label);
                group.InnerHtml.AppendHtml(widthContainer);
                element = group;
            }

            return(element);
        }
    }
}