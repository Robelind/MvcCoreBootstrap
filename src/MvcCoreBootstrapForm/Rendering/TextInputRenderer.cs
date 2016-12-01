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
            TagBuilder prepend = this.AddOn(_config.Prepend);
            TagBuilder append = this.AddOn(_config.Append);
            TagBuilder inputGroup = prepend != null || append != null ? new TagBuilder("div") : null;

            Element = _config.Password
                ? this.TagBuilderFromHtmlContent(HtmlHelper.PasswordFor(Expression, null))
                : this.TagBuilderFromHtmlContent(HtmlHelper.TextBoxFor(Expression, null, null));
            this.AddAttribute("placeholder", _config.PlaceHolder);
            this.AddAttribute("readonly", _config.ReadOnly);

            if(inputGroup != null)
            {
                inputGroup.AddCssClass("input-group");
                this.AddInner(prepend, inputGroup);
                inputGroup.InnerHtml.AppendHtml(Element);
                this.AddInner(append, inputGroup);
            }

            return(this.DoRender(inputGroup));
        }

        private TagBuilder AddOn(string text)
        {
            TagBuilder addOn = !string.IsNullOrEmpty(text) ? new TagBuilder("span") : null;

            addOn?.AddCssClass("input-group-addon");
            addOn?.InnerHtml.Append(text);

            return(addOn);
        }
    }
}
