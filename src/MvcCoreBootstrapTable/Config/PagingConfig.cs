namespace MvcCoreBootstrapTable.Config
{
    internal class PagingConfig
    {
        public PagingConfig()
        {
            IconLib = "fa";
            FirstCssClass = "fa-fast-backward";
            PreviousCssClass = "fa-step-backward";
            NextCssClass = "fa-step-forward";
            LastCssClass = "fa-fast-forward";
        }

        public int PageSize { get; set; }
        public bool DirectPageAccess { get; set; }
        public bool PageInfo { get; set; }
        public string IconLib { get; set; }
        public string FirstCssClass { get; set; }
        public string PreviousCssClass { get; set; }
        public string NextCssClass { get; set; }
        public string LastCssClass { get; set; }
    }
}
