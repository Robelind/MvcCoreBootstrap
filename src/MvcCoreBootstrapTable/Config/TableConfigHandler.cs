using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcCoreBootstrapTable.Config
{
    internal interface ITableConfigHandler
    {
        void Check(ITableConfig config, IEnumerable<object> tableEntities);
    }

    internal class TableConfigHandler : ITableConfigHandler
    {
        public void Check(ITableConfig config, IEnumerable<object> tableEntities)
        {
            if((config.Paging.PageSize > 0 || config.Columns.Any(c => c.Value.SortState.HasValue)
               || config.Columns.Any(c => c.Value.Filtering.Threshold > 0)) &&
               string.IsNullOrEmpty(config.Update.Url))
            {
                throw(new Exception("Update url must be configured if using paging, sorting or filtering."));
            }

            if(!config.Rows.Any())
            {
                 IEnumerable<object> entities = config.Paging.PageSize > 0
                    ? tableEntities.Take(config.Paging.PageSize)
                    : tableEntities;

                // No row configuration has been performed.
                // Create row configs from the entities.
                foreach(var row in entities.Select(e => new RowConfig(e)))
                {
                    config.Rows.Add(row);
                }
            }

            if(config.Paging.PageSize > 0 && config.Rows.Count > config.Paging.PageSize)
            {
                IEnumerable<RowConfig> rows = config.Rows.Take(config.Paging.PageSize).ToList();
                
                // Apply paging.
                config.Rows.Clear();
                foreach(var row in rows)
                {
                    config.Rows.Add(row);
                }
            }
        }
    }
}
