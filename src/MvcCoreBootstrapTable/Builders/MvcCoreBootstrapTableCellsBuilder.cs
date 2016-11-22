using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MvcCoreBootstrapTable.Config;

namespace MvcCoreBootstrapTable.Builders
{
    public class MvcCoreBootstrapTableCellsBuilder
    {
        private readonly Dictionary<string, CellConfig> _configs;
        private readonly IBuilderFactory _builderFactory;

        internal MvcCoreBootstrapTableCellsBuilder(Dictionary<string, CellConfig> configs, IBuilderFactory builderFactory)
        {
            _configs = configs;
            _builderFactory = builderFactory;
        }

        /// <summary>
        /// Configures a table cell.
        /// </summary>
        /// <typeparam name="TVal"></typeparam>
        /// <param name="expression">Cell property expression</param>
        /// <returns>Cell builder</returns>
        public MvcCoreBootstrapTableCellBuilder Cell<TVal>(Expression<Func<TVal>> expression)
        {
            string cellProperty = ((MemberExpression)expression.Body).Member.Name;
            CellConfig cellConfig = new CellConfig();

            _configs.Add(cellProperty, cellConfig);

            return(_builderFactory.CellBuilder(cellConfig));
        }
    }
}
