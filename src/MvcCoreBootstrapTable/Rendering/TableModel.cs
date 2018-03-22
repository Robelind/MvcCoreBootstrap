using System.Collections.Generic;
using System.Linq;

namespace MvcCoreBootstrapTable.Rendering
{
    public class TableModel<T>
    {
        public TableModel(IEnumerable<T> entities)
        {
            Entities = ProcessedEntities = entities;
            EntityCount = entities.Count();
        }

        internal TableModel(IEnumerable<T> entities, IEnumerable<T> processedEntities, int entityCount)
        {
            Entities = entities;
            ProcessedEntities = processedEntities;
            EntityCount = entityCount;
            Processed = true;
        }

        internal IEnumerable<T> Entities { get; }
        internal IEnumerable<T> ProcessedEntities { get; }
        internal int EntityCount { get; }
        internal bool Processed { get; }
    }
}
