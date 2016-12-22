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
            TagBuilder dialog = new TagBuilder("div");
            TagBuilder content = new TagBuilder("div");
            TagBuilder header = new TagBuilder("div");
            TagBuilder body = new TagBuilder("div");
            TagBuilder footer = new TagBuilder("div");
            
            Element = new TagBuilder("div");
            this.BaseConfig(config, "modal");
            Element.Attributes.Add("role", "dialog");
            Element.Attributes.Add("tabindex", "-1");
            Element.InnerHtml.AppendHtml(dialog);

            dialog.AddCssClass("modal-dialog");
            dialog.Attributes.Add("role", "document");
            dialog.InnerHtml.AppendHtml(content);
            
            content.AddCssClass("modal-content");
            content.InnerHtml.AppendHtml(header);
            content.InnerHtml.AppendHtml(body);
            content.InnerHtml.AppendHtml(footer);
            
            header.AddCssClass("modal-header");
            if(config.Dismissable)
            {
                TagBuilder closeBtn = new TagBuilder("button");
                TagBuilder x = new TagBuilder("span");

                closeBtn.Attributes.Add("type", "button");
                closeBtn.AddCssClass("close");
                closeBtn.Attributes.Add("data-dismiss", "modal");
                closeBtn.Attributes.Add("aria-label", "Close");
                closeBtn.Attributes.Add("aria-hidden", "true");
                x.InnerHtml.AppendHtml("&times;");
                closeBtn.InnerHtml.AppendHtml(x);
                header.InnerHtml.AppendHtml(closeBtn);
            }
            header.InnerHtml.AppendHtml(config.Title);
            
            body.AddCssClass("modal-body");
            body.InnerHtml.AppendHtml(config.Body);
            
            footer.AddCssClass("modal-footer");
            if(config.CloseBtnText != null)
            {
                TagBuilder closeBtn = new TagBuilder("button");

                closeBtn.AddCssClass("btn");
                this.AddContextualState(closeBtn, config.CloseBtnState);
                closeBtn.Attributes.Add("data-dismiss", "modal");
                closeBtn.InnerHtml.AppendHtml(config.CloseBtnText);
                footer.InnerHtml.AppendHtml(closeBtn);
            }

            return(Element);
        }
    }
}