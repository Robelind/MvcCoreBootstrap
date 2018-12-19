using System;
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
        /// Do not generate a label automatically for the dropdown.
        /// </summary>
        /// <returns>The dropdown builder instance.</returns>
        public MvcCoreBootstrapDropdownBuilder NoLabel()
        {
            return(this.SetConfigProp<MvcCoreBootstrapDropdownBuilder>(() => _config.AutoLabel = false));
        }

        /// <summary>
        /// Sets the label for the dropdown.
        /// </summary>
        /// <param name="label">Text input label.</param>
        /// <returns>The dropdown builder instance.</returns>
        public MvcCoreBootstrapDropdownBuilder Label(string label)
        {
            return(this.SetConfigProp<MvcCoreBootstrapDropdownBuilder>(() => _config.Label = label));
        }

        /// <summary>
        /// Sets the disabled state for the text input.
        /// </summary>
        /// <param name="disabled">If true, the text input is disabled</param>
        /// <returns>The dropdown builder instance.</returns>
        public MvcCoreBootstrapDropdownBuilder Disabled(bool disabled = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapDropdownBuilder>(() => _config.Disabled = disabled));
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

        /// <summary>
        /// Tooltip for the dropdown.
        /// </summary>
        /// <param name="content">Tooltip content.</param>
        /// <param name="configAction">Action that implements tooltip configuration.</param>
        /// <returns>The dropdown builder instance.</returns>
        public MvcCoreBootstrapDropdownBuilder Tooltip(string content, Action<MvcCoreBootstrapTooltipBuilder> configAction = null)
        {
            this.Tooltip(content, _config, configAction);
            return(this);
        }
    }
}