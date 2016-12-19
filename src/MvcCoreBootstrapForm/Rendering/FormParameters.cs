using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Rendering
{
    internal class FormParameters
    {
        public FormConfig Config { get; set; }
        public IHtmlHelper HtmlHelper { get; set; }
        public HtmlParser Parser { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public object RouteValues { get; set; }
        public FormMethod Method { get; set; }
        public bool? AntiForgery { get; set; }
        public object HtmlAttributes { get; set; }
    }
}
