using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapModal.Config;

namespace MvcCoreBootstrapModal.Rendering
{
    internal interface IModalRenderer
    {
        IHtmlContent Render(ModalConfig config);
    }

    internal class ModalRenderer : RenderBase, IModalRenderer
    {
        public IHtmlContent Render(ModalConfig config)
        {
            Element = new TagBuilder("div");
            this.BaseConfig(config, "alert", "alert-");
            Element.Attributes.Add("role", "alert");

            if(config.Dismissable)
            {
                TagBuilder button = new TagBuilder("button");
                TagBuilder x = new TagBuilder("span");

                Element.AddCssClass("alert-dismissible");
                button.AddCssClass("close");
                button.Attributes.Add("data-dismiss", "alert");
                button.Attributes.Add("aria-label", "Close");
                x.InnerHtml.AppendHtml("&times;");
                x.Attributes.Add("aria-hidden", "true");
                button.InnerHtml.AppendHtml(x);
                Element.InnerHtml.AppendHtml(button);
            }
            Element.InnerHtml.AppendHtml(config.Text);
                        
            return(Element);
        }

   }
}