using System;
using MvcCoreBootstrap;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapTable.Config;

namespace MvcCoreBootstrapTable.Builders
{
    public class MvcCoreBootstrapTableRowBuilder<T> : BuilderBase
    {
        private readonly IBuilderFactory _builderFactory;

        internal MvcCoreBootstrapTableRowBuilder(T entity, IBuilderFactory builderFactory)
        {
            _builderFactory = builderFactory;
            Config = new RowConfig(entity);
        }

        internal RowConfig Config { get; private set; }

        /// <summary>
        /// Sets the url to navigate to when the row is clicked.
        /// </summary>
        /// <param name="url">Url</param>
        /// <returns>Row builder instance.</returns>
        public MvcCoreBootstrapTableRowBuilder<T> Navigate(string url)
        {
            Config.NavigationUrl = url;
            return(this);
        }

        /// <summary>
        /// Sets the <see cref="ContextualState"/> of the row.
        /// </summary>
        /// <param name="state">Contextual state</param>
        /// <param name="condition">If true, the contextual state will be applied.</param>
        /// <returns>Row builder instance.</returns>
        public MvcCoreBootstrapTableRowBuilder<T> Contextual(ContextualState state, bool condition = true)
        {
            Config.State = condition ? state : ContextualState.Default;
            return(this);
        }

        /// <summary>
        /// Sets a css class for the row element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the row element.</param>
        /// <returns>Row builder instance.</returns>
        public MvcCoreBootstrapTableRowBuilder<T> CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapTableRowBuilder<T>>(Config.CssClasses, cssClass, condition));
        }

        /// <summary>
        /// Configures a java script function to be called the row is clicked.
        /// </summary>
        /// <param name="jsFunc">Name of java script function.</param>
        /// <param name="condition">If true, the java script function will be called.</param>
        /// <returns>Row builder instance.</returns>
        public MvcCoreBootstrapTableRowBuilder<T> Click(string jsFunc, bool condition = true)
        {
            if(condition)
            {
                Config.RowClick = jsFunc;
            }
            return(this);
        }

        /// <summary>
        /// Configures the cells of the row.
        /// </summary>
        /// <param name="configAction">Configuration action</param>
        /// <returns>Cells builder.</returns>
        public MvcCoreBootstrapTableRowBuilder<T> Cells(Action<MvcCoreBootstrapTableCellsBuilder> configAction)
        {
            configAction(_builderFactory.CellsBuilder(Config.CellConfigs));
            return(this);
        }
    }
}
