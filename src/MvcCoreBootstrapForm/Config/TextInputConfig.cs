using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapForm.Config
{
    public class TextInputConfig : ConfigBase
    {
        public TextInputConfig()
        {
            AutoLabel = true;
        }

        public string PlaceHolder { get; set; }
        public bool AutoLabel { get; set; }
        public string Label { get; set; }
    }
}
