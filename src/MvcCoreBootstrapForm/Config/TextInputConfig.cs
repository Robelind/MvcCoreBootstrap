namespace MvcCoreBootstrapForm.Config
{
    public class TextInputConfig : ControlConfig
    {
        public string Format { get; set; }
        public object HtmlAttributes { get; set; }
        public string PlaceHolder { get; set; }
        public bool ReadOnly { get; set; }
        public bool Password { get; set; }
        public string Prepend { get; set; }
        public string Append { get; set; }
        public string PrependIcon { get; set; }
        public string PrependIconPrefix { get; set; }
        public string AppendIcon { get; set; }
        public string AppendIconPrefix { get; set; }
    }
}
