using System.Collections.Generic;
using Microsoft.AspNetCore.Html;

namespace MvcCoreBootstrapForm.Config
{
    public class GroupConfig
    {
        public GroupConfig(IHtmlContent[] contents)
        {
            Contents = new List<IHtmlContent>(contents);
        }

        public IEnumerable<IHtmlContent> Contents { get; set; }
    }
}
