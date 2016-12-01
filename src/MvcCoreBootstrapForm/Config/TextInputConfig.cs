namespace MvcCoreBootstrapForm.Config
{
    public class TextInputConfig : ControlConfig
    {
        public string PlaceHolder { get; set; }
        public bool ReadOnly { get; set; }
        public bool Password { get; set; }
        public string Prepend { get; set; }
        public string Append { get; set; }
    }
}
