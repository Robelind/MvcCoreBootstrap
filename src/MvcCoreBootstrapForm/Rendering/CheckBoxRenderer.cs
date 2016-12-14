using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Extensions;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class CheckBoxRenderer : ControlRenderer, IControlRenderer2
    {
        private readonly CheckBoxConfig _config;

        public CheckBoxRenderer(CheckBoxConfig config)
        : base(config)
        {
            _config = config;
        }

        public IHtmlContent Render<TModel>(IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression)
        {
            TagBuilder checkBox = this.TagBuilderFromHtmlContent(htmlHelper.CheckBoxFor(expression, _config.HtmlAttributes), false);
            TagBuilder propLabel = this.Label(htmlHelper, expression);

            return(this.CommonRender(checkBox, propLabel));
        }

        public IHtmlContent Render(IHtmlContent element, IHtmlHelper htmlHelper)
        {
            TagBuilder checkbox = this.TagBuilderFromHtmlContent(element);
            TagBuilder label = this.Label();

            return(this.CommonRender(checkbox, label));
        }

        private TagBuilder CommonRender(TagBuilder checkBox, TagBuilder label)
        {
            TagBuilder container = _config.Horizontal ? null : new TagBuilder("div");
            ColumnWidths columnWidths = _config.ColumnWidths;
            TagBuilder clabel = new TagBuilder("label");
            TagBuilder element = container ?? clabel;

            if(container != null)
            {
                container.AddCssClass("checkbox");
                this.AddCssClass("disabled", _config.Disabled, container);
                container.InnerHtml.AppendHtml(clabel);
            }
            //label.AddCssClass("control-label");
            this.AddCssClass("disabled", _config.Disabled, checkBox);
            clabel.InnerHtml.AppendHtml(checkBox);
            clabel.InnerHtml.AppendHtml(label?.InnerHtml);
            this.AddCssClass("checkbox-inline", _config.Horizontal, clabel);
            this.AddCssClasses(_config.CssClasses, container);

            if(columnWidths != null)
            {
                TagBuilder group = new TagBuilder("div");
                TagBuilder widthContainer = new TagBuilder("div");
                string[] colClassParts = columnWidths.LeftColumn.CssClass().Split(new[] {'-'});

                //  Set an offset column class, since check boxes doesn't have labels to the left.
                widthContainer.AddCssClass($"col-{colClassParts[1]}-offset-{colClassParts[2]}");
                widthContainer.AddCssClass(columnWidths.RightColumn.CssClass());
                group.AddCssClass("form-group");
                widthContainer.InnerHtml.AppendHtml(container ?? clabel);
                group.InnerHtml.AppendHtml(widthContainer);
                element = group;
            }

            return(element);
        }
    }
}