using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MvcCoreBootstrapTable.Rendering
{
    public interface ITableUpdater
    {
        TableModel<T> Update<T>(IQueryable<T> entities) where T : new();
    }

    internal class TableUpdater : ITableUpdater
    {
        private readonly TableState _tableState;

        internal TableUpdater(TableState tableState)
        {
            _tableState = tableState;
        }

        public TableModel<T> Update<T>(IQueryable<T> entities) where T : new()
        {
            IQueryable<T> processedEntities = entities;

            // Filtering.
            foreach(var filter in _tableState.Filter)
            {
                processedEntities = processedEntities
                    .Where(ExpressionHelper.ComparisonExpr<T>(filter.Key, filter.Value));
            }

            // Sorting.
            if(!string.IsNullOrEmpty(_tableState.SortProp))
            {
                var lambda = ExpressionHelper.PropertyExpr<T>(_tableState.SortProp);

                processedEntities = _tableState.AscSort
                    ? processedEntities.OrderBy(lambda)
                    : processedEntities.OrderByDescending(lambda);
            }

            // Paging.
            if(_tableState.PageSize > 0)
            {
                processedEntities = processedEntities.Skip(_tableState.PageSize * (_tableState.Page - 1))
                    .Take(_tableState.PageSize);
            }

            return(new TableModel<T>(entities, processedEntities));
        }
    }
}
