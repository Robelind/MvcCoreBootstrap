using MvcCoreBootstrap.Building;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Builders
{
    public class MvcCoreBootstrapFormBuilder : BuilderBase
    {
        private readonly FormConfig _config;

        internal MvcCoreBootstrapFormBuilder(FormConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Configures the form for horizontal layout.
        /// </summary>
        /// <param name="left">Width of the left (label) column.</param>
        /// <param name="right">Width of the right (control) column.</param>
        /// <returns>The form builder instance.</returns>
        public MvcCoreBootstrapFormBuilder Horizontal(ColumnWidth left, ColumnWidth right)
        {
            return(this.SetConfigProp<MvcCoreBootstrapFormBuilder>(() =>
                _config.ColumnWidths = new ColumnWidths {LeftColumn = left, RightColumn = right}));
        }

        /// <summary>
        /// Configures the form for inline layout.
        /// </summary>
        /// <returns>The form builder instance.</returns>
        public MvcCoreBootstrapFormBuilder Inline()
        {
            return(this.SetConfigProp<MvcCoreBootstrapFormBuilder>(() => _config.Inline = true));
        }

        /// <summary>
        /// Sets a css class for the form.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the form element.</param>
        /// <returns>The form builder instance.</returns>
        public MvcCoreBootstrapFormBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapFormBuilder>(_config.CssClasses, cssClass, condition));
        }
    }
}