using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapListGroup.Builders;
using MvcCoreBootstrapListGroup.Config;
using MvcCoreBootstrapListGroup.Rendering;

namespace MvcCoreBootstrapListGroup
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders an Mvc Core Bootstrap list group.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="configAction">Action that implements list group configuration.</param>
        /// <returns>List group html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapListGroup(this IHtmlHelper htmlHelper,
            Action<MvcCoreBootstrapListGroupBuilder> configAction)
        {
            ListGroupConfig config = new ListGroupConfig();

            configAction(new MvcCoreBootstrapListGroupBuilder(config));

            return(new ListGroupRenderer(config).Render());
        }
    }
}
