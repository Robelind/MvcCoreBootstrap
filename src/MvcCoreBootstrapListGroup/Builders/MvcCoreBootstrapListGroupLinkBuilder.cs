using System;
using MvcCoreBootstrap;
using MvcCoreBootstrapListGroup.Config;

namespace MvcCoreBootstrapListGroup.Builders
{
    public class MvcCoreBootstrapListGroupLinkBuilder
    {
        private readonly ListGroupItem _item;

        internal MvcCoreBootstrapListGroupLinkBuilder(ListGroupItem item)
        {
            _item = item;
        }

        /// <summary>
        /// Sets the contextual state for the list group item.
        /// </summary>
        /// <param name="state">Item contextual state.</param>
        /// <param name="condition">Condition for applying contextual state.</param>
        /// <returns>The list group item builder instance.</returns>
        public MvcCoreBootstrapListGroupLinkBuilder Contextual(ContextualState state, bool condition = true)
        {
            if(condition)
            {
                _item.State = state;
            }
            return(this);
        }

        /// <summary>
        /// Sets the disabled state for the link item.
        /// </summary>
        /// <param name="disabled">If true, link is disabled.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapListGroupLinkBuilder Disabled(bool disabled = true)
        {
            _item.Disabled = disabled;
            return(this);
        }

        /// <summary>
        /// Adds a badge to the list group item.
        /// </summary>
        /// <param name="badge">Badge content.</param>
        /// <returns>The list group item builder instance.</returns>
        public MvcCoreBootstrapListGroupLinkBuilder Badge(string badge)
        {
            _item.Badge = badge;
            return(this);
        }

        /// <summary>
        /// Sets the panels content from an AJAX call.
        /// </summary>
        /// <param name="url">AJAX call url</param>
        /// <returns>The panel builder instance.</returns>
        public MvcCoreBootstrapListGroupLinkBuilder Ajax(string url, string updateId, Action<MvcCoreBootstrapListGroupAjaxBuilder> configAction = null)
        {
             _item.Ajax = new AjaxConfig {Url = url, UpdateId = updateId};
             configAction?.Invoke(new MvcCoreBootstrapListGroupAjaxBuilder(_item.Ajax));
             return(this);
        }
    }
}