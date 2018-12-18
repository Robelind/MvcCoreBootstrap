namespace MvcCoreBootstrapTable.Config
{
    internal class SortingConfig
    {
        public SortingConfig()
        {
            IconLib = "fa";
            AscendingCssClass = "fa-chevron-up";
            DescendingCssClass = "fa-chevron-down";
        }

        public string IconLib { get; set; }
        public string AscendingCssClass { get; set; }
        public string DescendingCssClass { get; set; }
    }
}
