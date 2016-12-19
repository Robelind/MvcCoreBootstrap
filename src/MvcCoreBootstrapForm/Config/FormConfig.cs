using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapForm.Config
{
    internal class ColumnWidths
    {
        public ColumnWidth LeftColumn { get; set; }
        public ColumnWidth RightColumn { get; set; }
    }

    internal class FormConfig : ConfigBase
    {
        public FormConfig()
        {
            PropertyValidationMessages = true;
        }

        public bool Inline { get; set; }
        public ColumnWidths ColumnWidths { get; set; }
        public bool PropertyValidationMessages { get; set; }
    }
}
