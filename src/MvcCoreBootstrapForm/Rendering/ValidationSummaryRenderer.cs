using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal interface IValidationSummaryRenderer
    {
        IHtmlContent Render();
    }

    internal class ValidationSummaryRenderer : RenderBase, IValidationSummaryRenderer
    {
        private readonly ValidationSummaryConfig _config;
        private readonly IHtmlHelper _htmlHelper;

        public ValidationSummaryRenderer(ValidationSummaryConfig config, IHtmlHelper htmlHelper)
        {
            _config = config;
            _htmlHelper = htmlHelper;
        }

        public IHtmlContent Render()
        {
            IEnumerable<ModelError> errors = _config.ModelState.SelectMany(keyValuePair => keyValuePair.Value.Errors).ToList();
            TagBuilder msg = new TagBuilder("span");
            TagBuilder msgs = new TagBuilder("ul");

            Element = new TagBuilder("div");
            this.BaseConfig(_config, "alert", "alert-");
            Element.Attributes.Add("role", "alert");

            Element.Attributes.Add("data-valmsg-summary", "true");
            if(!errors.Any())
            {
                Element.Attributes.Add("style", "display:none;");
            }

            if(errors.Count() > 1)
            {
                msg.Attributes.Add("style", "display:none;");
            }
            else if(errors.Count() == 1)
            {
                msg.InnerHtml.Append(errors.First().ErrorMessage);
            }
            Element.InnerHtml.AppendHtml(msg);

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
            this.AddJavaScript(sb => sb.Append(string.Format(ValidationJs, _config.Id)));
                        
            return(Element);
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