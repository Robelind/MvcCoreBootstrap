using System.Diagnostics;
using System.Linq;

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

            Debug.WriteLine("Updating");
            // Filtering.
            foreach(var filter in _tableState.Filters)
            {
                Debug.WriteLine($"Filter: {filter.Key};{filter.Value}");
                processedEntities = processedEntities.Where(filter.Value.Prepopulated
                    ? ExpressionHelper.ComparisonExpr<T>(filter.Key, filter.Value.Value)
                    : ExpressionHelper.StartsWithExpr<T>(filter.Key, filter.Value.Value));
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
