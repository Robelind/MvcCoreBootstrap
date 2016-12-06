namespace MvcCoreBootstrapForm.Config
{
    internal class TextAreaConfig : ControlConfig
    {
        public object HtmlAttributes { get; set; }
        public int Rows { get; set; }
        public bool ReadOnly { get; set; }
    }
}
