using System;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Builders
{
    public class MvcCoreBootstrapRadioButtonBuilder : BuilderBase
    {
        private readonly ControlConfig _config;

        internal MvcCoreBootstrapRadioButtonBuilder(ControlConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the disabled state for the radio button.
        /// </summary>
        /// <param name="disabled">If true, the radio button is disabled</param>
        /// <returns>The radio button builder instance.</returns>
        public MvcCoreBootstrapRadioButtonBuilder Disabled(bool disabled = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapRadioButtonBuilder>(() => _config.Disabled = disabled));
        }

        /// <summary>
        /// Sets the label for the radio button.
        /// </summary>
        /// <param name="label">radio button label.</param>
        /// <returns>The radio button builder instance.</returns>
        public MvcCoreBootstrapRadioButtonBuilder Label(string label)
        {
            return(this.SetConfigProp<MvcCoreBootstrapRadioButtonBuilder>(() => _config.Label = label));
        }

        /// <summary>
        /// Sets a css class for the radio button element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the radio button element.</param>
        /// <returns>The radio button builder instance.</returns>
        public MvcCoreBootstrapRadioButtonBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapRadioButtonBuilder>(_config.CssClasses, cssClass, condition));
        }

        /// <summary>
        /// Tooltip for the radio button.
        /// </summary>
        /// <param name="content">Tooltip content.</param>
        /// <param name="configAction">Action that implements tooltip configuration.</param>
        /// <returns>The radio button builder instance.</returns>
        public MvcCoreBootstrapRadioButtonBuilder Tooltip(string content, Action<MvcCoreBootstrapTooltipBuilder> configAction = null)
        {
            this.Tooltip(content, _config, configAction);
            return(this);
        }
    }
}