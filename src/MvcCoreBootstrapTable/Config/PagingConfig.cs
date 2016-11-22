namespace MvcCoreBootstrapTable.Config
{
    internal class PagingConfig
    {
        public PagingConfig()
        {
            IconLib = "glyphicon";
            FirstCssClass = "glyphicon-fast-backward";
            PreviousCssClass = "glyphicon-step-backward";
            NextCssClass = "glyphicon-step-forward";
            LastCssClass = "glyphicon-fast-forward";
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
