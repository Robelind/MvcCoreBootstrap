using System.Diagnostics;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapButton.Config;
using MvcCoreBootstrapModal.Config;
using MvcCoreBootstrapModal.Rendering;

namespace MvcCoreBootstrapButton.Rendering
{
    internal interface IButtonRenderer
    {
        IHtmlContent Render(ButtonConfig config, bool inGroup = false);
    }

    internal class ButtonRenderer : RenderBase, IButtonRenderer
    {
        private readonly IModalRenderer _modalRenderer;
        private readonly ITooltipRenderer _tooltipRenderer;
        private ButtonConfig _config;
        private TagBuilder _button;
        //private readonly IHtmlContentBuilder _builder = new HtmlContentBuilder();

        public ButtonRenderer(IModalRenderer modalRenderer, ITooltipRenderer tooltipRenderer)
        {
            _modalRenderer = modalRenderer;
            _tooltipRenderer = tooltipRenderer;
        }

        public IHtmlContent Render(ButtonConfig config, bool inGroup = false)
        {
            _config = config;
            _button = _config.Url != null || _config.Ajax != null
                ? new TagBuilder("a")
                : new TagBuilder("button");
            Element = config.Dropdown != null ? new TagBuilder("div") : _button;
            //_builder.AppendHtml(Element);
            if(config.Dropdown != null)
            {
                Element.AddCssClass(inGroup || _config.Url != null || _config.Ajax != null ? "btn-group" : "dropdown");
            }
            else
            {
                this.BaseConfig(config, config.Dropdown != null ? "btn-group" : "btn", "btn-");
            }
            _button.AddCssClass("btn");
            if(_config.Url == null)
            {
                this.AddAttribute("type", _config.Submit ? "submit" : "button", _button);
            }
            else
            {
                _button.Attributes.Add("role", "button");
            }
            this.AddAttribute("href", config.Url, _button);
            this.AddAttribute("onclick", this.AddJavascriptFuncPars(config.Click, _config.Id, false), _button);
            if(!string.IsNullOrEmpty(config.Text))
            {
                _button.InnerHtml.Append(config.Text);
            }
            this.AddElement(new TagBuilder("span"), new[] {"badge badge-light"}, _config.Badge);
            this.AddContextualState(_button, config.State, "btn-", _config.Outline ? "outline" : null);
            this.SetSize();
            this.AddCssClass("btn-block", _config.Block);
            this.AddCssClass("active", _config.Active);
            if(_config.Disabled)
            {
                if(_config.Url == null)
                {
                    _button.Attributes.Add("disabled", "disabled");
                }
                else
                {
                    _button.AddCssClass("disabled");
                }
            }
            this.AddCssClasses(config.CssClasses, _button);
            this.Dropdown();
            this.Ajax(_button, _config.Ajax);
            this.TriggerModal(_button, _config.Modal);
            this.Collapse(_button, _config.CollapseId);
            _tooltipRenderer.Render(Element, _config.Tooltip);
            
            return(Element);
        }

        private void Ajax(TagBuilder element, AjaxConfig config)
        {
            if(config != null)
            {
                this.ConfigAjax(element, config, null, true, _config.Id);
                if(config.ClearUpdateArea)
                {
                    string js = element.Attributes.ContainsKey("onclick")
                        ? element.Attributes["onclick"]
                        : null;

                    element.Attributes.Remove("onclick");
                    js += $"$('#{config.UpdateId}').html('');";
                    this.AddAttribute("onclick", js, _button);
                }
            }
        }

        private void Dropdown()
        {
            if(_config.Dropdown != null)
            {
                TagBuilder menu = new TagBuilder("div");
                bool splitBtn = _config.Url != null || _config.Ajax != null;
                TagBuilder toggleBtn = null;

                if(splitBtn)
                {
                    toggleBtn = new TagBuilder("button");
                    toggleBtn.AddCssClass("dropdown-toggle");
                    toggleBtn.AddCssClass("dropdown-toggle-split");
                    toggleBtn.AddCssClass("btn");
                    toggleBtn.AddCssClass("btn-" + _config.State.ToString().ToLower());
                    toggleBtn.Attributes.Add("data-toggle", "dropdown");
                }
                else
                {
                    _button.AddCssClass("dropdown-toggle");
                    _button.Attributes.Add("data-toggle", "dropdown");
                }

                Element.InnerHtml.AppendHtml(_button);
                if(toggleBtn != null)
                {
                    TagBuilder srOnly = new TagBuilder("span");

                    Element.InnerHtml.AppendHtml(toggleBtn);
                    srOnly.AddCssClass("sr-only");
                    toggleBtn.InnerHtml.AppendHtml(srOnly);
                }
                Element.InnerHtml.AppendHtml(menu);

                menu.AddCssClass("dropdown-menu");
                foreach(DropdownItemConfig item in _config.Dropdown.Items)
                {
                    TagBuilder link = new TagBuilder("a");

                    if(item.Separated)
                    {
                        TagBuilder separator = new TagBuilder("div");

                        separator.AddCssClass("dropdown-divider");
                        menu.InnerHtml.AppendHtml(separator);
                    }

                    link.AddCssClass("dropdown-item");
                    link.InnerHtml.Append(item.Text);
                    //menuItem.InnerHtml.AppendHtml(link);

                    this.Ajax(link, item.Ajax);
                    if(item.JsHandler != null)
                    {
                        link.Attributes.Add("onclick", item.JsHandler);
                        link.Attributes.Add("href", "javascript:void(0)");
                    }
                    else
                    {
                        link.Attributes.Add("href", item.Url);
                    }

                    menu.InnerHtml.AppendHtml(link);
                }
            }
        }

        private void TriggerModal(TagBuilder button, ModalConfig modal)
        {
            if(modal != null || _config.ModalId != null)
            {

                button.Attributes.Add("data-toggle", "modal");
                button.Attributes.Add("data-target", "#" + (modal != null ? modal.Id : _config.ModalId));
                //if(modal != null)
                //{
                //    _builder.AppendHtml(_modalRenderer.Render(modal));
                //}
            }
        }

        private void Collapse(TagBuilder button, string collapseId)
        {
            if(collapseId != null)
            {
                button.Attributes.Add("data-toggle", "collapse");
                button.Attributes.Add("data-target", collapseId.StartsWith("#") ? collapseId : $"#{collapseId}");
            }
        }

        private void SetSize()
        {
            switch(_config.Size)
            {
                case MvcCoreBootstrapButtonSize.Large:
                    _button.AddCssClass("btn-lg");
                    break;
                case MvcCoreBootstrapButtonSize.Default:
                    break;
                case MvcCoreBootstrapButtonSize.Small:
                    _button.AddCssClass("btn-sm");
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }
    }
}