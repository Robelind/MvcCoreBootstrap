using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapPanel.Builders;
using MvcCoreBootstrapPanel.Config;
using MvcCoreBootstrapPanel.Rendering;
using MvcCoreBootstrapTable.Config;

namespace MvcCoreBootstrapPanel
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders an Mvc Core Bootstrap panel.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="configAction">Action that implements panel configuration.</param>
        /// <returns>Panel html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapPanel(this IHtmlHelper htmlHelper,
            Action<MvcCoreBootstrapPanelBuilder> configAction)
        {
            PanelConfig panelConfig = new PanelConfig();
            MvcCoreBootstrapPanelBuilder builder = new MvcCoreBootstrapPanelBuilder(panelConfig,
                new TableConfigHandler(), htmlHelper.ViewContext.HttpContext); 

            configAction(builder);

            return(new PanelRenderer().Render(panelConfig, builder.TableRenderer, builder.ListGroupRenderer));
        }
    }
}
