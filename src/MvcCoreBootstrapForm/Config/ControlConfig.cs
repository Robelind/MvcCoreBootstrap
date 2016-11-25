using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapForm.Config
{
    public class ControlConfig : ConfigBase
    {
        public ControlConfig()
        {
            AutoLabel = true;
        }

        public bool AutoLabel { get; set; }
        public string Label { get; set; }
        public bool Disabled { get; set; }
    }
}