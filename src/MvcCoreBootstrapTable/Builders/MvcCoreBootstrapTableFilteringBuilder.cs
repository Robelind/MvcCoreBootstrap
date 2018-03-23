using System;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapTable.Config;

namespace MvcCoreBootstrapTable.Builders
{
    public class MvcCoreBootstrapTableFilteringBuilder : BuilderBase
    {
        private readonly FilteringConfig _config;

        internal MvcCoreBootstrapTableFilteringBuilder(FilteringConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the number of characters to be entered to trigger manual filtering.
        /// </summary>
        /// <param name="threshold">Number of characters</param>
        /// <param name="condition">If true, filtering will be activated.</param>
        /// <returns>Filtering builder instance.</returns>
        public MvcCoreBootstrapTableFilteringBuilder Threshold(int threshold, bool condition = true)
        {
            if(threshold <= 0)
            {
                throw(new ArgumentException("Filtering threshold must be larger than zero."));
            }
            
            return(this.SetConfigProp<MvcCoreBootstrapTableFilteringBuilder>(() => _config.Threshold = condition ? threshold : 0));
        }

        /// <summary>
        /// Activates on prepopulated filtering.
        /// </summary>
        /// <param name="condition">If true, filtering will be activated.</param>
        /// <returns>Filtering builder instance.</returns>
        public MvcCoreBootstrapTableFilteringBuilder Prepopulated(bool condition = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTableFilteringBuilder>(() => _config.Prepopulated = condition));
        }

        /// <summary>
        /// Activates initial filtering.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <param name="condition">If true, initial filtering will be activated.</param>
        /// <returns>Filtering builder instance.</returns>
        public MvcCoreBootstrapTableFilteringBuilder Initial(string value, bool condition = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTableFilteringBuilder>(() => _config.Initial = condition ? value : null));
        }

        /// <summary>
        /// Sets a css class for the filtering text input element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the text input element.</param>
        /// <returns>Filtering builder instance.</returns>
        public MvcCoreBootstrapTableFilteringBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapTableFilteringBuilder>(_config.CssClasses, cssClass, condition));
        }
    }
}
