using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class TextInputRenderer<TModel, TResult> : ControlRenderer<TModel, TResult>
    {
        private readonly TextInputConfig _config;

        public TextInputRenderer(TextInputConfig config, IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        : base(config, htmlHelper, expression)
        {
            _config = config;
        }

        public IHtmlContent Render()
        {
            Element = this.TagBuilderFromHtmlContent(HtmlHelper.TextBoxFor(Expression, null, null));
            this.AddAttribute("placeholder", _config.PlaceHolder);
            this.AddAttribute("disabled", _config.Disabled);
            this.AddAttribute("readonly", _config.ReadOnly);

            return(this.DoRender());
        }
    }
}
