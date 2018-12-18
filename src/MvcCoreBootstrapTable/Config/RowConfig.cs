using System.Collections.Generic;
using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapTable.Config
{
    internal class RowConfig : ConfigBase
    {
        public object Entity { get; set; }

        public RowConfig(object entity)
        {
            Entity = entity;
            Active = true;
            CellConfigs = new Dictionary<string, CellConfig>();
        }

        public bool Active { get; set; }
        public string NavigationUrl { get; set; }
        public string RowClick { get; set; }
        public Dictionary<string, CellConfig> CellConfigs;
    }
}
