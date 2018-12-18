using System.Diagnostics;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapButton.Config;
using MvcCoreBootstrapModal.Rendering;

namespace MvcCoreBootstrapButton.Rendering
{
    internal class GroupRenderer : RenderBase
    {
        public IHtmlContent Render(GroupConfig config)
        {
            return(config.Groups != null ? this.Toolbar(config) : this.Group(config));
        }

        private IHtmlContent Group(GroupConfig config)
        {
            TagBuilder group = new TagBuilder("div");
            ButtonRenderer buttonRenderer = new ButtonRenderer(new ModalRenderer());

            group.AddCssClass(config.Vertical ? "btn-group-vertical" : "btn-group");
            this.AddCssClasses(config.CssClasses, group);
            group.Attributes.Add("role", "group");
            this.ButtonSize(@group, config);
            foreach(ButtonConfig button in config.Buttons)
            {
                if(button.Dropdown != null)
                {
                    button.Size = config.ButtonSize;
                }
                button.State = config.State;
                group.InnerHtml.AppendHtml(buttonRenderer.Render(button, true));
            }

            return group;
        }

        private IHtmlContent Toolbar(GroupConfig config)
        {
            TagBuilder toolbar = new TagBuilder("div");
            GroupRenderer groupRenderer = new GroupRenderer();

            toolbar.AddCssClass("btn-toolbar");
            toolbar.Attributes.Add("role", "toolbar");
            foreach(GroupConfig group in config.Groups)
            {
                group.ButtonSize = config.ButtonSize;
                group.State = config.State;
                toolbar.InnerHtml.AppendHtml(groupRenderer.Render(group));
            }

            return(toolbar);
        }

        private void ButtonSize(TagBuilder group, GroupConfig config)
        {
            switch(config.ButtonSize)
            {
                case MvcCoreBootstrapButtonSize.Large:
                    group.AddCssClass("btn-group-lg");
                    break;
                case MvcCoreBootstrapButtonSize.Default:
                    break;
                case MvcCoreBootstrapButtonSize.Small:
                    group.AddCssClass("btn-group-sm");
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }
    }
}

