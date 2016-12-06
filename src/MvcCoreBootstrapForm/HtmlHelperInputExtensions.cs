using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Builders;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Rendering;

namespace MvcCoreBootstrapForm
{
    public static class HtmlHelperInputExtensions
    {
        /// <summary>
        /// Renders a Bootstrap text input.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="configAction">Action that implements text input configuration.</param>
        /// <returns>Text input html markup.</returns>
        public static IHtmlContent BootstrapTextInputFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, Action<MvcCoreBootstrapTextInputBuilder> configAction = null)
        {
            TextInputConfig config = new TextInputConfig();

            if (htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapTextInputBuilder(config));

            return(new TextInputRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders a Bootstrap text input.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="format">
        /// The composite format <see cref="T:System.String" /> (see http://msdn.microsoft.com/en-us/library/txafckwd.aspx).
        /// </param>
        /// <param name="configAction">Action that implements text input configuration.</param>
        /// <returns>Text input html markup.</returns>
        public static IHtmlContent BootstrapTextInputFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, string format, Action<MvcCoreBootstrapTextInputBuilder> configAction = null)
        {
            TextInputConfig config = new TextInputConfig {Format = format};

            if (htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapTextInputBuilder(config));

            return(new TextInputRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders a Bootstrap text input.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="configAction">Action that implements text input configuration.</param>
        /// <returns>Text input html markup.</returns>
        public static IHtmlContent BootstrapTextInputFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, object htmlAttributes,
            Action<MvcCoreBootstrapTextInputBuilder> configAction = null)
        {
            TextInputConfig config = new TextInputConfig {HtmlAttributes = htmlAttributes};

            if (htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapTextInputBuilder(config));

            return(new TextInputRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders a Bootstrap checkbox.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="configAction">Action that implements checkbox configuration.</param>
        /// <returns>Checkbox html markup.</returns>
        public static IHtmlContent BootstrapCheckBoxFor<TModel>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, Action<MvcCoreBootstrapCheckBoxBuilder> configAction = null)
        {
            CheckBoxConfig config = new CheckBoxConfig();

            if (htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapCheckBoxBuilder(config));

            return(new CheckBoxRenderer<TModel>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders a Bootstrap checkbox.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the checkbox element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="configAction">Action that implements checkbox configuration.</param>
        /// <returns>Checkbox html markup.</returns>
        public static IHtmlContent BootstrapCheckBoxFor<TModel>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, object htmlAttributes,
            Action<MvcCoreBootstrapCheckBoxBuilder> configAction = null)
        {
            CheckBoxConfig config = new CheckBoxConfig {HtmlAttributes = htmlAttributes};

            if (htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapCheckBoxBuilder(config));

            return(new CheckBoxRenderer<TModel>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders Bootstrap buttons for a model property.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="values">Possible values for the property.</param>
        /// <param name="labels">Labels for each radio button (value).</param>
        /// <param name="configAction">Action that implements radio buttons configuration.</param>
        /// <returns>Radio buttons html markup.</returns>
        public static IHtmlContent BootstrapRadioButtonsFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<object> values, IEnumerable<string> labels,
            Action<MvcCoreBootstrapRadioButtonsBuilder<TModel, TResult>> configAction = null)
        {
            RadioButtonsConfig<TModel, TResult> config = new RadioButtonsConfig<TModel, TResult>();

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));
            if (values.Count() != labels.Count())
                throw new ArgumentException("Values and labels must have the same count");

            for(int i = 0; i < values.Count(); i++)
            {
                config.RadioButtons.Add(new RadioButtonConfig<TModel, TResult>
                {
                    Expression = expression,
                    Value = values.ElementAt(i),
                    Label = labels.ElementAt(i),
                });
            }
            configAction?.Invoke(new MvcCoreBootstrapRadioButtonsBuilder<TModel, TResult>(config, expression));

            return(new RadioButtonsRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders Bootstrap radio buttons for an enum model property.
        /// The possible values for the property is extracted from the enum type.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="labels">Labels for each radio button (value).</param>
        /// <param name="configAction">Action that implements radio buttons configuration.</param>
        /// <returns>Radio buttons html markup.</returns>
        public static IHtmlContent BootstrapRadioButtonsFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<string> labels,
            Action<MvcCoreBootstrapRadioButtonsBuilder<TModel, TResult>> configAction = null) where TModel : class
        {
            RadioButtonsConfig<TModel, TResult> config = new RadioButtonsConfig<TModel, TResult>();

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if(expression == null)
                throw new ArgumentNullException(nameof(expression));

            if(expression.ReturnType.GetTypeInfo().IsEnum || expression.ReturnType.GenericTypeArguments[0].GetTypeInfo().IsEnum)
            {
                Array values = Enum.GetValues(expression.ReturnType.GenericTypeArguments[0]);

                if(values.Length != labels.Count())
                    throw new ArgumentException("Values and labels must have the same count");

                for(int i = 0; i < values.Length; i++)
                {
                    config.RadioButtons.Add(new RadioButtonConfig<TModel, TResult>
                    {
                        Expression = expression,
                        Value = values.GetValue(i).ToString(),
                        Label = labels.ElementAt(i),
                    });
                }
            }
            else
            {
                throw new ArgumentException("Model property is not an enum");
            }
            configAction?.Invoke(new MvcCoreBootstrapRadioButtonsBuilder<TModel, TResult>(config, expression));

            return(new RadioButtonsRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders Bootstrap radio buttons. Each radio button is configrued individually.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="configAction">Action that implements radio buttons configuration.</param>
        /// <returns>Radio buttons html markup.</returns>
        public static IHtmlContent BootstrapRadioButtonsFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression,
            Action<MvcCoreBootstrapRadioButtonsBuilder<TModel, TResult>> configAction)
        {
            RadioButtonsConfig<TModel, TResult> config = new RadioButtonsConfig<TModel, TResult>();

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if(configAction == null)
                throw new ArgumentNullException(nameof(configAction));

            configAction(new MvcCoreBootstrapRadioButtonsBuilder<TModel, TResult>(config, expression));

            return(new RadioButtonsRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders a Bootstrap dropdown.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="selectList">List containing the possible choices.</param>
        /// <param name="configAction">Action that implements alert configuration.</param>
        /// <returns>Dropdown html markup.</returns>
        public static IHtmlContent BootstrapDropdownFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList,
            Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList};

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapDropdownBuilder(config));

            return(new DropdownRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders a Bootstrap text area.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="rows">Number of rows in the text area.</param>
        /// <param name="configAction">Action that implements text area configuration.</param>
        /// <returns>Text area html markup.</returns>
        public static IHtmlContent BootstrapTextAreaFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, int rows = 3,
            Action<MvcCoreBootstrapTextAreaBuilder> configAction = null)
        {
            TextAreaConfig config = new TextAreaConfig {Rows = rows};

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapTextAreaBuilder(config));

            return(new TextAreaRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders a Bootstrap text area.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="rows">Number of rows in the text area.</param>
        /// <param name="configAction">Action that implements text area configuration.</param>
        /// <returns>Text area html markup.</returns>
        public static IHtmlContent BootstrapTextAreaFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, object htmlAttributes, int rows = 3,
            Action<MvcCoreBootstrapTextAreaBuilder> configAction = null)
        {
            TextAreaConfig config = new TextAreaConfig {HtmlAttributes = htmlAttributes, Rows = rows};

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapTextAreaBuilder(config));

            return(new TextAreaRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }
    }
}
