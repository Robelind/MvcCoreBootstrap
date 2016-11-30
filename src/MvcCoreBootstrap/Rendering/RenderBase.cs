using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrap.Rendering
{
    public class RenderBase
    {
        protected TagBuilder Element;

        protected void BaseConfig(ConfigBase config, string cssClass = null, string statePrefix = null)
        {
            Debug.Assert(Element != null);
            this.AddAttribute(Element, "id", config.Id);
            this.AddAttribute(Element, "name", config.Name);
            Element.AddCssClass(cssClass);
            this.AddCssClasses(config.CssClasses);
            if(statePrefix != null)
            {
                this.AddContextualState(Element, config.State, statePrefix);
            }
        }

        protected void AddElement(TagBuilder element, string content, TagBuilder parentElement = null)
        {
            if(!string.IsNullOrEmpty(content))
            {
                TagBuilder parent = parentElement ?? Element;
            
                element.InnerHtml.AppendHtml(content);
                parent.InnerHtml.AppendHtml(element);
            }
        }

        protected void AddElement(TagBuilder element, IEnumerable<string> cssClasses, string content,
            TagBuilder parentElement = null)
        {
            if(!string.IsNullOrEmpty(content))
            {
                TagBuilder parent = parentElement ?? Element;
            
                element.InnerHtml.AppendHtml(content);
                this.AddCssClasses(cssClasses, element);
                parent.InnerHtml.AppendHtml(element);
            }
        }

        protected void AddAttribute(TagBuilder element, string attribute, string value)
        {
            if(!string.IsNullOrEmpty(value))
            {
                element.Attributes.Add(attribute, value);
            }
        }

        protected void AddAttribute(string attribute, bool condition, TagBuilder element = null)
        {
            if(condition)
            {
                (element ?? Element).Attributes.Add(attribute, null);
            }
        }

        protected void AddCssClass(string cssClass, bool condition, TagBuilder element = null)
        {
            if(condition)
            {
                (element ?? Element).AddCssClass(cssClass);
            }
        }

        protected void AddCssClasses(IEnumerable<string> cssClasses, TagBuilder element = null)
        {
            foreach(var cssClass in cssClasses)
            {
                (element ?? Element).AddCssClass(cssClass);
            }
        }

        protected void AddContextualState(TagBuilder element, ContextualState state, string prefix = null)
        {
            element.AddCssClass(prefix + state.ToString().ToLower());
        }

        protected void ConfigAjax(TagBuilder element, AjaxConfigBase config, string url = null,
            bool dataOnSuccess = false, string id = null)
        {
            if(config != null)
            {
                element.Attributes.Add("data-ajax", "true");
                element.Attributes.Add("data-ajax-update", $"#{config.UpdateId}");
                element.Attributes.Add("data-ajax-mode", config.UpdateMode.ToString().ToLower());
                element.Attributes.Add("data-ajax-url", url ?? config.Url);
                element.Attributes.Add("data-ajax-loading", "#" + config.BusyIndicatorId);
                element.Attributes.Add("data-ajax-begin", $"{this.AddJavascriptFuncPars(config.Start, id)}");
                element.Attributes.Add("data-ajax-success", $"{this.AddJavascriptFuncPars(config.Success, id, true, dataOnSuccess)}");
                element.Attributes.Add("data-ajax-failure", $"{this.AddJavascriptFuncPars(config.Error, id)}");
                element.Attributes.Add("data-ajax-complete", $"{this.AddJavascriptFuncPars(config.Complete, id)}");
            }
        }

        protected string AddJavascriptFuncPars(string jsFunc, string id, bool forAjax = true, bool data = false)
        {
            if(jsFunc != null && id != null)
            {
                jsFunc = forAjax
                    ? (data ? jsFunc + $"('{id}', data);" : jsFunc + $"('{id}');")
                    : jsFunc + $"('{id}');";
            }

            return(jsFunc);
        }

        protected void AddJavaScript(Action<StringBuilder> codeAction, bool onload = true)
        {
            StringBuilder js = new StringBuilder(@"<script type=""text/javascript"">");

            if(onload)
            {
                js.Append("$(function() {");
            }
            codeAction(js);
            if(onload)
            {
                js.Append("});");
            }
            js.Append("</script>");

            Element.InnerHtml.AppendHtml(js.ToString());
        }
    }
}
