using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal interface ITextAreaRenderer
    {
        IHtmlContent Render();
    }

    internal class TextAreaRenderer<TModel, TResult> : ControlRenderer<TModel, TResult>, ITextAreaRenderer
    {
        private readonly TextAreaConfig _config;

        public TextAreaRenderer(TextAreaConfig config, IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        : base(config, htmlHelper, expression)
        {
            _config = config;
        }

        public IHtmlContent Render()
        {
            Element = this.TagBuilderFromHtmlContent(HtmlHelper.TextAreaFor(Expression, _config.Rows));
            this.AddAttribute("rows", _config.Rows.ToString());
            this.AddAttribute("readonly", _config.ReadOnly);

            return(this.DoRender());
       }
    }
}