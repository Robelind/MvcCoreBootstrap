using Microsoft.AspNetCore.Html;

namespace MvcCoreBootstrapForm.Config
{
    public class ColumnConfig
    {
        public ColumnWidth Width { get; set; }
        public IHtmlContent Content { get; set; }
    }
}
