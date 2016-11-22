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
            IEnumerable<T> modelEntities = entities;
            int entityCount;

            // Filtering.
            foreach(var filter in _tableState.Filter)
            {
                PropertyInfo propertyInfo = new T().GetType().GetProperties().Single(pi => pi.Name == filter.Key);
                Expression<Func<T, bool>> expr = arg => propertyInfo.GetValue(arg).ToString()
                    .ToLower().StartsWith(filter.Value.ToLower());
                
                modelEntities = modelEntities.Where(expr.Compile());
            }
            entityCount = modelEntities.Count();

            // Sorting.
            if(!string.IsNullOrEmpty(_tableState.SortProp))
            {
                var lambda = Lambda<T>(_tableState.SortProp);

                modelEntities = _tableState.AscSort
                    ? modelEntities.OrderBy(lambda)
                    : modelEntities.OrderByDescending(lambda);
            }

            // Paging.
            if(_tableState.PageSize > 0)
            {
                modelEntities = modelEntities.Skip(_tableState.PageSize * (_tableState.Page - 1))
                    .Take(_tableState.PageSize);
            }

            return(new TableModel<T>(modelEntities, entityCount));
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
