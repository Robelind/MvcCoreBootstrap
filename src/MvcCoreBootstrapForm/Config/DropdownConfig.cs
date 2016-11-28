using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapForm.Config
{
    internal class DropdownConfig : ConfigBase
    {
        public IEnumerable<SelectListItem> Items { get; set; }
        public bool Multiple { get; set; }
    }
}
