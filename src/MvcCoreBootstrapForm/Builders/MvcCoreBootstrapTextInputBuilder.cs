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