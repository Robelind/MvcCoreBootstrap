using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class TextBoxRenderer<TModel, TResult> : ControlRenderer<TModel, TResult>
    {
        public IHtmlContent Render(TextInputConfig config, IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            TagBuilder textInput = this.TagBuilderFromHtmlContent(htmlHelper.TextBoxFor(expression, null, null));
            TagBuilder label = !string.IsNullOrEmpty(config.Label) || config.AutoLabel
                ? this.TagBuilderFromHtmlContent(htmlHelper.LabelFor(expression, null, null), false)
                : null;

            if(!string.IsNullOrEmpty(config.Label))
            {
                label.InnerHtml.Clear();
                label.InnerHtml.Append(config.Label);
            }

            this.AddAttribute(textInput, "placeholder", config.PlaceHolder);

            return(this.RenderInGroup(label != null ? new [] {label, textInput} : new [] {textInput}));
        }
    }
}
