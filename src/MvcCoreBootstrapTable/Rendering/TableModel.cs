using System.Collections.Generic;
using System.Linq;

namespace MvcCoreBootstrapTable.Rendering
{
    public class TableModel<T>
    {
        public TableModel(IQueryable<T> entities)
        {
            Entities = ProcessedEntities = entities;
        }

        internal TableModel(IQueryable<T> entities, IQueryable<T> processedEntities)
        {
            Entities = entities;
            ProcessedEntities = processedEntities;
            Processed = true;
        }

        internal IQueryable<T> Entities { get; }
        internal IQueryable<T> ProcessedEntities { get; }
        internal bool Processed { get; }
    }
}
