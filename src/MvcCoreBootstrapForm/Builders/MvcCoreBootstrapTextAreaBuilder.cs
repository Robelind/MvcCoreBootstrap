using MvcCoreBootstrap.Building;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Builders
{
    public class MvcCoreBootstrapTextAreaBuilder : BuilderBase
    {
        private readonly TextAreaConfig _config;

        internal MvcCoreBootstrapTextAreaBuilder(TextAreaConfig config)
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