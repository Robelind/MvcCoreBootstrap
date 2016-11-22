using System;
using MvcCoreBootstrapListGroup.Config;

namespace MvcCoreBootstrapListGroup.Builders
{
    public class MvcCoreBootstrapListGroupLinksBuilder
    {
        private readonly ListGroupConfig _config;

        internal MvcCoreBootstrapListGroupLinksBuilder(ListGroupConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Adds an link item to the list group.
        /// </summary>
        /// <param name="content">List group item text.</param>
        /// <param name="url">Target url.</param>
        /// <param name="configAction">List group link configuration action.</param>
        /// <returns>The links builder instance.</returns>
        public MvcCoreBootstrapListGroupLinksBuilder Item(string content, string url, Action<MvcCoreBootstrapListGroupLinkBuilder> configAction = null)
        {
            ListGroupItem item = new ListGroupItem {Content = content, Url = url};

            _config.Items.Add(item);
            configAction?.Invoke(new MvcCoreBootstrapListGroupLinkBuilder(item));

            return(this);
        }

        /// <summary>
        /// Adds an link item to the list group.
        /// </summary>
        /// <param name="content">List group item text.</param>
        /// <param name="url">Target url.</param>
        /// <param name="configAction">List group link configuration action.</param>
        /// <returns>The links builder instance.</returns>
        public MvcCoreBootstrapListGroupLinksBuilder Item(string content, Action<MvcCoreBootstrapListGroupLinkBuilder> configAction = null)
        {
            ListGroupItem item = new ListGroupItem {Content = content};

            _config.Items.Add(item);
            configAction?.Invoke(new MvcCoreBootstrapListGroupLinkBuilder(item));

            return(this);
        }

         /// <summary>
        /// Indicates the last clicked link as 'active'.
        /// </summary>
        /// <returns>The links builder instance.</returns>
        public MvcCoreBootstrapListGroupLinksBuilder TrackActive()
        {
            _config.TrackActive = true;
            return(this);
        }
    }
}