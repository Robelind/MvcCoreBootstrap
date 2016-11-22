using System.Collections.Generic;
using MvcCoreBootstrapTable.Builders;

namespace MvcCoreBootstrapTable.Config
{
    internal class ColumnConfig
    {
        public ColumnConfig()
        {
            Visible = true;
            CssClasses = new List<string>();
            Filtering = new FilteringConfig();
        }

        public string Header { get; set; }
        public bool Visible { get; set; }
        public SortState? SortState { get; set; }
        public FilteringConfig Filtering { get; set; }
        public IList<string> CssClasses { get; private set; }
    }
}
