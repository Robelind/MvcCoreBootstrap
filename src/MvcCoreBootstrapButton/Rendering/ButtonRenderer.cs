using System.Diagnostics;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapButton.Config;

namespace MvcCoreBootstrapButton.Rendering
{
    internal interface IButtonRenderer
    {
        IHtmlContent Render(ButtonConfig config);
    }

    internal class ButtonRenderer : RenderBase, IButtonRenderer
    {
        private ButtonConfig _config;
        private TagBuilder _button;

        public IHtmlContent Render(ButtonConfig config)
        {
            _config = config;
            _button = _config.Url != null || _config.Ajax != null
                ? new TagBuilder("a")
                : new TagBuilder("button");
            Element = config.Dropdown != null ? new TagBuilder("div") : _button;
            this.BaseConfig(config, config.Dropdown != null ? "btn-group" : "btn", "btn-");
            _button.AddCssClass("btn");
            if(_config.Url == null)
            {
                this.AddAttribute(_button, "type", _config.Submit ? "submit" : "button");
            }
            else
            {
                _button.Attributes.Add("role", "button");
            }
            this.AddAttribute(_button, "href", config.Url);
            this.AddAttribute(_button, "onclick", this.AddJavascriptFuncPars(config.Click, _config.Id, false));
            if(!string.IsNullOrEmpty(config.Text))
            {
                _button.InnerHtml.Append(config.Text);
            }
            this.AddContextualState(_button, config.State, "btn-");
            this.SetSize();
            this.AddClassIf("btn-block", _config.Block);
            this.AddClassIf("active", _config.Active);
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
            this.AddCssClasses(_button, config.CssClasses);
            this.Dropdown();
            this.Ajax(_button, _config.Ajax);
                        
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
                    this.AddAttribute(_button, "onclick", js);
                }
            }
        }

        private void Dropdown()
        {
            if(_config.Dropdown != null)
            {
                TagBuilder caret = new TagBuilder("span");
                TagBuilder menu = new TagBuilder("ul");
                bool splitBtn = _config.Url != null || _config.Ajax != null;
                TagBuilder toggleBtn = null;

                if(splitBtn)
                {
                    toggleBtn = new TagBuilder("button");
                    toggleBtn.AddCssClass("dropdown-toggle");
                    toggleBtn.AddCssClass("btn");
                    toggleBtn.AddCssClass("btn-" + _config.State.ToString().ToLower());
                    toggleBtn.Attributes.Add("data-toggle", "dropdown");
                }
                else
                {
                    _button.AddCssClass("dropdown-toggle");
                    _button.Attributes.Add("data-toggle", "dropdown");
                }

                caret.AddCssClass("caret");
                Element.InnerHtml.AppendHtml(_button);
                if(toggleBtn != null)
                {
                    TagBuilder srOnly = new TagBuilder("span");

                    Element.InnerHtml.AppendHtml(toggleBtn);
                    toggleBtn.InnerHtml.AppendHtml(caret);
                    srOnly.AddCssClass("sr-only");
                    toggleBtn.InnerHtml.AppendHtml(srOnly);
                }
                else
                {
                    _button.InnerHtml.AppendHtml(caret);
                }
                Element.InnerHtml.AppendHtml(menu);

                menu.AddCssClass("dropdown-menu");
                foreach(DropdownItemConfig item in _config.Dropdown.Items)
                {
                    TagBuilder menuItem = new TagBuilder("li");
                    TagBuilder link = new TagBuilder("a");

                    if(item.Separated)
                    {
                        TagBuilder separator = new TagBuilder("li");

                        separator.AddCssClass("divider");
                        separator.Attributes.Add("role", "separator");
                        menu.InnerHtml.AppendHtml(separator);
                    }

                    link.InnerHtml.Append(item.Text);
                    menuItem.InnerHtml.AppendHtml(link);

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

                    menu.InnerHtml.AppendHtml(menuItem);
                }
            }
        }

        private void AddClassIf(string cssClass, bool condition)
        {
            if(condition)
            {
                _button.AddCssClass(cssClass);
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
                case MvcCoreBootstrapButtonSize.ExtraSmall:
                    _button.AddCssClass("btn-xs");
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }
    }
}