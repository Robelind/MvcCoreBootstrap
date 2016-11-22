using System;
using System.Collections.Generic;

namespace MvcCoreBootstrap.Config
{
    public class ConfigBase
    {
        public ConfigBase()
        {
            Id = Guid.NewGuid().ToString();
            CssClasses = new List<string>();
            State = ContextualState.Default;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ContextualState State { get; set; }
        public IList<string> CssClasses { get; private set; }
    }
}
