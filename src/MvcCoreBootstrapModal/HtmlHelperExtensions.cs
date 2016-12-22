using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapModal.Builders;
using MvcCoreBootstrapModal.Config;
using MvcCoreBootstrapModal.Rendering;

namespace MvcCoreBootstrapModal
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders an Mvc Core Bootstrap modal.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="configAction">Action that implements modal configuration.</param>
        /// <returns>Modal html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapModal(this IHtmlHelper htmlHelper,
            Action<MvcCoreBootstrapModalBuilder> configAction)
        {
            ModalConfig config = new ModalConfig();

            configAction(new MvcCoreBootstrapModalBuilder(config));

            return(new ModalRenderer().Render(config));
        }
    }
}
