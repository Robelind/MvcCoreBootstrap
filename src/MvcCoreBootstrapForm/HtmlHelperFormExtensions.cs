using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Builders;
using MvcCoreBootstrapForm.Config;
using MvcCoreBootstrapForm.Rendering;

namespace MvcCoreBootstrapForm
{
    public static class HtmlHelperFormExtensions
    {
        /// <summary>
        /// Renders a Bootstrap form.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="configAction">Action that implements form configuration.</param>
        /// <returns>Form html markup.</returns>
        public static MvcForm MvcCoreBootstrapForm(this IHtmlHelper htmlHelper,
            Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            return(BeginForm(htmlHelper, configAction, null, null, null, FormMethod.Post, null, null));
        }

        /// <summary>
        /// Renders a Bootstrap form.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will not include an antiforgery token.
        /// </param>
        /// <param name="configAction">Action that implements form configuration.</param>
        /// <returns>Form html markup.</returns>
        public static MvcForm MvcCoreBootstrapForm(this IHtmlHelper htmlHelper, bool? antiforgery,
            Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            return(BeginForm(htmlHelper, configAction, null, null, null, FormMethod.Post, antiforgery, null));
        }

        /// <summary>
        /// Renders a Bootstrap form.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="configAction">Action that implements form configuration.</param>
        /// <returns>Form html markup.</returns>
        public static MvcForm MvcCoreBootstrapForm(this IHtmlHelper htmlHelper, FormMethod method,
            Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            return(BeginForm(htmlHelper, configAction, null, null, null, method, null, null));
        }

        /// <summary>
        /// Renders a Bootstrap form.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="configAction">Action that implements form configuration.</param>
        /// <returns>Form html markup.</returns>
        public static MvcForm MvcCoreBootstrapForm(this IHtmlHelper htmlHelper, FormMethod method, object htmlAttributes,
            Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            return(BeginForm(htmlHelper, configAction, null, null, null, method, null, htmlAttributes));
        }

        /// <summary>
        /// Renders a Bootstrap form.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="antiforgery">
        /// If <c>true</c>, &lt;form&gt; elements will include an antiforgery token.
        /// If <c>false</c>, suppresses the generation an &lt;input&gt; of type "hidden" with an antiforgery token.
        /// If <c>null</c>, &lt;form&gt; elements will not include an antiforgery token.
        /// </param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="configAction">Action that implements form configuration.</param>
        /// <returns>Form html markup.</returns>
        public static MvcForm MvcCoreBootstrapForm(this IHtmlHelper htmlHelper, FormMethod method, bool? antiforgery,
            object htmlAttributes, Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            return(BeginForm(htmlHelper, configAction, null, null, null, method, antiforgery, htmlAttributes));
        }

        /// <summary>
        /// Renders a Bootstrap form.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="routeValues">
        /// An <see cref="T:System.Object" /> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="T:System.Object" />. This <see cref="T:System.Object" /> is typically
        /// created using <see cref="T:System.Object" /> initializer syntax. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="configAction">Action that implements form configuration.</param>
        /// <returns>Form html markup.</returns>
        public static MvcForm MvcCoreBootstrapForm(this IHtmlHelper htmlHelper, object routeValues,
            Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            return(BeginForm(htmlHelper, configAction, null, null, routeValues, FormMethod.Post, null, null));
        }

        /// <summary>
        /// Renders a Bootstrap form.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="configAction">Action that implements form configuration.</param>
        /// <returns>Form html markup.</returns>
        public static MvcForm MvcCoreBootstrapForm(this IHtmlHelper htmlHelper, string actionName, string controllerName,
            Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            return(BeginForm(htmlHelper, configAction, actionName, controllerName, null, FormMethod.Post, null, null));
        }

        /// <summary>
        /// Renders a Bootstrap form.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">
        /// An <see cref="T:System.Object" /> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="T:System.Object" />. This <see cref="T:System.Object" /> is typically
        /// created using <see cref="T:System.Object" /> initializer syntax. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="configAction">Action that implements form configuration.</param>
        /// <returns>Form html markup.</returns>
        public static MvcForm MvcCoreBootstrapForm(this IHtmlHelper htmlHelper, string actionName, string controllerName,
            object routeValues, Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            return(BeginForm(htmlHelper, configAction, actionName, controllerName, routeValues, FormMethod.Post, null, null));
        }

        /// <summary>
        /// Renders a Bootstrap form.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="configAction">Action that implements form configuration.</param>
        /// <returns>Form html markup.</returns>
        public static MvcForm MvcCoreBootstrapForm(this IHtmlHelper htmlHelper, string actionName, string controllerName,
            FormMethod method, Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            return(BeginForm(htmlHelper, configAction, actionName, controllerName, null, method, null, null));
        }

        /// <summary>
        /// Renders a Bootstrap form.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">
        /// An <see cref="T:System.Object" /> that contains the parameters for a route. The parameters are retrieved through
        /// reflection by examining the properties of the <see cref="T:System.Object" />. This <see cref="T:System.Object" /> is typically
        /// created using <see cref="T:System.Object" /> initializer syntax. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the route
        /// parameters.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="configAction">Action that implements form configuration.</param>
        /// <returns>Form html markup.</returns>
        public static MvcForm MvcCoreBootstrapForm(this IHtmlHelper htmlHelper, string actionName, string controllerName,
            object routeValues, FormMethod method,  Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            return(BeginForm(htmlHelper, configAction, actionName, controllerName, routeValues, method, null, null));
        }

        /// <summary>
        /// Renders a Bootstrap form.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="htmlAttributes">
        /// An <see cref="T:System.Object" /> that contains the HTML attributes for the element. Alternatively, an
        /// <see cref="T:System.Collections.Generic.IDictionary`2" /> instance containing the HTML
        /// attributes.
        /// </param>
        /// <param name="method">The HTTP method for processing the form, either GET or POST.</param>
        /// <param name="configAction">Action that implements form configuration.</param>
        /// <returns>Form html markup.</returns>
        public static MvcForm MvcCoreBootstrapForm(this IHtmlHelper htmlHelper, string actionName, string controllerName,
            FormMethod method, object htmlAttributes,  Action<MvcCoreBootstrapFormBuilder> configAction = null)
        {
            return(BeginForm(htmlHelper, configAction, actionName, controllerName, null, method, null, htmlAttributes));
        }

        internal static MvcForm BeginForm(IHtmlHelper htmlHelper, Action<MvcCoreBootstrapFormBuilder> configAction,
            string actionName, string controllerName, object routeValues, FormMethod method, bool? antiforgery,
            object htmlAttributes)
        {
            FormConfig config = new FormConfig();

            configAction?.Invoke(new MvcCoreBootstrapFormBuilder(config));

            FormParameters parameters = new FormParameters
            {
                Config = config,
                HtmlHelper = htmlHelper,
                Parser = new HtmlParser(),
                AntiForgery = antiforgery,
                ActionName = actionName,
                ControllerName = controllerName,
                Method = method,
                HtmlAttributes = htmlAttributes,
                RouteValues = routeValues,
            };
            
            htmlHelper.ViewBag.FormSetup = new FormSetup
            {
                Horizontal = config.ColumnWidths != null,
                PropertyValidationMessages = config.PropertyValidationMessages,
            };

            return(new FormRenderer(parameters).Render());
        }
    }
}
