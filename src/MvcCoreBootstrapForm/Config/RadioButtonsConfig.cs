using System.Collections.Generic;

namespace MvcCoreBootstrapForm.Config
{
    public class RadioButtonsConfig<TModel> : ControlConfig
    {
        public RadioButtonsConfig()
        {
            RadioButtons = new List<RadioButtonConfig<TModel>>();
        }

        public bool Horizontal { get; set; }
        public IList<RadioButtonConfig<TModel>> RadioButtons { get; set; }
    }
}
