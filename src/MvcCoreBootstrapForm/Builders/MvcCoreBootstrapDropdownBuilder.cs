using MvcCoreBootstrap.Building;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Builders
{
    public class MvcCoreBootstrapDropdownBuilder : BuilderBase
    {
        private readonly DropdownConfig _config;

        internal MvcCoreBootstrapDropdownBuilder(DropdownConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the id attribute for the dropdown.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The dropdown builder instance.</returns>
        public MvcCoreBootstrapDropdownBuilder Id(string id)
        {
            return(this.SetConfigProp<MvcCoreBootstrapDropdownBuilder>(() => _config.Id = id));
        }

        /// <summary>
        /// Sets the name attribute for the dropdown.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The dropdown builder instance.</returns>
        public MvcCoreBootstrapDropdownBuilder Name(string name)
        {
            return(this.SetConfigProp<MvcCoreBootstrapDropdownBuilder>(() => _config.Name = name));
        }

        /// <summary>
        /// Configures the dropdown for multi select.
        /// </summary>
        /// <returns>The dropdown builder instance.</returns>
        public MvcCoreBootstrapDropdownBuilder Multiple()
        {
            return(this.SetConfigProp<MvcCoreBootstrapDropdownBuilder>(() => _config.Multiple = true));
        }

        /// <summary>
        /// Configures whether no item will be initially selected in the dropdown.
        /// </summary>
        /// <returns>The dropdown builder instance.</returns>
        public MvcCoreBootstrapDropdownBuilder NoInitialSelection(bool condition)
        {
            return(this.SetConfigProp<MvcCoreBootstrapDropdownBuilder>(() => _config.NoInitialSelection = condition));
        }

        /// <summary>
        /// Sets the disabled state for the text input.
        /// </summary>
        /// <param name="disabled">If true, the text input is disabled</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder Disabled(bool disabled = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.Disabled = disabled));
        }

        /// <summary>
        /// Sets a css class for the dropdown element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the dropdown element.</param>
        /// <returns>The dropdown builder instance.</returns>
        public MvcCoreBootstrapDropdownBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapDropdownBuilder>(_config.CssClasses, cssClass, condition));
        }
    }
}