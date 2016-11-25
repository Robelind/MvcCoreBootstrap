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

            this.AddAttribute(textInput, "placeholder", config.PlaceHolder);

            return(textInput);
        }
    }
}
