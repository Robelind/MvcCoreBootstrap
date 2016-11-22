using MvcCoreBootstrap;
using MvcCoreBootstrapListGroup.Config;

namespace MvcCoreBootstrapListGroup.Builders
{
    public class MvcCoreBootstrapListGroupItemBuilder
    {
        private readonly ListGroupItem _item;

        internal MvcCoreBootstrapListGroupItemBuilder(ListGroupItem item)
        {
            _item = item;
        }

        /// <summary>
        /// Sets the contextual state for the list group item.
        /// </summary>
        /// <param name="state">Item contextual state.</param>
        /// <param name="condition">Condition for applying contextual state.</param>
        /// <returns>The list group item builder instance.</returns>
        public MvcCoreBootstrapListGroupItemBuilder Contextual(ContextualState state, bool condition = true)
        {
            if(condition)
            {
                _item.State = state;
            }
            return(this);
        }

        /// <summary>
        /// Adds a badge to the list group item.
        /// </summary>
        /// <param name="badge">Badge content.</param>
        /// <returns>The list group item builder instance.</returns>
        public MvcCoreBootstrapListGroupItemBuilder Badge(string badge)
        {
            _item.Badge = badge;
            return(this);
        }
    }
}