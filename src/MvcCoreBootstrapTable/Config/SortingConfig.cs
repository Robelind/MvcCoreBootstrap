namespace MvcCoreBootstrapTable.Config
{
    internal class SortingConfig
    {
        public SortingConfig()
        {
            IconLib = "glyphicon";
            AscendingCssClass = "glyphicon-chevron-up";
            DescendingCssClass = "glyphicon-chevron-down";
        }

        public string IconLib { get; set; }
        public string AscendingCssClass { get; set; }
        public string DescendingCssClass { get; set; }
    }
}
