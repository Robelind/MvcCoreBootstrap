﻿using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapButton.Builders;
using MvcCoreBootstrapButton.Config;
using MvcCoreBootstrapButton.Rendering;
using MvcCoreBootstrapModal.Rendering;

namespace MvcCoreBootstrapButton
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders an Mvc Core Bootstrap button.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="configAction">Action that implements button configuration.</param>
        /// <returns>Button html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapButton(this IHtmlHelper htmlHelper,
            Action<MvcCoreBootstrapButtonBuilder> configAction)
        {
            ButtonConfig config = new ButtonConfig();

            configAction(new MvcCoreBootstrapButtonBuilder(config));

            return(new ButtonRenderer(new ModalRenderer(), new TooltipRenderer()).Render(config));
        }

        /// <summary>
        /// Renders an Mvc Core Bootstrap button group.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="configAction">Action that implements button group configuration.</param>
        /// <returns>Button group html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapButtonGroup(this IHtmlHelper htmlHelper,
            Action<MvcCoreBootstrapButtonGroupBuilder> configAction)
        {
            GroupConfig config = new GroupConfig();

            configAction(new MvcCoreBootstrapButtonGroupBuilder(config));

            return(new GroupRenderer().Render(config));
        }
    }
}
