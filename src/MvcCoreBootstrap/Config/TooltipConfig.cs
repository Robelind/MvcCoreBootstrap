namespace MvcCoreBootstrap.Config
{
    public class TooltipConfig
    {
        internal TooltipConfig(string content)
        {
            Content = content;
            Placement = Placement.Top;
            Trigger = ToolTipTrigger.Hover;
        }

        public string Content { get; set; }
        public Placement Placement { get; set; }
        public ToolTipTrigger Trigger { get; set; }
        public int ShowDelay { get; set; }
        public int HideDelay { get; set; }
    }
}
