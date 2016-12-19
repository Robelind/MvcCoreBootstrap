using System.Collections.Generic;

namespace MvcCoreBootstrapForm.Config
{
    public class RowConfig
    {
        public RowConfig()
        {
            Columns = new List<ColumnConfig>();
        }

        public IList<ColumnConfig> Columns { get; set; }
    }
}
