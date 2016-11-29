using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcCoreBootstrapForm.Config
{
    internal class DropdownConfig : ControlConfig
    {
        public IEnumerable<SelectListItem> Items { get; set; }
        public bool Multiple { get; set; }
        public bool NoInitialSelection { get; set; }
    }
}
