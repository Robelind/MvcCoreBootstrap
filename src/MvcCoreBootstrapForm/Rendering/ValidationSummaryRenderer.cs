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
            IEnumerable<ModelError> errors = _config.ExcludePropertyErrors
                ? _htmlHelper.ViewContext.ModelState.Where(keyValuePair => keyValuePair.Key == string.Empty)
                    .SelectMany(keyValuePair => keyValuePair.Value.Errors).ToList()
                : _htmlHelper.ViewContext.ModelState.SelectMany(keyValuePair => keyValuePair.Value.Errors).ToList();

            if(errors.Any() || !_config.ExcludePropertyErrors)
            {
                TagBuilder msg = new TagBuilder("span");
                TagBuilder msgs = new TagBuilder("ul");

                Element = new TagBuilder("div");
                this.BaseConfig(_config, "alert", "alert-");
                Element.Attributes.Add("role", "alert");
                Element.AddCssClass("MvcCoreBootstrapValSummary");

                Element.Attributes.Add("data-valmsg-summary", "true");
                this.AddAttribute("style", "display:none;", !errors.Any());
                this.AddCssClass("MvcCoreBootstrapNoPropErrors", _config.ExcludePropertyErrors);
                this.AddAttribute("style", "display:none;", errors.Count() > 1, msg);
                if(errors.Count() == 1)
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

                this.AddAttribute("style", "display:none;", errors.Count() == 1, msgs);
                Element.InnerHtml.AppendHtml(msgs);
            }

            return(Element);
        }
    }
}