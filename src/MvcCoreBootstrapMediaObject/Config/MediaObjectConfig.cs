using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapMediaObject.Config
{
    internal class MediaObjectConfig : ConfigBase
    {
        public string Heading { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public string ImageAlt { get; set; }
    }
}
