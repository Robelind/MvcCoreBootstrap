using System;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Builders
{
    public class MvcCoreBootstrapRadioButtonsBuilder : BuilderBase
    {
        private readonly RadioButtonsConfig _config;

        internal MvcCoreBootstrapRadioButtonsBuilder(RadioButtonsConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Places the radio buttons horizontally instead of vertically.
        /// </summary>
        /// <returns>The radion buttons builder instance.</returns>
        public MvcCoreBootstrapRadioButtonsBuilder Horizontal()
        {
            return(this.SetConfigProp<MvcCoreBootstrapRadioButtonsBuilder>(() => _config.Horizontal = true));
        }

        /// <summary>
        /// Do not generate a label automatically for the radio buttons.
        /// </summary>
        /// <returns>The radio buttons builder instance.</returns>
        public MvcCoreBootstrapRadioButtonsBuilder NoLabel()
        {
            return(this.SetConfigProp<MvcCoreBootstrapRadioButtonsBuilder>(() => _config.AutoLabel = false));
        }

        /// <summary>
        /// Sets the label for the radio buttons.
        /// </summary>
        /// <param name="label">radio buttons label.</param>
        /// <returns>The radio buttons builder instance.</returns>
        public MvcCoreBootstrapRadioButtonsBuilder Label(string label)
        {
            return(this.SetConfigProp<MvcCoreBootstrapRadioButtonsBuilder>(() => _config.Label = label));
        }

        /// <summary>
        /// Sets the disabled state for the radio buttons.
        /// </summary>
        /// <param name="disabled">If true, the radio buttons are disabled</param>
        /// <returns>The radio buttons builder instance.</returns>
        public MvcCoreBootstrapRadioButtonsBuilder Disabled(bool disabled = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapRadioButtonsBuilder>(() => _config.Disabled = disabled));
        }

        /// <summary>
        /// Configures a radio button.
        /// </summary>
        /// <param name="value">Radio button value.</param>
        /// <param name="configAction">Action that implements radio button configuration.</param>
        /// <returns>The radion buttons builder instance.</returns>
        public MvcCoreBootstrapRadioButtonsBuilder RadioButton(object value,
            Action<MvcCoreBootstrapRadioButtonBuilder> configAction = null)
        {
            RadioButtonConfig config = new RadioButtonConfig { Value = value };
            MvcCoreBootstrapRadioButtonBuilder builder = new MvcCoreBootstrapRadioButtonBuilder(config);

            configAction?.Invoke(builder);
            _config.RadioButtons.Add(config);

            return(this);
        }
    }
}