using System.Collections.Generic;
using System.Linq;

namespace MvcCoreBootstrapTable.Rendering
{
    public class TableModel<T>
    {
        public TableModel(IEnumerable<T> entities)
        {
            Entities = entities;
            EntityCount = entities.Count();
        }

        internal TableModel(IEnumerable<T> entities, int entityCount)
        {
            Entities = entities;
            EntityCount = entityCount;
        }

        internal IEnumerable<T> Entities { get; set; }
        internal int EntityCount { get; set; }
    }
}
