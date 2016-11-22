using MvcCoreBootstrap;
using MvcCoreBootstrapListGroup.Config;

namespace MvcCoreBootstrapListGroup.Builders
{
    public class MvcCoreBootstrapListGroupButtonBuilder
    {
        private readonly ListGroupItem _item;

        internal MvcCoreBootstrapListGroupButtonBuilder(ListGroupItem item)
        {
            _item = item;
        }

        /// <summary>
        /// Sets the contextual state for the list group item.
        /// </summary>
        /// <param name="state">Item contextual state.</param>
        /// <param name="condition">Condition for applying contextual state.</param>
        /// <returns>The list group item builder instance.</returns>
        public MvcCoreBootstrapListGroupButtonBuilder Contextual(ContextualState state, bool condition = true)
        {
            if(condition)
            {
                _item.State = state;
            }
            return(this);
        }

        /// <summary>
        /// Sets a javascript click handler function for the button item.
        /// </summary>
        /// <param name="id">Id to pass as parameter to the click handler.</param>
        /// <param name="jsFunc">Javascript function to call when the button is clicked.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapListGroupButtonBuilder Click(string id, string jsFunc)
        {
            _item.Id = id;
            _item.ClickHandler = jsFunc;
            return(this);
        }

        /// <summary>
        /// Sets the disalbed state for the button item.
        /// </summary>
        /// <param name="disabled">If true, button is disabled.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapListGroupButtonBuilder Disabled(bool disabled = true)
        {
            _item.Disabled = disabled;
            return(this);
        }

        /// <summary>
        /// Adds a badge to the list group item.
        /// </summary>
        /// <param name="badge">Badge content.</param>
        /// <returns>The list group item builder instance.</returns>
        public MvcCoreBootstrapListGroupButtonBuilder Badge(string badge)
        {
            _item.Badge = badge;
            return(this);
        }
    }
}