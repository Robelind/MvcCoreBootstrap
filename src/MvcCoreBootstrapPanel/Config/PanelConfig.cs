using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapPanel.Config
{
    internal class PanelConfig : ConfigBase
    {
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Footer { get; set; }
        public AjaxConfig Ajax { get; set; }
    }
}
