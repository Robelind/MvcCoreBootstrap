using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MvcCoreBootstrapTable.Rendering
{
    public interface ITableUpdater
    {
        TableModel<T> Update<T>(IEnumerable<T> entities) where T : new();
    }

    internal class TableUpdater : ITableUpdater
    {
        private readonly TableState _tableState;

        internal TableUpdater(TableState tableState)
        {
            _tableState = tableState;
        }

        public TableModel<T> Update<T>(IEnumerable<T> entities) where T : new()
        {
            IEnumerable<T> processedEntities = entities;
            int entityCount;

            // Filtering.
            foreach(var filter in _tableState.Filter)
            {
                PropertyInfo propertyInfo = new T().GetType().GetProperties().Single(pi => pi.Name == filter.Key);
                Expression<Func<T, bool>> expr = arg => propertyInfo.GetValue(arg).ToString()
                    .ToLower().StartsWith(filter.Value.ToLower());
                
                processedEntities = processedEntities.Where(expr.Compile());
            }
            entityCount = processedEntities.Count();

            // Sorting.
            if(!string.IsNullOrEmpty(_tableState.SortProp))
            {
                var lambda = Lambda<T>(_tableState.SortProp);

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

            return(new TableModel<T>(entities, processedEntities, entityCount));
        }

        private Func<T, object> Lambda<T>(string propName) where T : new()
        {
            T e = new T();
            PropertyInfo p = e.GetType().GetProperties().Single(pi => pi.Name == propName);
            var parameter = Expression.Parameter(e.GetType());
            var property = Expression.Property(parameter, p);
            var conversion = Expression.Convert(property, typeof(object));

            return(Expression.Lambda<Func<T, object>>(conversion, parameter).Compile());
        }
    }
}
