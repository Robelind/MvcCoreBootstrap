using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Config;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapListGroup.Rendering;
using MvcCoreBootstrapPanel.Config;
using MvcCoreBootstrapTable.Rendering;

namespace MvcCoreBootstrapPanel.Rendering
{
    internal interface IPanelRenderer
    {
        IHtmlContent Render(PanelConfig config, ITableRenderer tableRenderer, IListGroupRenderer listGroupRenderer);
    }

    internal class PanelRenderer : RenderBase, IPanelRenderer
    {
        public IHtmlContent Render(PanelConfig config, ITableRenderer tableRenderer, IListGroupRenderer listGroupRenderer)
        {
            TagBuilder body = new TagBuilder("div");

            Element = new TagBuilder("div");
            this.BaseConfig(config, "panel", "panel-");
            this.Header(config);
            this.Body(config, body);
            if(tableRenderer != null)
            {
                Element.InnerHtml.AppendHtml(tableRenderer.Render());
            }
            if(listGroupRenderer != null)
            {
                Element.InnerHtml.AppendHtml(listGroupRenderer.Render());
            }
            this.AddElement(new TagBuilder("div"), new []{"panel-footer"}, config.Footer);
                        
            return(Element);
        }

        private void Body(PanelConfig config, TagBuilder body)
        {
            if(config.Ajax != null)
            {
                string id = Guid.NewGuid().ToString();
                string triggerId = Guid.NewGuid().ToString();
                string indicatorId = null;
                TagBuilder ajaxTrigger = new TagBuilder("a");
                AjaxConfigBase ajaxConfig = new AjaxConfigBase
                {
                    Url = config.Ajax.Url,
                    UpdateId = id,
                    UpdateMode = AjaxUpdateMode.Replace,
                };

                this.AddElement(body, new[] {"panel-body"}, " ");
                body.Attributes.Add("id", id);
                ajaxTrigger.Attributes.Add("style", "display:none;");
                ajaxTrigger.Attributes.Add("id", triggerId);
                body.InnerHtml.AppendHtml(ajaxTrigger);
                if(!string.IsNullOrEmpty(config.Ajax.IndicatorPath))
                {
                    TagBuilder container = new TagBuilder("div");
                    TagBuilder indicator = new TagBuilder("img");

                    indicatorId = Guid.NewGuid().ToString();
                    indicator.Attributes.Add("src", config.Ajax.IndicatorPath);
                    indicator.AddCssClass(config.Ajax.IndicatorCss);
                    container.Attributes.Add("id", indicatorId);
                    container.Attributes.Add("style", "text-align: center;");
                    container.InnerHtml.AppendHtml(indicator);
                    body.InnerHtml.AppendHtml(container);
                }
                if(!string.IsNullOrEmpty(config.Ajax.ErrorContent))
                {
                    TagBuilder container = new TagBuilder("div");

                    id = Guid.NewGuid().ToString();
                    container.Attributes.Add("id", id);
                    container.Attributes.Add("style", "display:none;");
                    container.InnerHtml.AppendHtml(config.Ajax.ErrorContent);
                    body.InnerHtml.AppendHtml(container);
                    ajaxConfig.Error = $"$('#{id}').show();$('#{indicatorId}').hide();";
                }
                this.ConfigAjax(ajaxTrigger, ajaxConfig);
                this.AddJavaScript(sb => sb.Append($"$('#{triggerId}').click();"));
            }
            else
            {
                this.AddElement(body, new[] {"panel-body"}, config.Body);
            }
        }

        private void Header(PanelConfig config)
        {
            if(!string.IsNullOrEmpty(config.Title) || !string.IsNullOrEmpty(config.Heading))
            {
                TagBuilder heading = new TagBuilder("div");

                heading.AddCssClass("panel-heading");
                Element.InnerHtml.AppendHtml(heading);
                if(!string.IsNullOrEmpty(config.Title))
                {
                    this.AddElement(new TagBuilder("h3"), new []{"panel-title"}, config.Title, heading);
                }
                else
                {
                    heading.InnerHtml.Append(config.Heading);
                }
            }
        }
    }
}