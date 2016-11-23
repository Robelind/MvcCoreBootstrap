using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapAlert.Config;

namespace MvcCoreBootstrapAlert.Rendering
{
    internal interface IAlertRenderer
    {
        IHtmlContent Render(AlertConfig config);
    }

    internal class AlertRenderer : RenderBase, IAlertRenderer
    {
        private AlertConfig _config;

        public IHtmlContent Render(AlertConfig config)
        {
            _config = config;
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