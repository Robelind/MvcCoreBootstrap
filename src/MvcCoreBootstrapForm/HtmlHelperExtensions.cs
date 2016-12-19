using System;
using Microsoft.AspNetCore.Html;
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
        public static IHtmlContent MvcCoreBootstrapFormRow(this IHtmlHelper htmlHelper,
            Action<MvcCoreBootstrapFormRowBuilder> configAction)
        {
            RowConfig config = new RowConfig();
            MvcCoreBootstrapFormRowBuilder builder = new MvcCoreBootstrapFormRowBuilder(config);

            configAction(builder);

            return(new RowRenderer(config).Render());
        }

        /// <summary>
        /// Renders a Bootstrap form group.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="contents">Contents of the form group.</param>
        public static IHtmlContent MvcCoreBootstrapFormGroup(this IHtmlHelper htmlHelper, params IHtmlContent[] contents)
        {
            return(new GroupRenderer(contents, null).Render());
        }

        /// <summary>
        /// Renders a Bootstrap form group.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="contents">Contents of the form group.</param>
        /// <param name="label">Label for the form group.</param>
        public static IHtmlContent MvcCoreBootstrapFormGroup(this IHtmlHelper htmlHelper, string label, params IHtmlContent[] contents)
        {
            return(new GroupRenderer(contents, label).Render());
        }

        /// <summary>
        /// Renders an Mvc Core Bootstrap form validation summary.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="state">Validation summary contextual state. Defaults to "Danger"</param>
        /// <returns>Validation summary html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapValidationSummary(this IHtmlHelper htmlHelper,
            ContextualState state = ContextualState.Danger)
        {
            ValidationSummaryConfig config = new ValidationSummaryConfig {State = state};

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if(state == ContextualState.Default)
            {
                throw new ArgumentException(@"""Default"" is not a valid state for the validation summary.");
            }

            return(new ValidationSummaryRenderer(config, htmlHelper).Render());
        }

        /// <summary>
        /// Renders an Mvc Core Bootstrap form validation summary.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="excludePropertyErrors">If <c>true</c>, display model-level errors only; otherwise display all errors.</param>
        /// <param name="state">Validation summary contextual state. Defaults to "Danger"</param>
        /// <returns>Validation summary html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapValidationSummary(this IHtmlHelper htmlHelper, bool excludePropertyErrors,
            ContextualState state = ContextualState.Danger)
        {
            ValidationSummaryConfig config = new ValidationSummaryConfig
            {
                State = state, ExcludePropertyErrors = excludePropertyErrors
            };

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if(state == ContextualState.Default)
            {
                throw new ArgumentException(@"""Default"" is not a valid state for the validation summary.");
            }

            return(new ValidationSummaryRenderer(config, htmlHelper).Render());
        }
    }
}
