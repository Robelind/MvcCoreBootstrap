﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapMediaObject.Config;

namespace MvcCoreBootstrapMediaObject.Rendering
{
    internal interface IMediaObjectRenderer
    {
        IHtmlContent Render(MediaObjectConfig config);
    }

    internal class MediaObjectRenderer : RenderBase, IMediaObjectRenderer
    {
        public IHtmlContent Render(MediaObjectConfig config)
        {
            TagBuilder body = new TagBuilder("div");
            TagBuilder heading = new TagBuilder("h5");

            Element = new TagBuilder("div");
            this.BaseConfig(config, "media");
            if(!string.IsNullOrWhiteSpace(config.ImagePath))
            {
                TagBuilder image = new TagBuilder("img");

                image.Attributes.Add("src", config.ImagePath);
                image.Attributes.Add("alt", config.ImageAlt);
                image.AddCssClass("mr-3");
                Element.InnerHtml.AppendHtml(image);
            }
            heading.AddCssClass("mt-0");
            heading.InnerHtml.AppendHtml(config.Heading);
            body.AddCssClass("media-body");
            body.InnerHtml.AppendHtml(heading);
            body.InnerHtml.AppendHtml(config.Text);
            Element.InnerHtml.AppendHtml(body);

            return(Element);
        }

   }
}