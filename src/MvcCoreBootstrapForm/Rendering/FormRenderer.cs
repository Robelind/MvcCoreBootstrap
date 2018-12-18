using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal interface IFormRenderer
    {
        MvcForm Render();
    }

    internal class FormRenderer : RenderBase, IFormRenderer
    {
        private readonly FormParameters _parameters;
        private readonly FormConfig _config;
        private readonly IHtmlHelper _htmlHelper;

        public FormRenderer(FormParameters parameters)
        {
            _parameters = parameters;
            _config = _parameters.Config;
            _htmlHelper = parameters.HtmlHelper;
        }

        public MvcForm Render()
        {
            IDictionary<string, object> htmlAttributes =
                HtmlHelper.AnonymousObjectToHtmlAttributes(_parameters.HtmlAttributes) ?? new Dictionary<string, object>();
            string classes = htmlAttributes.ContainsKey("class") ? htmlAttributes["class"].ToString() : "";

            // If so configured, add the appropriate class to the forms html attributes.
            if(_config.ColumnWidths != null || _config.Inline)
            {
                string formClass = (_config.ColumnWidths != null ? "form-horizontal" : null) ?? "form-inline";

                classes += $" {formClass}";
            }
            classes += _config.PropertyValidationMessages ? null : " MvcCoreBootstrapNoPropErrors";
            htmlAttributes["class"] = classes;

            return(_htmlHelper.BeginForm(_parameters.ActionName, _parameters.ControllerName,
                    _parameters.RouteValues, _parameters.Method, _parameters.AntiForgery, htmlAttributes));
        }
    }
}