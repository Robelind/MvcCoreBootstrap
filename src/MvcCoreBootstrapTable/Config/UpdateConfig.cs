using System.Collections.Generic;
using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapTable.Config
{
    internal class UpdateConfig : AjaxConfigBase
    {
        public UpdateConfig()
        {
            CustomQueryPars = new Dictionary<string, string>();
        }

        public Dictionary<string, string> CustomQueryPars { get; set; }
    }
}
