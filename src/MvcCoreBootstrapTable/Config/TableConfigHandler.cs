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
        }
    }
}
