using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IHtmlParser _htmlParser;

        public FormRenderer(FormParameters parameters)
        {
            _parameters = parameters;
            _config = _parameters.Config;
            _htmlHelper = parameters.HtmlHelper;
            _htmlParser = parameters.Parser;
        }

        internal FormRenderer(FormConfig config, IHtmlHelper htmlHelper, IHtmlParser htmlParser)
        {
            _config = config;
            _htmlHelper = htmlHelper;
            _htmlParser = htmlParser;
        }

        public MvcForm Render()
        {
            IList<string> content = new List<string>();
            MvcForm form = this.CreateForm();
            ViewBufferTextWriter viewWriter = _htmlHelper.ViewContext.Writer as ViewBufferTextWriter;
            string formTag = null;
            StringWriter formWriter = new StringWriter();
            TagBuilder formBuilder = null;

            Debug.Assert(viewWriter != null);
            foreach(ViewBufferValue viewBufferValue in viewWriter.Buffer.Pages[0].Buffer.Where(b => b.Value != null))
            {
                string value = viewBufferValue.Value.ToString();

                if(formTag != null)
                {
                    formTag += value;
                    if(value == ">")
                    {
                        formBuilder = _htmlParser.TagBuilderFromHtmlContent(new HtmlString(formTag), false);
                        formTag = null;
                    }
                }
                else if(value != "<")
                {
                    content.Add(value);
                }
                else
                {
                    formTag = "<";
                }
            }

            Debug.Assert(formBuilder != null);
            this.AddCssClass("form-horizontal", _config.ColumnWidths != null, formBuilder);
            this.AddCssClass("form-inline", _config.Inline, formBuilder);
            formBuilder.TagRenderMode = TagRenderMode.StartTag;
            formBuilder.WriteTo(formWriter, HtmlEncoder.Default);
            content.Add(formWriter.ToString());

            // Replace view writer content.
            viewWriter.Buffer.Clear();
            foreach(var s in content)
            {
                viewWriter.WriteLine(s);
            }

            // Add validation handling java script, if not already added.
            if(_htmlHelper.ViewBag.MvcBootStrapFormValJs == null)
            {
                viewWriter.WriteLine(ValidationJs);
                _htmlHelper.ViewBag.MvcBootStrapFormValJs = "MvcBootStrapFormValJs";
            }

            _htmlHelper.ViewBag.MvcBootStrapFormColumnWidths = _config.ColumnWidths;

            return(form);
        }

         private MvcForm CreateForm()
        {
            MvcForm form;

            if(_parameters.AntiForgery.HasValue)
            {
                form = _htmlHelper.BeginForm(_parameters.Method, _parameters.AntiForgery,
                    _parameters.HtmlAttributes);
            }
            else if(_parameters.HtmlAttributes != null)
            {
                form = _htmlHelper.BeginForm(_parameters.ActionName, _parameters.ControllerName, _parameters.Method,
                    _parameters.HtmlAttributes);
            }
            else if(_parameters.RouteValues != null)
            {
                form = _htmlHelper.BeginForm(_parameters.ActionName, _parameters.ControllerName, _parameters.Method,
                    _parameters.RouteValues);
            }
            else
            {
                form = _htmlHelper.BeginForm(_parameters.Method);
            }

            return(form);
        }

        private string ValidationJs { get; } =
    @"<script type=""text/javascript"">
        $(document).ready(function() {
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