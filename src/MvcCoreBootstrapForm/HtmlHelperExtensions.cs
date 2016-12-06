using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap;
using MvcCoreBootstrapForm.Builders;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Rendering;

namespace MvcCoreBootstrapForm
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders a Bootstrap form row.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="configAction">Action that implements form row configuration.</param>
        public static void BootstrapFormRow(this IHtmlHelper htmlHelper, Action<MvcCoreBootstrapFormRowBuilder> configAction)
        {
            RowConfig config = new RowConfig();
            MvcCoreBootstrapFormRowBuilder builder = new MvcCoreBootstrapFormRowBuilder(config);

            configAction(builder);
            new RowRenderer(config, htmlHelper.ViewContext.Writer).Render();
        }

        /// <summary>
        /// Renders a Bootstrap form group.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="contents">Contents of the form group.</param>
        public static void BootstrapFormGroup(this IHtmlHelper htmlHelper, params IHtmlContent[] contents)
        {
            new GroupRenderer(contents, htmlHelper.ViewContext.Writer).Render();
        }

        /// <summary>
        /// Renders an Mvc Core Bootstrap form validation summary.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="modelState">Model state.</param>
        /// <param name="state">Validation summary contextual state. Defaults to "Danger"</param>
        /// <returns>Validation summary html markup.</returns>
        public static IHtmlContent BootstrapValidationSummary(this IHtmlHelper htmlHelper, ModelStateDictionary modelState,
            ContextualState state = ContextualState.Danger)
        {
            ValidationSummaryConfig config = new ValidationSummaryConfig {State = state, ModelState = modelState};

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if(modelState == null)
            {
                throw new ArgumentNullException(nameof(modelState));
            }
            if(state == ContextualState.Default)
            {
                throw new ArgumentException(@"""Default"" is not a valid state for the validation summary.");
            }

            return(new ValidationSummaryRenderer(config, htmlHelper).Render());
        }
    }
}
