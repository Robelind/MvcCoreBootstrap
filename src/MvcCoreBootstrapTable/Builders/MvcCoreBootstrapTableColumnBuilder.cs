using System;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapTable.Config;

namespace MvcCoreBootstrapTable.Builders
{
    public class MvcCoreBootstrapTableColumnBuilder : BuilderBase
    {
        private readonly ColumnConfig _config;
        private readonly IBuilderFactory _builderFactory;

        internal MvcCoreBootstrapTableColumnBuilder(ColumnConfig config, IBuilderFactory builderFactory)
        {
            _config = config;
            _builderFactory = builderFactory;
        }

        /// <summary>
        /// Sets the header for the column.
        /// </summary>
        /// <param name="header">Header</param>
        /// <returns>The column builder instance.</returns>
        public MvcCoreBootstrapTableColumnBuilder Header(string header)
        {
            _config.Header = header;
            return(this);
        }

        /// <summary>
        /// Sets whether the column should be visible.
        /// </summary>
        /// <param name="visible">If true, the column will be visible.</param>
        /// <returns>The column builder instance.</returns>
        /// <remarks>
        /// Columns are visible by default.
        /// </remarks>
        public MvcCoreBootstrapTableColumnBuilder Visible(bool visible = true)
        {
            _config.Visible = visible;
            return(this);
        }

        /// <summary>
        /// Sets whether the table can be sorted by the content in the column.
        /// </summary>
        /// <param name="sortable">If true, the table can be sorted</param>
        /// <returns>The column builder instance.</returns>
        public MvcCoreBootstrapTableColumnBuilder Sortable(bool sortable = true)
        {
            if(!_config.SortState.HasValue)
            {
                _config.SortState = sortable ? SortState.None : (SortState?)null;
            }
            return(this);
        }

        /// <summary>
        /// Sets whether the column is initially sorted.
        /// </summary>
        /// <param name="ascending">If true, indicates that the column is sorted ascending, otherwise descending.</param>
        /// <returns>The column builder instance.</returns>
        public MvcCoreBootstrapTableColumnBuilder Sorted(bool ascending = true)
        {
            _config.SortState = ascending ? SortState.Ascending : SortState.Descending;
            return(this);
        }

        /// <summary>
        /// Configures filtering for the column.
        /// </summary>
        /// <param name="configAction">Configuration action</param>
        /// <returns>The column builder instance.</returns>
        public MvcCoreBootstrapTableColumnBuilder Filterable(Action<MvcCoreBootstrapTableFilteringBuilder> configAction)
        {
            configAction(_builderFactory.FilteringBuilder(_config.Filtering));
            return(this);
        }

        /// <summary>
        /// Sets a css class for the column header element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the column header element.</param>
        /// <returns>The column builder instance.</returns>
        public MvcCoreBootstrapTableColumnBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapTableColumnBuilder>(_config.CssClasses, cssClass, condition));
        }
    }
}
