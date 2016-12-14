namespace MvcCoreBootstrapForm.Config
{
    internal class RadioButtonConfig : ControlConfig
    {
        public RadioButtonConfig()
        {
            AutoLabel = false;
        }

        public object Value { get; set; }
    }
}
