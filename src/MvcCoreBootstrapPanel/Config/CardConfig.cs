using Microsoft.AspNetCore.Html;
using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapCard.Config
{
    internal class CardConfig : ConfigBase
    {
        public string Header { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }
        public IHtmlContent HtmlContent { get; set; }
        public string Footer { get; set; }
        public AjaxConfig Ajax { get; set; }
    }
}
