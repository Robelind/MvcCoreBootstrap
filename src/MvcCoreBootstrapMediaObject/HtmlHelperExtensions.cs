using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapMediaObject.Builders;
using MvcCoreBootstrapMediaObject.Config;
using MvcCoreBootstrapMediaObject.Rendering;

namespace MvcCoreBootstrapMediaObject
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders an Mvc Core Bootstrap media object.
        /// </summary>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="heading">Media object heading.</param>
        /// <param name="text">Media object text.</param>
        /// <param name="configAction">Action that implements media object configuration.</param>
        /// <returns>Media object html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapMediaObject(this IHtmlHelper htmlHelper, string heading, string text,
            Action<MvcCoreBootstrapMediaObjectBuilder> configAction = null)
        {
            MediaObjectConfig config = new MediaObjectConfig {Heading = heading, Text = text};

            configAction?.Invoke(new MvcCoreBootstrapMediaObjectBuilder(config));

            return(new MediaObjectRenderer().Render(config));
        }
    }
}
