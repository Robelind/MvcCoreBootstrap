using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapCard.Builders;
using MvcCoreBootstrapCard.Config;
using MvcCoreBootstrapCard.Rendering;
using MvcCoreBootstrapTable.Config;

namespace MvcCoreBootstrapCard
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders an Mvc Core Bootstrap panel.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="configAction">Action that implements panel configuration.</param>
        /// <returns>Panel html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapCard(this IHtmlHelper htmlHelper,
            Action<MvcCoreBootstrapCardBuilder> configAction)
        {
            CardConfig cardConfig = new CardConfig();
            MvcCoreBootstrapCardBuilder builder = new MvcCoreBootstrapCardBuilder(cardConfig,
                new TableConfigHandler(), htmlHelper.ViewContext.HttpContext); 

            configAction(builder);

            return(new CardRenderer().Render(cardConfig, builder.TableRenderer, builder.ListGroupRenderer));
        }
    }
}
