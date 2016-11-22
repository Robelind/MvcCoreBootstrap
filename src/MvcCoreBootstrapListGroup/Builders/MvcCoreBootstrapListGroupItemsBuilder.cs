using System;
using System.Collections.Generic;
using MvcCoreBootstrapListGroup.Config;

namespace MvcCoreBootstrapListGroup.Builders
{
    public class MvcCoreBootstrapListGroupItemsBuilder
    {
        private readonly IList<ListGroupItem> _items;

        internal MvcCoreBootstrapListGroupItemsBuilder(IList<ListGroupItem> items)
        {
            _items = items;
        }

        /// <summary>
        /// Adds an item to the list group.
        /// </summary>
        /// <param name="content">List group item content.</param>
        /// <param name="configAction">List group item configuration action.</param>
        /// <returns>The list group builder instance.</returns>
        public MvcCoreBootstrapListGroupItemsBuilder Item(string content, Action<MvcCoreBootstrapListGroupItemBuilder> configAction = null)
        {
            ListGroupItem item = new ListGroupItem {Content = content};

            _items.Add(item);
            configAction?.Invoke(new MvcCoreBootstrapListGroupItemBuilder(item));

            return(this);
        }

        /// <summary>
        /// Adds items to the list group.
        /// </summary>
        /// <param name="items">List group items.</param>
        /// <returns>The list group builder instance.</returns>
        public MvcCoreBootstrapListGroupItemsBuilder Items(IEnumerable<string> items)
        {
            foreach(string item in items)
            {
                _items.Add(new ListGroupItem {Content = item});
            }            
            
            return(this);
        }
    }
}