using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class TextInputRenderer : ControlRenderer, ITypedControlRenderer, INonTypedControlRenderer
    {
        private readonly TextInputConfig _config;

        public TextInputRenderer(TextInputConfig config)
        : base(config)
        {
            _config = config;
        }

        public IHtmlContent Render<TModel, TResult>(IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression)
        {
            Element = _config.Password
                ? this.TagBuilderFromHtmlContent(htmlHelper.PasswordFor(expression, null))
                : this.TagBuilderFromHtmlContent(htmlHelper.TextBoxFor(expression, _config.Format, _config.HtmlAttributes));

            return(this.DoRender(htmlHelper, expression, this.CommonRender()));
        }

        public IHtmlContent Render(IHtmlContent element, IHtmlHelper htmlHelper)
        {
            Element = this.TagBuilderFromHtmlContent(element);

            return(this.DoRender(this.CommonRender()));
        }

        private TagBuilder CommonRender()
        {
            TagBuilder inputGroup = this.InputGroup(ig => ig.InnerHtml.AppendHtml(Element));

            this.AddAttribute("placeholder", _config.PlaceHolder);
            this.AddAttribute("readonly", _config.ReadOnly);

            return(inputGroup);
        }

        private TagBuilder InputGroup(Action<TagBuilder> elementAction)
        {
            TagBuilder prependIcon = this.AddOn(_config.PrependIcon, _config.PrependIconPrefix);
            TagBuilder prepend = prependIcon == null ? this.AddOn(_config.Prepend) : null;
            TagBuilder appendIcon = this.AddOn(_config.AppendIcon, _config.AppendIconPrefix);
            TagBuilder append = appendIcon == null ? this.AddOn(_config.Append) : null;
            TagBuilder inputGroup = prepend != null || append != null || prependIcon != null || appendIcon != null
                ? new TagBuilder("div")
                : null;

            if(inputGroup != null)
            {
                inputGroup.AddCssClass("input-group");
                this.AddInner(prependIcon, inputGroup);
                this.AddInner(prepend, inputGroup);
                elementAction(inputGroup);
                this.AddInner(appendIcon, inputGroup);
                this.AddInner(append, inputGroup);
            }

            return(inputGroup);
        }

        private TagBuilder AddOn(string text, string iconPrefix = null)
        {
            TagBuilder addOn = !string.IsNullOrEmpty(text) ? new TagBuilder("span") : null;

            addOn?.AddCssClass("input-group-addon");
            if(iconPrefix != null)
            {
                TagBuilder iconTag = new TagBuilder("span");

                iconTag.AddCssClass(iconPrefix);
                iconTag.AddCssClass($"{iconPrefix}-{text}");
                addOn?.InnerHtml.AppendHtml(iconTag);
            }
            else
            {
                addOn?.InnerHtml.Append(text);
            }

            return(addOn);
        }
    }
}
