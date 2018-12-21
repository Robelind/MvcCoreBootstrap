using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
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
        public static IHtmlContent MvcCoreBootstrapDropdownFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList,
            Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList};

            return(htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapDropdownBuilder(config),
                new DropdownRenderer(config, new TooltipRenderer()), config));
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
        public static IHtmlContent MvcCoreBootstrapDropdownFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes,
            Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList, HtmlAttributes = htmlAttributes};

            return(htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapDropdownBuilder(config),
                new DropdownRenderer(config, new TooltipRenderer()), config));
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
        public static IHtmlContent MvcCoreBootstrapDropdownFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList, string optionLabel,
            Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList, Default = optionLabel};

            return(htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapDropdownBuilder(config),
                new DropdownRenderer(config, new TooltipRenderer()), config));
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
        public static IHtmlContent MvcCoreBootstrapDropdownFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList, string optionLabel,
            object htmlAttributes, Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList, Default = optionLabel, HtmlAttributes = htmlAttributes};

            return(htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapDropdownBuilder(config),
                new DropdownRenderer(config, new TooltipRenderer()), config));
        }

        /// <summary>
        /// Renders a Bootstrap dropdown.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="configAction">Action that implements dropdown configuration.</param>
        /// <returns>Dropdown html markup.</returns>
        /// <remarks>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;select&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapDropdown(this IHtmlHelper htmlHelper, string expression,
            Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig();

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapDropdownBuilder(config),
                new DropdownRenderer(config, new TooltipRenderer()), config, htmlHelper.DropDownList(expression)));
        }

        /// <summary>
        /// Renders a Bootstrap dropdown.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="optionLabel">
        /// The text for a default empty item. Does not include such an item if argument is <c>null</c>.
        /// </param>
        /// <param name="configAction">Action that implements dropdown configuration.</param>
        /// <returns>Dropdown html markup.</returns>
        /// <remarks>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;select&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapDropdown(this IHtmlHelper htmlHelper, string expression, string optionLabel,
            Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig();

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapDropdownBuilder(config),
                new DropdownRenderer(config, new TooltipRenderer()), config, htmlHelper.DropDownList(expression, optionLabel)));
        }

        /// <summary>
        /// Renders a Bootstrap dropdown.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="selectList">
        /// A collection of <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.SelectListItem" /> objects used to populate the &lt;select&gt; element with
        /// &lt;optgroup&gt; and &lt;option&gt; elements.
        /// </param>
        /// <param name="configAction">Action that implements dropdown configuration.</param>
        /// <returns>Dropdown html markup.</returns>
        /// <remarks>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;select&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapDropdown(this IHtmlHelper htmlHelper, string expression,
            IEnumerable<SelectListItem> selectList, Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList};

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapDropdownBuilder(config),
                new DropdownRenderer(config, new TooltipRenderer()), config, htmlHelper.DropDownList(expression, selectList)));
        }

        /// <summary>
        /// Renders a Bootstrap dropdown.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="selectList">
        /// A collection of <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.SelectListItem" /> objects used to populate the &lt;select&gt; element with
        /// &lt;optgroup&gt; and &lt;option&gt; elements.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the &lt;select&gt; element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML attributes.
        /// </param>
        /// <param name="configAction">Action that implements dropdown configuration.</param>
        /// <returns>Dropdown html markup.</returns>
        /// <remarks>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;select&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapDropdown(this IHtmlHelper htmlHelper, string expression,
            IEnumerable<SelectListItem> selectList, object htmlAttributes,
            Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList, HtmlAttributes = htmlAttributes};

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapDropdownBuilder(config),
                new DropdownRenderer(config, new TooltipRenderer()), config, htmlHelper.DropDownList(expression, selectList, htmlAttributes)));
        }

        /// <summary>
        /// Renders a Bootstrap dropdown.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="selectList">
        /// A collection of <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.SelectListItem" /> objects used to populate the &lt;select&gt; element with
        /// &lt;optgroup&gt; and &lt;option&gt; elements.
        /// </param>
        /// <param name="optionLabel">
        /// The text for a default empty item. Does not include such an item if argument is <c>null</c>.
        /// </param>
        /// <param name="configAction">Action that implements dropdown configuration.</param>
        /// <returns>Dropdown html markup.</returns>
        /// <remarks>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;select&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapDropdown(this IHtmlHelper htmlHelper, string expression,
            IEnumerable<SelectListItem> selectList, string optionLabel, Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList, Default = optionLabel};

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapDropdownBuilder(config),
                new DropdownRenderer(config, new TooltipRenderer()), config, htmlHelper.DropDownList(expression, selectList, optionLabel)));
        }

        /// <summary>
        /// Renders a Bootstrap multiple select listbox.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="expression">Model property expression.</param>
        /// <param name="selectList">List containing the possible choices.</param>
        /// <param name="configAction">Action that implements listbox configuration.</param>
        /// <returns>Listbox html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapListboxFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList,
            Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Items = selectList, Multiple = true};

            return(htmlHelper.ControlFor(expression, configAction, new MvcCoreBootstrapDropdownBuilder(config),
                new DropdownRenderer(config, new TooltipRenderer()), config));
        }

        /// <summary>
        /// Renders a Bootstrap multiple select listbox.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="configAction">Action that implements listbox configuration.</param>
        /// <returns>Listbox html markup.</returns>
        /// <remarks>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;select&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapListbox(this IHtmlHelper htmlHelper,
            string expression, Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Multiple = true};

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapDropdownBuilder(config),
                new DropdownRenderer(config, new TooltipRenderer()), config, htmlHelper.DropDownList(expression)));
        }

        /// <summary>
        /// Renders a Bootstrap multiple select listbox.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper" /> instance this method extends.</param>
        /// <param name="expression">Expression name, relative to the current model.</param>
        /// <param name="selectList">
        /// A collection of <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.SelectListItem" /> objects used to populate the &lt;select&gt; element with
        /// &lt;optgroup&gt; and &lt;option&gt; elements.
        /// </param>
        /// <param name="configAction">Action that implements listbox configuration.</param>
        /// <returns>Listbox html markup.</returns>
        /// <remarks>
        /// Combines <see cref="P:Microsoft.AspNetCore.Mvc.ViewFeatures.TemplateInfo.HtmlFieldPrefix" /> and <paramref name="expression" /> to set
        /// &lt;select&gt; element's "name" attribute. Sanitizes <paramref name="expression" /> to set element's "id"
        /// attribute.
        /// </remarks>
        public static IHtmlContent MvcCoreBootstrapListbox(this IHtmlHelper htmlHelper,
            string expression, IEnumerable<SelectListItem> selectList,
            Action<MvcCoreBootstrapDropdownBuilder> configAction = null)
        {
            DropdownConfig config = new DropdownConfig {Multiple = true, Items = selectList};

            return(htmlHelper.Control(configAction, new MvcCoreBootstrapDropdownBuilder(config),
                new DropdownRenderer(config, new TooltipRenderer()), config, htmlHelper.DropDownList(expression, selectList)));
        }
    }
}
