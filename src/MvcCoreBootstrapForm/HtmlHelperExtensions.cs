using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Builders;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Rendering;

namespace MvcCoreBootstrapForm
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders an Mvc Core Bootstrap form.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="configAction">Action that implements form configuration.</param>
        /// <returns>Form html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapForm(this IHtmlHelper htmlHelper,
            Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            FormConfig config = new FormConfig();

            configAction?.Invoke(new MvcCoreBootstrapFormBuilder(config));

            return(new FormRenderer().Render(config));
        }
    }
}
