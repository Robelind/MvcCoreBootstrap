using System;
using MvcCoreBootstrapListGroup.Config;

namespace MvcCoreBootstrapListGroup.Builders
{
    public class MvcCoreBootstrapListGroupButtonsBuilder
    {
        private readonly ListGroupConfig _config;

        internal MvcCoreBootstrapListGroupButtonsBuilder(ListGroupConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Adds an button item to the list group.
        /// </summary>
        /// <param name="content">Button item content.</param>
        /// <param name="configAction">List group button configuration action.</param>
        /// <returns>The buttons builder instance.</returns>
        public MvcCoreBootstrapListGroupButtonsBuilder Item(string content, Action<MvcCoreBootstrapListGroupButtonBuilder> configAction = null)
        {
            ListGroupItem item = new ListGroupItem {Content = content, Button = true};

            _config.Items.Add(item);
            configAction?.Invoke(new MvcCoreBootstrapListGroupButtonBuilder(item));

            return(this);
        }

        /// <summary>
        /// Indicates the last clicked button as 'active'.
        /// </summary>
        /// <returns>The buttons builder instance.</returns>
        public MvcCoreBootstrapListGroupButtonsBuilder TrackActive()
        {
            _config.TrackActive = true;
            return(this);
        }
    }
}