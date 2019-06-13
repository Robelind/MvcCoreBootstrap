using Microsoft.AspNetCore.Html;
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
            heading.AddCssClass("mt-0");
            heading.InnerHtml.AppendHtml(config.Heading);

            //if(config.Dismissable)
            //{
            //    TagBuilder button = new TagBuilder("button");
            //    TagBuilder x = new TagBuilder("span");

            //    Element.AddCssClass("alert-dismissible");
            //    button.AddCssClass("close");
            //    button.Attributes.Add("data-dismiss", "alert");
            //    button.Attributes.Add("aria-label", "Close");
            //    x.InnerHtml.AppendHtml("&times;");
            //    x.Attributes.Add("aria-hidden", "true");
            //    button.InnerHtml.AppendHtml(x);
            //    Element.InnerHtml.AppendHtml(button);
            //}
            body.InnerHtml.AppendHtml(heading);
            body.InnerHtml.AppendHtml(config.Text);
                        
            return(Element);
        }

   }
}