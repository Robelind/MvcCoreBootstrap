using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public static IHtmlContent MvcCoreBootstrapForm(this IHtmlHelper htmlHelper, object model,
            Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            FormConfig config = new FormConfig {Model = model};

            configAction?.Invoke(new MvcCoreBootstrapFormBuilder(config));

            return(new FormRenderer(htmlHelper).Render(config));
        }

        public static IHtmlContent BootstrapTextBoxFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, Action<MvcCoreBootstrapTextInputBuilder> configAction = null)
        {
            TextInputConfig config = new TextInputConfig();

            if (htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapTextInputBuilder(config));

            return (new TextBoxRenderer<TModel, TResult>().Render(config, htmlHelper, expression));
        }

        public static IHtmlContent BootstrapCheckBoxFor<TModel>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, Action<MvcCoreBootstrapCheckBoxBuilder> configAction = null)
        {
            CheckBoxConfig config = new CheckBoxConfig();

            if (htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapCheckBoxBuilder(config));

            return (new CheckBoxRenderer<TModel>().Render(config, htmlHelper, expression));
        }

        /// <summary>
        /// Renders radio buttons for a model property.
        /// </summary>
        /// <typeparam name="TModel">Model type.</typeparam>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="values">Possible values for the property.</param>
        /// <param name="labels">Labels for each radio button (value).</param>
        /// <param name="vertical">If true renders the radio buttons vertically, otherwise horizontally</param>
        /// <returns>Radio buttons html markup.</returns>
        public static IHtmlContent BootstrapRadioButtonsFor<TModel>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, IEnumerable<object> values, IEnumerable<string> labels,
            bool vertical = true)
        {
            RadioButtonsConfig<TModel> config = new RadioButtonsConfig<TModel> {Horizontal = !vertical};

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));
            if (values.Count() != labels.Count())
                throw new ArgumentException("Values and labels must have the same count");

            for(int i = 0; i < values.Count(); i++)
            {
                config.RadioButtons.Add(new RadioButtonConfig<TModel>
                {
                    Expression = expression,
                    Value = values.ElementAt(i),
                    Label = labels.ElementAt(i),
                });
            }

            return(new RadioButtonsRenderer<TModel>().Render(config, htmlHelper));
        }

        /// <summary>
        /// Renders radio buttons. Each radio button is configrued individually.
        /// </summary>
        /// <typeparam name="TModel">Model type.</typeparam>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="configAction">Action that implements radio buttons configuration.</param>
        /// <returns>Radio buttons html markup.</returns>
        public static IHtmlContent BootstrapRadioButtons<TModel>(this IHtmlHelper<TModel> htmlHelper,
            Action<MvcCoreBootstrapRadioButtonsBuilder<TModel>> configAction)
        {
            RadioButtonsConfig<TModel> config = new RadioButtonsConfig<TModel>();

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if(configAction == null)
                throw new ArgumentNullException(nameof(configAction));

            configAction(new MvcCoreBootstrapRadioButtonsBuilder<TModel>(config));

            return(new RadioButtonsRenderer<TModel>().Render(config, htmlHelper));
        }
    }
}
