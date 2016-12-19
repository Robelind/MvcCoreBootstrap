using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Rendering;

namespace MvcCoreBootstrapForm
{
    internal static class ControlFactory
    {
        public static IHtmlContent ControlFor<TModel, TResult, TBuilder>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, Action<TBuilder> configAction, TBuilder builder,
            ITypedControlRenderer renderer, ControlConfig config)
        {
            FormConfig formConfig = htmlHelper.ViewBag.FormConfig as FormConfig;

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if(expression == null)
                throw new ArgumentNullException(nameof(expression));
            if(formConfig == null)
                throw new InvalidOperationException("Control must be placed inside an MVC Core Bootstrap form");

            config.ColumnWidths = formConfig.ColumnWidths;
            config.PropertyValidationMessages = formConfig.PropertyValidationMessages;
            configAction?.Invoke(builder);
            
            return(renderer.Render(htmlHelper, expression));
        }

        public static IHtmlContent Control<TBuilder>(this IHtmlHelper htmlHelper, Action<TBuilder> configAction,
            TBuilder builder, INonTypedControlRenderer renderer, ControlConfig config, IHtmlContent htmlContent)
        {
            FormConfig formConfig = htmlHelper.ViewBag.FormConfig as FormConfig;

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if(formConfig == null)
                throw new InvalidOperationException("Control must be placed inside an MVC Core Bootstrap form");

            config.ColumnWidths = formConfig.ColumnWidths;
            config.PropertyValidationMessages = formConfig.PropertyValidationMessages;
            configAction?.Invoke(builder);
            
            return(renderer.Render(htmlContent, htmlHelper));
        }
    }
}
