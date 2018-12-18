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
            IEnumerable<ModelError> serverSideErrors = _config.ExcludePropertyErrors
                ? _htmlHelper.ViewContext.ModelState.Where(keyValuePair => keyValuePair.Key == string.Empty)
                    .SelectMany(keyValuePair => keyValuePair.Value.Errors).ToList()
                : _htmlHelper.ViewContext.ModelState.SelectMany(keyValuePair => keyValuePair.Value.Errors).ToList();
            string alertClasses = $"alert alert-{_config.State.ToString().ToLower()}";
            IHtmlContent summary = _htmlHelper.ValidationSummary(_config.ExcludePropertyErrors, "", new {@class = alertClasses});

            if(serverSideErrors.Any())
            {
                TagBuilder msg = new TagBuilder("span");

                Element = new TagBuilder("div");
                this.BaseConfig(_config, "alert", "alert-");
                Element.Attributes.Add("role", "alert");

                if(serverSideErrors.Count() == 1)
                {
                    msg.InnerHtml.Append(serverSideErrors.First().ErrorMessage);
                    Element.InnerHtml.AppendHtml(msg);
                }
                else
                {
                    TagBuilder msgs = new TagBuilder("ul");

                    foreach (ModelError error in serverSideErrors)
                    {
                        msg = new TagBuilder("li");
                        msg.InnerHtml.Append(error.ErrorMessage);
                        msgs.InnerHtml.AppendHtml(msg);
                    }
                    Element.InnerHtml.AppendHtml(msgs);
                }
                summary = Element;
            }

            return(summary);
        }
    }
}