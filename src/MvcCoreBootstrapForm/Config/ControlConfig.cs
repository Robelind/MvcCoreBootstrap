using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapForm.Config
{
    internal class ControlConfig : TooltipConfigBase
    {
        public ControlConfig()
        {
            AutoLabel = true;
        }

        public ColumnWidths ColumnWidths { get; set; }
        public bool PropertyValidationMessages { get; set; }
        public bool AutoLabel { get; set; }
        public string Label { get; set; }
        public bool Disabled { get; set; }
    }
}