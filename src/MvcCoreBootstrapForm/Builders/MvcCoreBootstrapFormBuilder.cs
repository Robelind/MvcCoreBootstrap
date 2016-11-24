using MvcCoreBootstrap.Building;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Builders
{
    public class MvcCoreBootstrapFormBuilder : BuilderBase
    {
        private readonly FormConfig _config;

        internal MvcCoreBootstrapFormBuilder(FormConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the id attribute for the form.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The form builder instance.</returns>
        public MvcCoreBootstrapFormBuilder Id(string id)
        {
            return(this.SetConfigProp<MvcCoreBootstrapFormBuilder>(() => _config.Id = id));
        }

        /// <summary>
        /// Sets the name attribute for the form.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The form builder instance.</returns>
        public MvcCoreBootstrapFormBuilder Name(string name)
        {
            return(this.SetConfigProp<MvcCoreBootstrapFormBuilder>(() => _config.Name = name));
        }

        /// <summary>
        /// Sets a css class for the form element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the form element.</param>
        /// <returns>The form builder instance.</returns>
        public MvcCoreBootstrapFormBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapFormBuilder>(_config.CssClasses, cssClass, condition));
        }
    }
}