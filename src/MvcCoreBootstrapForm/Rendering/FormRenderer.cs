using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
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
            ViewBufferTextWriter viewWriter = _htmlHelper.ViewContext.Writer as ViewBufferTextWriter;
            IDictionary<string, object> htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(_parameters.HtmlAttributes);

            // If so configured, add the appropriate class to the forms html attributes.
            if(_config.ColumnWidths != null || _config.Inline)
            {
                string formClass = (_config.ColumnWidths != null ? "form-horizontal" : null) ?? "form-inline";

                htmlAttributes = htmlAttributes ?? new Dictionary<string, object>();
                if(htmlAttributes.ContainsKey("class"))
                {
                    htmlAttributes["class"] += $" {formClass}";
                }
                else
                {
                    htmlAttributes.Add("class", formClass);
                }
            }

            // TODO: To js-file?
            // Add validation handling java script, if not already added.
            if(_htmlHelper.ViewBag.MvcBootStrapFormValJs == null)
            {
                viewWriter.WriteLine(ValidationJs);
                _htmlHelper.ViewBag.MvcBootStrapFormValJs = "MvcBootStrapFormValJs";
            }

            _htmlHelper.ViewBag.MvcBootStrapFormColumnWidths = _config.ColumnWidths;

            return(_htmlHelper.BeginForm(_parameters.ActionName, _parameters.ControllerName,
                    _parameters.RouteValues, _parameters.Method, _parameters.AntiForgery, htmlAttributes));
        }

        private string ValidationJs { get; } =
    @"<script type=""text/javascript"">
        $(document).ready(function() {
            $('.field-validation-valid').addClass('text-danger');
            $('form').bind('invalid-form.validate', function (a, b) {
                $('.has-error').removeClass('has-error');
                $.each(b.errorList, function () {
                    if ($(this.element).is('input[type=radio]')) {
                        $('[id=' + this.element.id + ']').closest('.radio').addClass('has-error');
                        $('[id=' + this.element.id + ']').closest('.radio-inline').addClass('has-error');
                    } else {
                        $(this.element).closest('.form-group').addClass('has-error');
                    }
                });
            });
        });
    </script>";
    }
}