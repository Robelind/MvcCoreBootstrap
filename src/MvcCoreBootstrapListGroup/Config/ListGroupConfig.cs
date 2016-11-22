using System.Collections.Generic;
using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapListGroup.Config
{
    internal class ListGroupConfig : ConfigBase
    {
        public ListGroupConfig()
        {
            Items = new List<ListGroupItem>();
        }

        public IList<ListGroupItem> Items { get; set; }
        public bool TrackActive { get; set; }
    }
}
