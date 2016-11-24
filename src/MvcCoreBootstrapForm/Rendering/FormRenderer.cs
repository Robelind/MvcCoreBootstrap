using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal interface IFormRenderer
    {
        IHtmlContent Render(FormConfig config);
    }

    internal class FormRenderer : RenderBase, IFormRenderer
    {
        public IHtmlContent Render(FormConfig config)
        {
            Element = new TagBuilder("form");
            this.BaseConfig(config);
            Element.Attributes.Add("action", config.Url);
            Element.Attributes.Add("method", "post");

            this.AddElement(new TagBuilder("button"), new [] {"btn", "btn-success"}, "Submit");

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