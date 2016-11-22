using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace MvcCoreBootstrapTable.Rendering
{
    internal class TableStateParser
    {
        public TableState Parse(HttpContext httpContext)
        {
            StringValues sort = httpContext.Request.Query["sort"];
            StringValues ascSort = httpContext.Request.Query["asc"];
            StringValues page = httpContext.Request.Query["page"];
            StringValues pageSize = httpContext.Request.Query["pageSize"];
            StringValues currentFilter = httpContext.Request.Query["currentFilter"];
            StringValues containerId = httpContext.Request.Query["containerId"];
            TableState tableState = new TableState
            {
                SortProp = sort.Count == 1 ? sort[0] : null,
                AscSort = ascSort.Count == 1 && bool.Parse(ascSort[0]),
                Page = page.Count == 1 ? int.Parse(page[0]) : 1,
                PageSize = pageSize.Count == 1 ? int.Parse(pageSize[0]) : 0,
                CurrentFilter = currentFilter.Count == 1 ? currentFilter[0] : null,
                ContainerId = containerId.Count == 1 ? containerId[0] : null,
                Filter = new Dictionary<string, string>(),
            };

            for(int i = 0; i < httpContext.Request.Query["filter[]"].Count; i += 2)
            {
                tableState.Filter.Add(httpContext.Request.Query["filter[]"][i],
                    httpContext.Request.Query["filter[]"][i + 1]);
            }

            return(tableState);
        }
    }
}
