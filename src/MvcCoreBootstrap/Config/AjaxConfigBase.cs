namespace MvcCoreBootstrap.Config
{
    public class AjaxConfigBase
    {
        public AjaxConfigBase()
        {
            UpdateMode = AjaxUpdateMode.Replace;
        }

        public string Url { get; set; }
        public string UpdateId { get; set; }
        public string BusyIndicatorId { get; set; }
        public string Start { get; set; }
        public string Success { get; set; }
        public string Error { get; set; }
        public string Complete { get; set; }
        public AjaxUpdateMode UpdateMode { get; set; }
    }
}
