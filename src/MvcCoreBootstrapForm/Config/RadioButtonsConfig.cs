using System.Collections.Generic;

namespace MvcCoreBootstrapForm.Config
{
    internal class RadioButtonsConfig : ControlConfig
    {
        public RadioButtonsConfig()
        {
            RadioButtons = new List<RadioButtonConfig>();
        }

        public bool Horizontal { get; set; }
        public IList<RadioButtonConfig> RadioButtons { get; set; }
    }
}
