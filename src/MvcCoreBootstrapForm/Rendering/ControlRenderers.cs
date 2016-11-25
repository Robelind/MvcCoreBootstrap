using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class TextBoxRenderer<TModel, TResult> : ControlRenderer<TModel, TResult>
    {
        public IHtmlContent Render(IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            return(this.Render(htmlHelper.TextBoxFor(expression, null, null), htmlHelper, expression));
        }
    }
}
