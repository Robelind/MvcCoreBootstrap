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
        public MvcCoreBootstrapTextInputBuilder Disabled(bool disabled = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.Disabled = disabled));
        }

        /// <summary>
        /// Renders the check box inline, i.e. next to other check boxes.
        /// </summary>
        /// <returns>The check box builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder Inline()
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.Inline = true));
        }

        /// <summary>
        /// Do not generate a label automatically for the check box.
        /// </summary>
        /// <returns>The check box builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder NoLabel()
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.AutoLabel = false));
        }

        /// <summary>
        /// Sets the label for the check box.
        /// </summary>
        /// <param name="label">check box label.</param>
        /// <returns>The check box builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder Label(string label)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.Label = label));
        }

        /// <summary>
        /// Sets a css class for the check box element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the check box element.</param>
        /// <returns>The check box builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapTextInputBuilder>(_config.CssClasses, cssClass, condition));
        }
    }
}