using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
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
        public static IHtmlContent MvcCoreBootstrapTextInputFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, Action<MvcCoreBootstrapTextInputBuilder> configAction = null)
        {
            TextInputConfig config = new TextInputConfig();

            //return(htmlHelper.TextInputFor(expression, configAction, new MvcCoreBootstrapTextInputBuilder(config),
            //    new TextInputRenderer(config), config));
            return (htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapTextInputBuilder(config),
                new TextInputRenderer(config, new TooltipRenderer()), config));
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
        public static IHtmlContent MvcCoreBootstrapTextInputFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, string format, Action<MvcCoreBootstrapTextInputBuilder> configAction = null)
        {
            TextInputConfig config = new TextInputConfig {Format = format};

            return(htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapTextInputBuilder(config),
                new TextInputRenderer(config, new TooltipRenderer()), config));
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
        public static IHtmlContent MvcCoreBootstrapTextInputFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, object htmlAttributes,
            Action<MvcCoreBootstrapTextInputBuilder> configAction = null)
        {
            TextInputConfig config = new TextInputConfig {HtmlAttributes = htmlAttributes};

            return(htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapTextInputBuilder(config),
                new TextInputRenderer(config, new TooltipRenderer()), config));
        }

        /// <summary>
        /// Renders a Bootstrap text input.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="configAction">Action that implements text input configuration.</param>
        /// <returns>Text input html markup.</returns>
        /// <remarks>
        /// <para>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;input&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </para>
        /// <para>Determines &lt;input&gt; element's "value" attribute based on the following precedence:</para>
        /// <list type="number">
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item>Otherwise, <c>string.Empty</c>.</item>
        /// </list>
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapTextInput(this IHtmlHelper htmlHelper, string expression,
            Action<MvcCoreBootstrapTextInputBuilder> configAction = null)
        {
            TextInputConfig config = new TextInputConfig();

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapTextInputBuilder(config),
                new TextInputRenderer(config, new TooltipRenderer()), config, htmlHelper.TextBox(expression)));
        }

        /// <summary>
        /// Renders a Bootstrap text input.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="value">If non-<c>null</c>, value to include in the element.</param>
        /// <param name="configAction">Action that implements text input configuration.</param>
        /// <returns>Text input html markup.</returns>
        /// <remarks>
        /// <para>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;input&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </para>
        /// <para>Determines &lt;input&gt; element's "value" attribute based on the following precedence:</para>
        /// <list type="number">
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item>
        /// <paramref name="value" /> if non-<c>null</c>.
        /// </item>
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item>
        /// Linq expression based on <paramref name="expression" /> (converted to a fully-qualified name) run against
        /// current model if result is non-<c>null</c> and can be converted to a <see cref="T:System.String" />. For example
        /// <c>string.Empty</c> identifies the current model and <c>"prop"</c> identifies the current model's "prop"
        /// property.
        /// </item>
        /// <item>Otherwise, <c>string.Empty</c>.</item>
        /// </list>
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapTextInput(this IHtmlHelper htmlHelper, string expression, object value,
            Action<MvcCoreBootstrapTextInputBuilder> configAction = null)
        {
            TextInputConfig config = new TextInputConfig();

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapTextInputBuilder(config),
                new TextInputRenderer(config, new TooltipRenderer()), config, htmlHelper.TextBox(expression, value)));
        }

        /// <summary>
        /// Returns an &lt;input&gt; element of type "text" for the specified <paramref name="expression" />.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="value">If non-<c>null</c>, value to include in the element.</param>
        /// <param name="format">
        /// The composite format <see cref="T:System.String" /> (see http://msdn.microsoft.com/en-us/library/txafckwd.aspx).
        /// </param>
        /// <param name="configAction">Action that implements text input configuration.</param>
        /// <returns>Text input html markup.</returns>
        /// <remarks>
        /// <para>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;input&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </para>
        /// <para>Determines &lt;input&gt; element's "value" attribute based on the following precedence:</para>
        /// <list type="number">
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item>
        /// <paramref name="value" /> if non-<c>null</c>. Formats <paramref name="value" /> using
        /// <paramref name="format" /> or converts <paramref name="value" /> to a <see cref="T:System.String" /> directly if
        /// <paramref name="format" /> is <c>null</c> or empty.
        /// </item>
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />. Formats entry using
        /// <paramref name="format" /> or converts entry to a <see cref="T:System.String" /> directly if <paramref name="format" />
        /// is <c>null</c> or empty.
        /// </item>
        /// <item>
        /// Linq expression based on <paramref name="expression" /> (converted to a fully-qualified name) run against
        /// current model if result is non-<c>null</c> and can be converted to a <see cref="T:System.String" />. For example
        /// <c>string.Empty</c> identifies the current model and <c>"prop"</c> identifies the current model's "prop"
        /// property. Formats result using <paramref name="format" /> or converts result to a <see cref="T:System.String" />
        /// directly if <paramref name="format" /> is <c>null</c> or empty.
        /// </item>
        /// <item>Otherwise, <c>string.Empty</c>.</item>
        /// </list>
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapTextInput(this IHtmlHelper htmlHelper, string expression, object value,
            string format, Action<MvcCoreBootstrapTextInputBuilder> configAction = null)
        {
            TextInputConfig config = new TextInputConfig();

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapTextInputBuilder(config),
                new TextInputRenderer(config, new TooltipRenderer()), config, htmlHelper.TextBox(expression, value, format)));
        }

        /// <summary>
        /// Returns an &lt;input&gt; element of type "text" for the specified <paramref name="expression" />.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="value">If non-<c>null</c>, value to include in the element.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="configAction">Action that implements text input configuration.</param>
        /// <returns>Text input html markup.</returns>
        /// <remarks>
        /// <para>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;input&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </para>
        /// <para>Determines &lt;input&gt; element's "value" attribute based on the following precedence:</para>
        /// <list type="number">
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item>
        /// <paramref name="value" /> if non-<c>null</c>.
        /// </item>
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item>
        /// Linq expression based on <paramref name="expression" /> (converted to a fully-qualified name) run against
        /// current model if result is non-<c>null</c> and can be converted to a <see cref="T:System.String" />. For example
        /// <c>string.Empty</c> identifies the current model and <c>"prop"</c> identifies the current model's "prop"
        /// property.
        /// </item>
        /// <item>Existing "value" entry in <paramref name="htmlAttributes" /> if any.</item>
        /// <item>Otherwise, <c>string.Empty</c>.</item>
        /// </list>
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapTextInput(this IHtmlHelper htmlHelper, string expression, object value,
            object htmlAttributes, Action<MvcCoreBootstrapTextInputBuilder> configAction = null)
        {
            TextInputConfig config = new TextInputConfig();

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapTextInputBuilder(config),
                new TextInputRenderer(config, new TooltipRenderer()), config, htmlHelper.TextBox(expression, value, htmlAttributes)));
        }

        /// <summary>
        /// Renders a Bootstrap checkbox.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="configAction">Action that implements checkbox configuration.</param>
        /// <returns>Checkbox html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapCheckBoxFor<TModel>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, Action<MvcCoreBootstrapCheckBoxBuilder> configAction = null)
        {
            return(htmlHelper.CheckBoxFor(expression, configAction, new CheckBoxConfig()));
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
        public static IHtmlContent MvcCoreBootstrapCheckBoxFor<TModel>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, object htmlAttributes,
            Action<MvcCoreBootstrapCheckBoxBuilder> configAction = null)
        {
            return(htmlHelper.CheckBoxFor(expression, configAction, new CheckBoxConfig {HtmlAttributes = htmlAttributes}));
        }

        private static IHtmlContent CheckBoxFor<TModel>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, Action<MvcCoreBootstrapCheckBoxBuilder> configAction,
            CheckBoxConfig config)
        {
            FormConfig formConfig = htmlHelper.ViewBag.FormConfig as FormConfig;

            Debug.Assert(formConfig != null);
            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if(expression == null)
                throw new ArgumentNullException(nameof(expression));

            config.ColumnWidths = formConfig.ColumnWidths;
            config.PropertyValidationMessages = formConfig.PropertyValidationMessages;
            configAction?.Invoke(new MvcCoreBootstrapCheckBoxBuilder(config));
            
            return(new CheckBoxRenderer(config, new TooltipRenderer()).Render(htmlHelper, expression));
        }

        /// <summary>
        /// Renders a Bootstrap checkbox.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="configAction">Action that implements checkbox configuration.</param>
        /// <returns>Checkbox html markup.</returns>
        /// <remarks>
        /// <para>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// checkbox element's "name" attribute. Sanitizes <paramref name="expression" /> to set checkbox element's "id"
        /// attribute.
        /// </para>
        /// <para>Determines checkbox element's "checked" attribute based on the following precedence:</para>
        /// <list type="number">
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.Boolean" />.
        /// </item>
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.Boolean" />.
        /// </item>
        /// <item>
        /// Linq expression based on <paramref name="expression" /> (converted to a fully-qualified name) run against
        /// current model if result is non-<c>null</c> and can be converted to a <see cref="T:System.Boolean" />. For example
        /// <c>string.Empty</c> identifies the current model and <c>"prop"</c> identifies the current model's "prop"
        /// property.
        /// </item>
        /// <item>Otherwise, does not include a "checked" attribute.</item>
        /// </list>
        /// <para>
        /// In all but the default case, includes a "checked" attribute with
        /// value "checked" if the <see cref="T:System.Boolean" /> values is <c>true</c>; does not include the attribute otherwise.
        /// </para>
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapCheckBox(this IHtmlHelper htmlHelper, string expression,
            Action<MvcCoreBootstrapCheckBoxBuilder> configAction = null)
        {
            CheckBoxConfig config = new CheckBoxConfig();

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapCheckBoxBuilder(config),
                new CheckBoxRenderer(config, new TooltipRenderer()), config, htmlHelper.CheckBox(expression)));
        }

        /// <summary>
        /// Renders a Bootstrap checkbox.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="isChecked">If <c>true</c>, checkbox is initially checked.</param>
        /// <param name="configAction">Action that implements checkbox configuration.</param>
        /// <returns>Checkbox html markup.</returns>
        /// <remarks>
        /// <para>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// checkbox element's "name" attribute. Sanitizes <paramref name="expression" /> to set checkbox element's "id"
        /// attribute.
        /// </para>
        /// <para>Determines checkbox element's "checked" attribute based on the following precedence:</para>
        /// <list type="number">
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.Boolean" />.
        /// </item>
        /// <item><paramref name="isChecked" /> if non-<c>null</c>.</item>
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.Boolean" />.
        /// </item>
        /// <item>
        /// Linq expression based on <paramref name="expression" /> (converted to a fully-qualified name) run against
        /// current model if result is non-<c>null</c> and can be converted to a <see cref="T:System.Boolean" />. For example
        /// <c>string.Empty</c> identifies the current model and <c>"prop"</c> identifies the current model's "prop"
        /// property.
        /// </item>
        /// <item>Otherwise, does not include a "checked" attribute.</item>
        /// </list>
        /// <para>
        /// In all but the default case, includes a "checked" attribute with
        /// value "checked" if the <see cref="T:System.Boolean" /> values is <c>true</c>; does not include the attribute otherwise.
        /// </para>
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapCheckBox(this IHtmlHelper htmlHelper, string expression, bool isChecked,
            Action<MvcCoreBootstrapCheckBoxBuilder> configAction = null)
        {
            CheckBoxConfig config = new CheckBoxConfig();

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapCheckBoxBuilder(config),
                new CheckBoxRenderer(config, new TooltipRenderer()), config, htmlHelper.CheckBox(expression, isChecked)));
        }

        /// <summary>
        /// Renders a Bootstrap checkbox.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the checkbox element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="configAction">Action that implements checkbox configuration.</param>
        /// <returns>Checkbox html markup.</returns>
        /// <remarks>
        /// <para>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// checkbox element's "name" attribute. Sanitizes <paramref name="expression" /> to set checkbox element's "id"
        /// attribute.
        /// </para>
        /// <para>Determines checkbox element's "checked" attribute based on the following precedence:</para>
        /// <list type="number">
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.Boolean" />.
        /// </item>
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.Boolean" />.
        /// </item>
        /// <item>
        /// Linq expression based on <paramref name="expression" /> (converted to a fully-qualified name) run against
        /// current model if result is non-<c>null</c> and can be converted to a <see cref="T:System.Boolean" />. For example
        /// <c>string.Empty</c> identifies the current model and <c>"prop"</c> identifies the current model's "prop"
        /// property.
        /// </item>
        /// <item>Existing "checked" entry in <paramref name="htmlAttributes" /> if any.</item>
        /// <item>Otherwise, does not include a "checked" attribute.</item>
        /// </list>
        /// <para>
        /// In all but the <paramref name="htmlAttributes" /> and default cases, includes a "checked" attribute with
        /// value "checked" if the <see cref="T:System.Boolean" /> values is <c>true</c>; does not include the attribute otherwise.
        /// </para>
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapCheckBox(this IHtmlHelper htmlHelper, string expression, object htmlAttributes,
            Action<MvcCoreBootstrapCheckBoxBuilder> configAction = null)
        {
            CheckBoxConfig config = new CheckBoxConfig();

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapCheckBoxBuilder(config),
                new CheckBoxRenderer(config, new TooltipRenderer()), config, htmlHelper.CheckBox(expression, htmlAttributes)));
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
        public static IHtmlContent MvcCoreBootstrapRadioButtonsFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<object> values, IEnumerable<string> labels,
            Action<MvcCoreBootstrapRadioButtonsBuilder> configAction = null)
        {
            RadioButtonsConfig config = new RadioButtonsConfig();

            if(values.Count() != labels.Count())
                throw new ArgumentException("Values and labels must have the same count");

            for(int i = 0; i < values.Count(); i++)
            {
                config.RadioButtons.Add(new RadioButtonConfig
                {
                    Value = values.ElementAt(i),
                    Label = labels.ElementAt(i),
                });
            }

            return(htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapRadioButtonsBuilder(config), 
                new RadioButtonsRenderer(config, new TooltipRenderer()), config));
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
        public static IHtmlContent MvcCoreBootstrapRadioButtonsFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<string> labels,
            Action<MvcCoreBootstrapRadioButtonsBuilder> configAction = null) where TModel : class
        {
            RadioButtonsConfig config = new RadioButtonsConfig();

            if(expression.ReturnType.GetTypeInfo().IsEnum || expression.ReturnType.GenericTypeArguments[0].GetTypeInfo().IsEnum)
            {
                Array values = Enum.GetValues(expression.ReturnType.GenericTypeArguments[0]);

                if(values.Length != labels.Count())
                    throw new ArgumentException("Values and labels must have the same count");

                for(int i = 0; i < values.Length; i++)
                {
                    config.RadioButtons.Add(new RadioButtonConfig
                    {
                        Value = values.GetValue(i).ToString(),
                        Label = labels.ElementAt(i),
                    });
                }
            }
            else
            {
                throw new ArgumentException("Model property is not an enum");
            }

            return(htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapRadioButtonsBuilder(config), 
                new RadioButtonsRenderer(config, new TooltipRenderer()), config));
        }

        /// <summary>
        /// Renders Bootstrap radio buttons. Each radio button is configrued individually.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="configAction">Action that implements radio buttons configuration.</param>
        /// <returns>Radio buttons html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapRadioButtonsFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression,
            Action<MvcCoreBootstrapRadioButtonsBuilder> configAction)
        {
            RadioButtonsConfig config = new RadioButtonsConfig();

            return(htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapRadioButtonsBuilder(config), 
                new RadioButtonsRenderer(config, new TooltipRenderer()), config));
        }

        /// <summary>
        /// Renders Bootstrap buttons.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="values">Radio buttons values.</param>
        /// <param name="labels">Labels for each radio button (value).</param>
        /// <param name="configAction">Action that implements radio buttons configuration.</param>
        /// <returns>Radio buttons html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapRadioButtons(this IHtmlHelper htmlHelper, string expression,
            IEnumerable<object> values, IEnumerable<string> labels,
            Action<MvcCoreBootstrapRadioButtonsBuilder> configAction = null)
        {
            RadioButtonsConfig config = new RadioButtonsConfig();

            if(values.Count() != labels.Count())
            {
                throw new ArgumentException("Values and labels must have the same count");
            }
                
            for(int i = 0; i < values.Count(); i++)
            {
                config.RadioButtons.Add(new RadioButtonConfig
                {
                    Value = values.ElementAt(i),
                    Label = labels.ElementAt(i),
                });
            }

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapRadioButtonsBuilder(config), 
                new RadioButtonsRenderer(config, new TooltipRenderer()), config, htmlHelper.RadioButton(expression, 0)));
        }

        /// <summary>
        /// Renders Bootstrap radio buttons. Each radio button is configrued individually.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="configAction">Action that implements radio buttons configuration.</param>
        /// <returns>Radio buttons html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapRadioButtons(this IHtmlHelper htmlHelper, string expression,
            Action<MvcCoreBootstrapRadioButtonsBuilder> configAction)
        {
            RadioButtonsConfig config = new RadioButtonsConfig();

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapRadioButtonsBuilder(config), 
                new RadioButtonsRenderer(config, new TooltipRenderer()), config, htmlHelper.RadioButton(expression, 0)));
        }

        /// <summary>
        /// Renders a Bootstrap text area.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="rows">Number of rows in the text area.</param>
        /// <param name="configAction">Action that implements text area configuration.</param>
        /// <returns>Text area html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapTextAreaFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, int rows = 3,
            Action<MvcCoreBootstrapTextAreaBuilder> configAction = null)
        {
            TextAreaConfig config = new TextAreaConfig {Rows = rows};

            return(htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapTextAreaBuilder(config),
                new TextAreaRenderer(config, new TooltipRenderer()), config));
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
        public static IHtmlContent MvcCoreBootstrapTextAreaFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, object htmlAttributes, int rows = 3,
            Action<MvcCoreBootstrapTextAreaBuilder> configAction = null)
        {
            TextAreaConfig config = new TextAreaConfig {HtmlAttributes = htmlAttributes, Rows = rows};

            return(htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapTextAreaBuilder(config),
                new TextAreaRenderer(config, new TooltipRenderer()), config));
        }

        /// <summary>
        /// Renders a Bootstrap text area.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="rows">Number of rows in the text area.</param>
        /// <param name="configAction">Action that implements text area configuration.</param>
        /// <returns>Text area html markup.</returns>
        /// <remarks>
        /// <para>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;textarea&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </para>
        /// <para>Determines &lt;textarea&gt; element's content based on the following precedence:</para>
        /// <list type="number">
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item>
        /// Linq expression based on <paramref name="expression" /> (converted to a fully-qualified name) run against
        /// current model if result is non-<c>null</c> and can be converted to a <see cref="T:System.String" />. For example
        /// <c>string.Empty</c> identifies the current model and <c>"prop"</c> identifies the current model's "prop"
        /// property.
        /// </item>
        /// <item>Otherwise, <c>string.Empty</c>.</item>
        /// </list>
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapTextArea(this IHtmlHelper htmlHelper, string expression, int rows = 3,
            Action<MvcCoreBootstrapTextAreaBuilder> configAction = null)
        {
            TextAreaConfig config = new TextAreaConfig {Rows = rows};

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapTextAreaBuilder(config), 
                new TextAreaRenderer(config, new TooltipRenderer()), config, htmlHelper.TextArea(expression)));
        }

        /// <summary>
        /// Renders a Bootstrap text area.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="rows">Number of rows in the text area.</param>
        /// <param name="configAction">Action that implements text area configuration.</param>
        /// <remarks>
        /// <para>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;textarea&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </para>
        /// <para>Determines &lt;textarea&gt; element's content based on the following precedence:</para>
        /// <list type="number">
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item>
        /// Linq expression based on <paramref name="expression" /> (converted to a fully-qualified name) run against
        /// current model if result is non-<c>null</c> and can be converted to a <see cref="T:System.String" />. For example
        /// <c>string.Empty</c> identifies the current model and <c>"prop"</c> identifies the current model's "prop"
        /// property.
        /// </item>
        /// <item>Otherwise, <c>string.Empty</c>.</item>
        /// </list>
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapTextArea(this IHtmlHelper htmlHelper, string expression, object htmlAttributes,
            int rows = 3, Action<MvcCoreBootstrapTextAreaBuilder> configAction = null)
        {
            TextAreaConfig config = new TextAreaConfig {Rows = rows};

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapTextAreaBuilder(config), 
                new TextAreaRenderer(config, new TooltipRenderer()), config, htmlHelper.TextArea(expression, htmlAttributes)));
        }

        /// <summary>
        /// Renders a Bootstrap text area.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="value">If non-<c>null</c>, value to include in the element.</param>
        /// <param name="rows">Number of rows in the text area.</param>
        /// <param name="configAction">Action that implements text area configuration.</param>
        /// <returns>Text area html markup.</returns>
        /// <remarks>
        /// <para>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;textarea&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </para>
        /// <para>Determines &lt;textarea&gt; element's content based on the following precedence:</para>
        /// <list type="number">
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item><paramref name="value" /> if non-<c>null</c>.</item>
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item>
        /// Linq expression based on <paramref name="expression" /> (converted to a fully-qualified name) run against
        /// current model if result is non-<c>null</c> and can be converted to a <see cref="T:System.String" />. For example
        /// <c>string.Empty</c> identifies the current model and <c>"prop"</c> identifies the current model's "prop"
        /// property.
        /// </item>
        /// <item>Otherwise, <c>string.Empty</c>.</item>
        /// </list>
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapTextArea(this IHtmlHelper htmlHelper, string expression, string value,
            int rows = 3, Action<MvcCoreBootstrapTextAreaBuilder> configAction = null)
        {
            TextAreaConfig config = new TextAreaConfig {Rows = rows};

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapTextAreaBuilder(config), 
                new TextAreaRenderer(config, new TooltipRenderer()), config, htmlHelper.TextArea(expression, value)));
        }

        /// <summary>
        /// Renders a Bootstrap text area.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="value">If non-<c>null</c>, value to include in the element.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="rows">Number of rows in the text area.</param>
        /// <param name="configAction">Action that implements text area configuration.</param>
        /// <returns>Text area html markup.</returns>
        /// <remarks>
        /// <para>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;textarea&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </para>
        /// <para>Determines &lt;textarea&gt; element's content based on the following precedence:</para>
        /// <list type="number">
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item><paramref name="value" /> if non-<c>null</c>.</item>
        /// <item>
        /// <see cref="T:Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary" /> entry for <paramref name="expression" /> (converted to a
        /// fully-qualified name) if entry exists and can be converted to a <see cref="T:System.String" />.
        /// </item>
        /// <item>
        /// Linq expression based on <paramref name="expression" /> (converted to a fully-qualified name) run against
        /// current model if result is non-<c>null</c> and can be converted to a <see cref="T:System.String" />. For example
        /// <c>string.Empty</c> identifies the current model and <c>"prop"</c> identifies the current model's "prop"
        /// property.
        /// </item>
        /// <item>Otherwise, <c>string.Empty</c>.</item>
        /// </list>
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapTextArea(this IHtmlHelper htmlHelper, string expression, string value,
            object htmlAttributes, int rows = 3, Action<MvcCoreBootstrapTextAreaBuilder> configAction = null)
        {
            TextAreaConfig config = new TextAreaConfig {Rows = rows};

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapTextAreaBuilder(config), 
                new TextAreaRenderer(config, new TooltipRenderer()), config, htmlHelper.TextArea(expression, value, htmlAttributes)));
        }
    }
}
