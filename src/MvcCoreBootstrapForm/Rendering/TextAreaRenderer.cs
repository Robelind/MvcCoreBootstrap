using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal interface ITextAreaRenderer
    {
        IHtmlContent Render<TModel, TResult>(IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression);
    }

    internal class TextAreaRenderer : ControlRenderer, IControlRenderer, IControlRenderer2
    {
        private readonly TextAreaConfig _config;

        public TextAreaRenderer(TextAreaConfig config)
        : base(config)
        {
            _config = config;
        }

        public IHtmlContent Render<TModel, TResult>(IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            Element = this.TagBuilderFromHtmlContent(htmlHelper.TextAreaFor(expression, _config.Rows, 1, _config.HtmlAttributes));
            this.CommonRender();

            return(this.DoRender(htmlHelper, expression));
       }

        public IHtmlContent Render(IHtmlContent element, IHtmlHelper htmlHelper)
        {
            Element = this.TagBuilderFromHtmlContent(element);
            this.CommonRender();

            return(this.DoRender());
        }

        private void CommonRender()
        {
            this.AddAttribute("rows", _config.Rows.ToString());
            this.AddAttribute("readonly", _config.ReadOnly);
        }
    }
}