using MvcCoreBootstrap.Building;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Builders
{
    public class MvcCoreBootstrapTextInputBuilder : BuilderBase
    {
        private readonly TextInputConfig _config;

        internal MvcCoreBootstrapTextInputBuilder(TextInputConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets a place holder text for the text input.
        /// </summary>
        /// <param name="text">Place holder text.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder PlaceHolder(string text)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.PlaceHolder = text));
        }

        /// <summary>
        /// Do not generate a label automatically for the text input.
        /// </summary>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder NoLabel()
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.AutoLabel = false));
        }

        /// <summary>
        /// Sets the label for the text input.
        /// </summary>
        /// <param name="text">Text input label.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder Label(string text)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.Label = text));
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
        /// Sets the read only state for the text input.
        /// </summary>
        /// <param name="conditon">If true, the text input is read only.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder ReadOnly(bool conditon = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.ReadOnly = conditon));
        }

        /// <summary>
        /// Configures the text input for password input.
        /// </summary>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder Password()
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.Password = true));
        }

        /// <summary>
        /// Prepends a text for the text input, making it an input group.
        /// </summary>
        /// <param name="text">Text to prepend.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder Prepend(string text)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.Prepend = text));
        }

        /// <summary>
        /// Prepends an icon for the text input, making it an input group.
        /// </summary>
        /// <param name="icon">Name of the icon.</param>
        /// <param name="prefix">Icon prefix.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder PrependIcon(string icon, string prefix = "glyphicon")
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() =>
            {
                _config.PrependIcon = icon;
                _config.PrependIconPrefix = prefix;
            }));
        }

        /// <summary>
        /// Appends a text for the text input, making it an input group.
        /// </summary>
        /// <param name="text">Text to append.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder Append(string text)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.Append = text));
        }

        /// <summary>
        /// Appends an icon for the text input, making it an input group.
        /// </summary>
        /// <param name="icon">Name of the icon.</param>
        /// <param name="prefix">Icon prefix.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder AppendIcon(string icon, string prefix = "glyphicon")
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() =>
            {
                _config.AppendIcon = icon;
                _config.AppendIconPrefix = prefix;
            }));
        }

        /// <summary>
        /// Sets a css class for the text input element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be applied to the text input.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapTextInputBuilder>(_config.CssClasses, cssClass, condition));
        }
    }
}