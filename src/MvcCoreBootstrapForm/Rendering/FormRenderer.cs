using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal interface IFormRenderer
    {
        MvcForm Render(FormConfig config, IHtmlHelper htmlHelper, IHtmlParser htmlParser);
    }

    internal class FormRenderer : IFormRenderer
    {
        public MvcForm Render(FormConfig config, IHtmlHelper htmlHelper, IHtmlParser htmlParser)
        {
            IList<string> content = new List<string>();
            MvcForm form = htmlHelper.BeginForm();
            ViewBufferTextWriter viewWriter = htmlHelper.ViewContext.Writer as ViewBufferTextWriter;
            string formTag = null;

            Debug.Assert(viewWriter != null);
            foreach(ViewBufferValue viewBufferValue in viewWriter.Buffer.Pages[0].Buffer.Where(b => b.Value != null))
            {
                string value = viewBufferValue.Value.ToString();

                if(formTag != null)
                {
                    formTag += value;
                    if(value == ">")
                    {
                        StringWriter formWriter = new StringWriter();
                        TagBuilder formBuilder = htmlParser.TagBuilderFromHtmlContent(new HtmlString(formTag), false);

                        formBuilder.TagRenderMode = TagRenderMode.StartTag;
                        formBuilder.WriteTo(formWriter, HtmlEncoder.Default);
                        content.Add(formWriter.ToString());
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

            // Replace view writer content.
            viewWriter.Buffer.Clear();
            foreach(var s in content)
            {
                viewWriter.WriteLine(s);
            }

            // Add validation handling java script, if not already added.
            if(htmlHelper.ViewBag.MvcBootStrapFormValJs == null)
            {
                viewWriter.WriteLine(ValidationJs);
                htmlHelper.ViewBag.MvcBootStrapFormValJs = "MvcBootStrapFormValJs";
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