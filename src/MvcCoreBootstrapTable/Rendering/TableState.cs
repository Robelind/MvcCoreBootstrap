using System.Collections.Generic;

namespace MvcCoreBootstrapTable.Rendering
{
    internal class TableState
    {
        public string SortProp { get; set; }
        public bool AscSort { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string CurrentFilter { get; set; }
        public string ContainerId { get; set; }
        public Dictionary<string, string> Filter { get; set; }
    }
}
