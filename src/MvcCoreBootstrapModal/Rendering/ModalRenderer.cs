﻿using System.Diagnostics;
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
            TagBuilder title = new TagBuilder("h4");

            Element = new TagBuilder("div");
            this.BaseConfig(config, "modal");
            Element.Attributes.Add("role", "dialog");
            Element.Attributes.Add("tabindex", "-1");
            this.AddCssClass("fade", config.Animation);
            Element.InnerHtml.AppendHtml(dialog);
            this.SetSize(config, dialog);

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

            title.AddCssClass("modal-title");
            title.InnerHtml.Append(config.Title);
            header.InnerHtml.AppendHtml(title);

            body.AddCssClass("modal-body");
            if(config.BodyHtml != null)
            {
                body.InnerHtml.AppendHtml(config.BodyHtml);
            }
            else
            {
                body.InnerHtml.AppendHtml(config.Body);
            }

            this.Footer(config, footer);

            return(Element);
        }

        private void Footer(ModalConfig config, TagBuilder footer)
        {
            footer.AddCssClass("modal-footer");
            foreach(ModalButton modalButton in config.Buttons)
            {
                TagBuilder button = new TagBuilder("button");

                button.AddCssClass("btn");
                this.AddContextualState(button, modalButton.State, "btn-");
                if(modalButton.JsFunc != null)
                {
                    button.Attributes.Add("data-mvccorebootstrap-modal-btn-action", modalButton.JsFunc +  "()");
                }
                else
                {
                    button.Attributes.Add("data-dismiss", "modal");
                }
                button.InnerHtml.AppendHtml(modalButton.Text);
                footer.InnerHtml.AppendHtml(button);
            }
        }

        private void SetSize(ModalConfig config, TagBuilder dialog)
        {
            switch(config.Size)
            {
                case MvcCoreBootstrapModalSize.Large:
                    dialog.AddCssClass("modal-lg");
                    break;
                case MvcCoreBootstrapModalSize.Default:
                    break;
                case MvcCoreBootstrapModalSize.Small:
                    dialog.AddCssClass("modal-sm");
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }
    }
}