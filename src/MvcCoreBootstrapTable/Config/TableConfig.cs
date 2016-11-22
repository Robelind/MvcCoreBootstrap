using System.Collections.Generic;

namespace MvcCoreBootstrapTable.Config
{
    internal interface ITableConfig
    {
        string Id { get; set; }
        string Name { get; set; }
        string Caption { get; set; }
        bool Striped { get; set; }
        bool Bordered { get; set; }
        bool Condensed { get; set; }
        bool HoverState { get; set; }
        string RowClick { get; set; }
        UpdateConfig Update { get; set; }
        SortingConfig Sorting { get; set; }
        FooterConfig Footer { get; set; }
        PagingConfig Paging { get; set; }
        IList<RowConfig> Rows { get; set; }
        Dictionary<string, ColumnConfig> Columns { get; set; }
        IList<string> CssClasses { get; set; }
        string ContainerId { get; set; }
    }

    internal class TableConfig : ITableConfig
    {
        public TableConfig()
        {
            Rows = new List<RowConfig>();
            Columns = new Dictionary<string, ColumnConfig>();
            CssClasses = new List<string>();
            Footer = new FooterConfig();
            Paging = new PagingConfig();
            Sorting = new SortingConfig();
            Update = new UpdateConfig();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public bool Striped { get; set; }
        public bool Bordered { get; set; }
        public bool Condensed { get; set; }
        public bool HoverState { get; set; }
        public string RowClick { get; set; }
        public UpdateConfig Update { get; set; }
        public SortingConfig Sorting { get; set; }
        public FooterConfig Footer { get; set; }
        public PagingConfig Paging { get; set; }
        public IList<RowConfig> Rows { get; set; }
        public Dictionary<string, ColumnConfig> Columns { get; set; }
        public IList<string> CssClasses { get; set; }
        public string ContainerId { get; set; }
    }
}
