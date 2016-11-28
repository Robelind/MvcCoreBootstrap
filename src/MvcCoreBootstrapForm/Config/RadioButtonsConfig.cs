using System.Collections.Generic;

namespace MvcCoreBootstrapForm.Config
{
    public class RadioButtonsConfig<TModel, TResult> : ControlConfig
    {
        public RadioButtonsConfig()
        {
            RadioButtons = new List<RadioButtonConfig<TModel, TResult>>();
        }

        public bool Horizontal { get; set; }
        public IList<RadioButtonConfig<TModel, TResult>> RadioButtons { get; set; }
    }
}
