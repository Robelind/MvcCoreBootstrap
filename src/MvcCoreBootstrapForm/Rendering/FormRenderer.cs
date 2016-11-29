using System.Collections.Generic;
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
            ViewBufferTextWriter a = htmlHelper.ViewContext.Writer as ViewBufferTextWriter;
            string formTag = null;
            TagBuilder formBuilder = null;
            object x = htmlHelper.ViewBag.xxx;

            if(x == null)
            {
                htmlHelper.ViewBag.xxx = "xxx";
            }

            foreach(ViewBufferValue viewBufferValue in a.Buffer.Pages[0].Buffer.Where(b => b.Value != null))
            {
                string s = viewBufferValue.Value.ToString();

                if(formTag != null)
                {
                    formTag += s;
                    if(s == ">")
                    {
                        StringWriter writers = new StringWriter();

                        formBuilder = htmlParser.TagBuilderFromHtmlContent(new HtmlString(formTag.Replace(@"\", "")), false);
                        formBuilder.TagRenderMode = TagRenderMode.StartTag;
                        formBuilder.AddCssClass("test");
                        formBuilder.WriteTo(writers, HtmlEncoder.Default);
                        content.Add(writers.ToString());
                        formTag = null;
                    }
                }
                else if(s != "<")
                {
                    content.Add(s);
                }
                else
                {
                    formTag = "<";
                }
            }
            a.Buffer.Clear();
            foreach(var s in content)
            {
                a.WriteLine(s);
            }

            return(form);
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