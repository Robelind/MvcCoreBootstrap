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
        /// <param name="placeHolder">Place holder text.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder PlaceHolder(string placeHolder)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.PlaceHolder = placeHolder));
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
        /// <param name="label">Text input label.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder Label(string label)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.Label = label));
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
        /// <param name="disabled">If true, the text input is read only.</param>
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
        /// <param name="prepend">String to prepend.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder Prepend(string prepend)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.Prepend = prepend));
        }

        /// <summary>
        /// Appends a text for the text input, making it an input group.
        /// </summary>
        /// <param name="append">String to append.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder Append(string append)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.Append = append));
        }

        /// <summary>
        /// Sets a css class for the text input element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the text input element.</param>
        /// <returns>The text input builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapTextInputBuilder>(_config.CssClasses, cssClass, condition));
        }
    }
}