using MvcCoreBootstrap.Building;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Builders
{
    public class MvcCoreBootstrapCheckBoxBuilder : BuilderBase
    {
        private readonly CheckBoxConfig _config;

        internal MvcCoreBootstrapCheckBoxBuilder(CheckBoxConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the disabled state for the check box.
        /// </summary>
        /// <param name="disabled">If true, the check box is disabled</param>
        /// <returns>The check box builder instance.</returns>
        public MvcCoreBootstrapCheckBoxBuilder Disabled(bool disabled = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapCheckBoxBuilder>(() => _config.Disabled = disabled));
        }

        /// <summary>
        /// Renders the check box inline, i.e. next to other check boxes.
        /// </summary>
        /// <returns>The check box builder instance.</returns>
        public MvcCoreBootstrapCheckBoxBuilder Inline()
        {
            return(this.SetConfigProp<MvcCoreBootstrapCheckBoxBuilder>(() => _config.Horizontal = true));
        }

        /// <summary>
        /// Do not generate a label automatically for the check box.
        /// </summary>
        /// <returns>The check box builder instance.</returns>
        public MvcCoreBootstrapCheckBoxBuilder NoLabel()
        {
            return(this.SetConfigProp<MvcCoreBootstrapCheckBoxBuilder>(() => _config.AutoLabel = false));
        }

        /// <summary>
        /// Sets the label for the check box.
        /// </summary>
        /// <param name="label">check box label.</param>
        /// <returns>The check box builder instance.</returns>
        public MvcCoreBootstrapCheckBoxBuilder Label(string label)
        {
            return(this.SetConfigProp<MvcCoreBootstrapCheckBoxBuilder>(() => _config.Label = label));
        }

        /// <summary>
        /// Sets a css class for the check box element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the check box element.</param>
        /// <returns>The check box builder instance.</returns>
        public MvcCoreBootstrapCheckBoxBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapCheckBoxBuilder>(_config.CssClasses, cssClass, condition));
        }
    }
}