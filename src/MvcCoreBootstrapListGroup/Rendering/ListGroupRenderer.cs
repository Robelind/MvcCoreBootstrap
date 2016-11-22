using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Config;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapListGroup.Config;

namespace MvcCoreBootstrapListGroup.Rendering
{
    internal interface IListGroupRenderer
    {
        IHtmlContent Render();
    }

    internal class ListGroupRenderer : RenderBase, IListGroupRenderer
    {
        private readonly ListGroupConfig _config;

        private enum ItemTypes
        {
            Texts, Links, Buttons
        }
        private ItemTypes _types;

        public ListGroupRenderer(ListGroupConfig config)
        {
            _config = config;
        }

        public IHtmlContent Render()
        {
            _types = _config.Items.Any(i => i.Url != null || i.Ajax != null)
                ? ItemTypes.Links
                : (_config.Items.Any(i => i.Button) ? ItemTypes.Buttons : ItemTypes.Texts);
            Element = new TagBuilder(_types == ItemTypes.Buttons || _types == ItemTypes.Links ? "div" : "ul");
            this.BaseConfig(_config, "list-group");
            this.Items();
            if(_config.TrackActive)
            {
                this.AddJavaScript(sb => sb.Append(string.Format(ActiveJs, _config.Id)));
            }
                        
            return(Element);
        }

        private void Items()
        {
            foreach(var item in _config.Items)
            {
                TagBuilder element = new TagBuilder(_types == ItemTypes.Buttons
                    ? "button"
                    : (_types == ItemTypes.Links ? "a" : "li"));
                IEnumerable<string> cssClasses = item.Disabled
                    ? new[] {"list-group-item", "disabled"}
                    : new[] {"list-group-item"};

                this.AddElement(element, cssClasses, item.Content);
                this.AddContextualState(element, item.State, "list-group-item-");
                if(_types == ItemTypes.Buttons && item.ClickHandler != null && !item.Disabled)
                {
                    element.Attributes.Add("onclick", item.ClickHandler.Replace("()", "") + $"({item.Id})");
                }
                if(_types == ItemTypes.Links && !item.Disabled)
                {
                    if(item.Ajax != null)
                    {
                        AjaxConfigBase ajaxConfig = new AjaxConfigBase
                        {
                            Url = item.Ajax.Url, UpdateId = item.Ajax.UpdateId, UpdateMode = AjaxUpdateMode.Replace,
                        };

                        this.ConfigAjax(element, ajaxConfig);
                        element.Attributes.Add("href", "#");
                    }
                    else
                    {
                        element.Attributes.Add("href", item.Url);
                    }
                }
                if(!string.IsNullOrEmpty(item.Badge))
                {
                    this.AddElement(new TagBuilder("span"), new[] {"badge"}, item.Badge, element);
                }
            }
        }

        private const string ActiveJs =
        @"$('#{0} .list-group-item').click(function () {{
            if (!$(this).hasClass('disabled')) {{
                $('#{0} .list-group-item').removeClass('active');
                $(this).addClass('active');
            }}
        }});";
    }
}