using System.Collections.Generic;

namespace MvcCoreBootstrapTable.Config
{
    internal class FilteringConfig
    {
        public FilteringConfig()
        {
            CssClasses = new List<string>();
        }

        public int Threshold { get; set; }
        public IList<string> CssClasses { get; }
        public bool Prepopulated { get; set; }
        public string Initial { get; set; }
    }
}
