using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public IHtmlContent Render(AlertConfig config)
        {
            Element = new TagBuilder("div");
            this.BaseConfig(config, "alert", "alert-");
            Element.Attributes.Add("role", "alert");

            if(config.ModelState != null)
            {
                this.ValidationSummary(config);
            }
            else
            {
                this.Alert(config);
            }
                        
            return(Element);
        }

        private void Alert(AlertConfig config)
        {
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
        }

        private void ValidationSummary(AlertConfig config)
        {
            IEnumerable<ModelError> errors = config.ModelState.SelectMany(keyValuePair => keyValuePair.Value.Errors).ToList();
            //string stateClass = config.ModelState.IsValid ? "validation-summary-valid" : "validation-summary-errors";

            Element.Attributes.Add("data-valmsg-summary", "true");
            if(!errors.Any())
            {
                Element.Attributes.Add("style", "display:none;");
            }
            //Element.AddCssClass(stateClass);

            TagBuilder msg = new TagBuilder("span");

            if(errors.Count() > 1)
            {
                msg.Attributes.Add("style", "display:none;");
            }
            else if(errors.Count() == 1)
            {
                msg.InnerHtml.Append(errors.First().ErrorMessage);
            }
            Element.InnerHtml.AppendHtml(msg);

            TagBuilder msgs = new TagBuilder("ul");

            foreach(ModelError error in errors)
            {
                msg = new TagBuilder("li");
                msg.InnerHtml.Append(error.ErrorMessage);
                msgs.InnerHtml.AppendHtml(msg);
            }
            if(errors.Count() == 1)
            {
                msgs.Attributes.Add("style", "display:none;");
            }
            Element.InnerHtml.AppendHtml(msgs);
            this.AddJavaScript(sb => sb.Append(string.Format(ValidationJs, config.Id)));
        }

        private string ValidationJs { get; } =
        @"$('#{0}').closest('form').bind('invalid-form.validate', function () {{
            $('#{0}').show();
            if ($('#{0} ul').children().length > 1) {{
                $('#{0} ul').show();
                $('#{0} span').hide();
            }} else {{
                $('#{0} span').html($('#{0} ul li').first().text());
                $('#{0} span').show();
                $('#{0} ul').hide();
            }}
        }});";
    }
}