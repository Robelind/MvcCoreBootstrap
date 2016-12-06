using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Builders;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Rendering;

namespace MvcCoreBootstrapForm
{
    public static class HtmlHelperSelectExtensions
    {
        /// <summary>
        /// Renders a Bootstrap dropdown.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="selectList">List containing the possible choices.</param>
        /// <param name="configAction">Action that implements dropdown configuration.</param>
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
        /// Renders a Bootstrap dropdown.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="selectList">List containing the possible choices.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the &lt;select&gt; element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML attributes.
        /// </param>
        /// <param name="configAction">Action that implements dropdown configuration.</param>
        /// <returns>Dropdown html markup.</returns>
        public static IHtmlContent BootstrapDropdownFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes,
            Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList, HtmlAttributes = htmlAttributes};

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapDropdownBuilder(config));

            return(new DropdownRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders a Bootstrap dropdown.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="selectList">List containing the possible choices.</param>
        /// <param name="optionLabel">
        /// The text for a default empty item. Does not include such an item if argument is <c>null</c>.
        /// Defaults to empty.
        /// </param>
        /// <param name="configAction">Action that implements dropdown configuration.</param>
        /// <returns>Dropdown html markup.</returns>
        public static IHtmlContent BootstrapDropdownFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList, string optionLabel,
            Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList, Default = optionLabel};

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapDropdownBuilder(config));

            return(new DropdownRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders a Bootstrap dropdown.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="selectList">List containing the possible choices.</param>
        /// <param name="optionLabel">
        /// The text for a default empty item. Does not include such an item if argument is <c>null</c>.
        /// Defaults to empty.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the &lt;select&gt; element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML attributes.
        /// </param>
        /// <param name="configAction">Action that implements dropdown configuration.</param>
        /// <returns>Dropdown html markup.</returns>
        public static IHtmlContent BootstrapDropdownFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList, string optionLabel,
            object htmlAttributes, Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList, Default = optionLabel, HtmlAttributes = htmlAttributes};

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapDropdownBuilder(config));

            return(new DropdownRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }

        /// <summary>
        /// Renders a Bootstrap multiple select listbox.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="selectList">List containing the possible choices.</param>
        /// <param name="configAction">Action that implements listbox configuration.</param>
        /// <returns>Listbox html markup.</returns>
        public static IHtmlContent BootstrapListboxFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList,
            Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList, Multiple = true};

            if(htmlHelper == null)
                throw new ArgumentNullException(nameof(htmlHelper));
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            configAction?.Invoke(new MvcCoreBootstrapDropdownBuilder(config));

            return(new DropdownRenderer<TModel, TResult>(config, htmlHelper, expression).Render());
        }
    }
}
