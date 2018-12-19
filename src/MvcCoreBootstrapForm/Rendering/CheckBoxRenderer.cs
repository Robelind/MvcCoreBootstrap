using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Extensions;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class CheckBoxRenderer : ControlRenderer, INonTypedControlRenderer
    {
        private readonly CheckBoxConfig _config;

        public CheckBoxRenderer(CheckBoxConfig config, ITooltipRenderer tooltipRenderer)
        : base(config, tooltipRenderer)
        {
            _config = config;
        }

        public IHtmlContent Render<TModel>(IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression)
        {
            TagBuilder checkBox = this.TagBuilderFromHtmlContent(htmlHelper.CheckBoxFor(expression, _config.HtmlAttributes), false);
            TagBuilder propLabel = this.Label(htmlHelper, expression, "form-check-label");

            return(this.CommonRender(checkBox, propLabel));
        }

        public IHtmlContent Render(IHtmlContent element, IHtmlHelper htmlHelper)
        {
            TagBuilder checkbox = this.TagBuilderFromHtmlContent(element);
            TagBuilder label = this.Label("form-check-label");

            return(this.CommonRender(checkbox, label));
        }

        private TagBuilder CommonRender(TagBuilder checkBox, TagBuilder label)
        {
            TagBuilder container = new TagBuilder("div");
            TagBuilder element = container;

            container.AddCssClass("form-check");
            this.AddCssClass("form-check-inline", _config.Horizontal, container);
            container.InnerHtml.AppendHtml(checkBox);
            container.InnerHtml.AppendHtml(label);

            this.AddAttribute("disabled", _config.Disabled, checkBox);
            checkBox.AddCssClass("form-check-input");
            this.AddCssClasses(_config.CssClasses, container);

            if(_config.ColumnWidths != null)
            {
                TagBuilder group = new TagBuilder("div");
                TagBuilder widthContainer = new TagBuilder("div");
                TagBuilder offsetColum = new TagBuilder("div");
                //string[] colClassParts = columnWidths.LeftColumn.CssClass().Split(new[] {'-'});

                //  Set an offset column class, since check boxes doesn't have labels to the left.
                //widthContainer.AddCssClass($"col-{colClassParts[1]}-offset-{colClassParts[2]}");
                widthContainer.AddCssClass(_config.ColumnWidths.RightColumn.CssClass());
                offsetColum.AddCssClass(_config.ColumnWidths.LeftColumn.CssClass());
                group.AddCssClass("form-group row");
                widthContainer.InnerHtml.AppendHtml(container);
                group.InnerHtml.AppendHtml(offsetColum);
                group.InnerHtml.AppendHtml(widthContainer);
                element = group;
            }
            TooltipRenderer.Render(checkBox, _config.Tooltip);

            return(element);
        }
    }
}