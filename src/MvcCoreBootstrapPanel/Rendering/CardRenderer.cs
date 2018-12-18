using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Config;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapCard.Config;
using MvcCoreBootstrapListGroup.Rendering;
using MvcCoreBootstrapTable.Rendering;

namespace MvcCoreBootstrapCard.Rendering
{
    internal interface ICardRenderer
    {
        IHtmlContent Render(CardConfig config, ITableRenderer tableRenderer, IListGroupRenderer listGroupRenderer);
    }

    internal class CardRenderer : RenderBase, ICardRenderer
    {
        public IHtmlContent Render(CardConfig config, ITableRenderer tableRenderer, IListGroupRenderer listGroupRenderer)
        {
            Element = new TagBuilder("div");
            this.BaseConfig(config, "card", "bg-");
            this.AddElement(new TagBuilder("div"), new[] { "card-header" }, config.Header);
            this.Body(config);
            if(tableRenderer != null)
            {
                Element.InnerHtml.AppendHtml(tableRenderer.Render());
            }
            if(listGroupRenderer != null)
            {
                Element.InnerHtml.AppendHtml(listGroupRenderer.Render());
            }
            this.AddElement(new TagBuilder("div"), new[] { "card-footer" }, config.Footer);
                        
            return(Element);
        }

        private void Body(CardConfig config)
        {
            if(config.Ajax != null || config.Content != null || config.HtmlContent != null)
            {
                TagBuilder body = new TagBuilder("div");

                Element.InnerHtml.AppendHtml(body);
                body.AddCssClass("card-body");
                this.AddElement(new TagBuilder("h5"), new[] { "card-title" }, config.Title, body);
                this.AddElement(new TagBuilder("h6"), new[] { "card-subtitle" }, config.SubTitle, body);

                if(config.Ajax != null)
                {
                    string id = Guid.NewGuid().ToString();
                    string indicatorId = Guid.NewGuid().ToString();
                    TagBuilder ajaxTrigger = new TagBuilder("a");
                    AjaxConfigBase ajaxConfig = new AjaxConfigBase
                    {
                        Url = config.Ajax.Url,
                        UpdateId = indicatorId,
                        UpdateMode = AjaxUpdateMode.Replace,
                    };
                    TagBuilder container = new TagBuilder("div");

                    body.Attributes.Add("id", id);

                    ajaxTrigger.Attributes.Add("style", "display:none;");
                    ajaxTrigger.Attributes.Add("data-mvccorebootstrap-panel-ajax", null);
                    body.InnerHtml.AppendHtml(ajaxTrigger);
                    container.Attributes.Add("id", indicatorId);
                    body.InnerHtml.AppendHtml(container);
                    if(!string.IsNullOrEmpty(config.Ajax.IndicatorPath))
                    {
                        TagBuilder indicator = new TagBuilder("img");

                        indicator.Attributes.Add("src", config.Ajax.IndicatorPath);
                        indicator.AddCssClass(config.Ajax.IndicatorCss);
                        container.InnerHtml.AppendHtml(indicator);
                    }
                    if(!string.IsNullOrEmpty(config.Ajax.ErrorContent))
                    {
                        TagBuilder error = new TagBuilder("div");
                        string errorId = Guid.NewGuid().ToString();

                        error.Attributes.Add("id", errorId);
                        error.Attributes.Add("style", "display:none;");
                        error.InnerHtml.AppendHtml(config.Ajax.ErrorContent);
                        body.InnerHtml.AppendHtml(error);
                        ajaxConfig.Error = $"$('#{errorId}').show();$('#{indicatorId}').hide();";
                    }
                    this.ConfigAjax(ajaxTrigger, ajaxConfig);
                }
                else
                {
                    this.AddElement(new TagBuilder("p"), new[] { "card-text" }, config.HtmlContent, body);
                    this.AddElement(new TagBuilder("p"), new[] { "card-text" }, config.Content, body);
                }
            }
        }
    }
}