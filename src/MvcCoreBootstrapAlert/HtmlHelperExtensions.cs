using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap;
using MvcCoreBootstrapAlert.Builders;
using MvcCoreBootstrapAlert.Config;
using MvcCoreBootstrapAlert.Rendering;

namespace MvcCoreBootstrapAlert
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders an Mvc Core Bootstrap alert.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="configAction">Action that implements alert configuration.</param>
        /// <returns>Alert html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapAlert(this IHtmlHelper htmlHelper, string text,
            ContextualState state, Action<MvcCoreBootstrapAlertBuilder> configAction = null)
        {
            AlertConfig config = new AlertConfig {Text = text, State = state};

            configAction?.Invoke(new MvcCoreBootstrapAlertBuilder(config));

            return(new AlertRenderer().Render(config));
        }
    }
}
