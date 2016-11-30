using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class CheckBoxRenderer<TModel> : ControlRenderer<TModel, bool>
    {
        private readonly CheckBoxConfig _config;

        public CheckBoxRenderer(CheckBoxConfig config, IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression)
        : base(config, htmlHelper, expression)
        {
            _config = config;
        }

        public IHtmlContent Render()
        {
            TagBuilder container = _config.Inline ? null : new TagBuilder("div");
            TagBuilder label = new TagBuilder("label");
            TagBuilder checkBox = this.TagBuilderFromHtmlContent(HtmlHelper.CheckBoxFor(Expression, null), false);
            TagBuilder propLabel = this.Label();
            ColumnWidths columnWidths = HtmlHelper.ViewBag.MvcBootStrapFormColumnWidths as ColumnWidths;
            TagBuilder element = container ?? label;

            if(container != null)
            {
                if(_config.Disabled)
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
            this.AddCssClass("checkbox-inline", _config.Inline, label);
            this.AddCssClasses(container, _config.CssClasses);

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