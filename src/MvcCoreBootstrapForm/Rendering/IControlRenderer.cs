using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcCoreBootstrapForm.Rendering
{
    internal interface ITypedControlRenderer
    {
        IHtmlContent Render<TModel, TResult>(IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression);
    }

    internal interface INonTypedControlRenderer
    {
        IHtmlContent Render(IHtmlContent element, IHtmlHelper htmlHelper = null);
    }
}