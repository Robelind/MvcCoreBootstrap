using MvcCoreBootstrap;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapTable.Config;

namespace MvcCoreBootstrapTable.Builders
{
    public class MvcCoreBootstrapTableFooterBuilder : BuilderBase
    {
        private readonly FooterConfig _config;

        internal MvcCoreBootstrapTableFooterBuilder(FooterConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the footer text.
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="condition">If true, the text will be set for the footer.</param>
        /// <returns>Footer builder instance.</returns>
        public MvcCoreBootstrapTableFooterBuilder Text(string text, bool condition = true)  
        {
            _config.Text = condition ? text : null;
            return(this);
        }

        /// <summary>
        /// Sets the <see cref="ContextualState"/> of the footer.
        /// </summary>
        /// <param name="state">Contextual state</param>
        /// <param name="condition">If true, the contextual state will be applied.</param>
        /// <returns>Footer builder instance.</returns>
        public MvcCoreBootstrapTableFooterBuilder Contextual(ContextualState state, bool condition = true)
        {
            _config.State = condition ? state : ContextualState.Default;
            return(this);
        }

        /// <summary>
        /// Sets a css class for the footer element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the footer element.</param>
        /// <returns>Footer builder instance.</returns>
        public MvcCoreBootstrapTableFooterBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapTableFooterBuilder>(_config.CssClasses, cssClass, condition));
        }
    }
}
