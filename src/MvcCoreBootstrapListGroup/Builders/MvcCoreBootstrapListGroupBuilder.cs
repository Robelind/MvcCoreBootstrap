using System;
using MvcCoreBootstrapListGroup.Config;

namespace MvcCoreBootstrapListGroup.Builders
{
    public class MvcCoreBootstrapListGroupBuilder
    {
        private readonly ListGroupConfig _config;

        internal MvcCoreBootstrapListGroupBuilder(ListGroupConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the id attribute for the list group.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The list group builder instance.</returns>
        public MvcCoreBootstrapListGroupBuilder Id(string id)
        {
            _config.Id = id;
            return(this);
        }

        /// <summary>
        /// Sets the name attribute for list group.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The list group builder instance.</returns>
        public MvcCoreBootstrapListGroupBuilder Name(string name)
        {
            _config.Name = name;
            return(this);
        }

        /// <summary>
        /// Adds textual items to the list group.
        /// </summary>
        /// <param name="configAction">List group items configuration action.</param>
        /// <returns>The list group builder instance.</returns>
        public MvcCoreBootstrapListGroupBuilder Items(Action<MvcCoreBootstrapListGroupItemsBuilder> configAction)
        {
            if(configAction == null)
            {
                throw(new NullReferenceException(nameof(configAction)));
            }
            configAction(new MvcCoreBootstrapListGroupItemsBuilder(_config.Items));
            return(this);
        }

        /// <summary>
        /// Adds link items to the list group.
        /// </summary>
        /// <param name="configAction">List group items configuration action.</param>
        /// <returns>The list group builder instance.</returns>
        public MvcCoreBootstrapListGroupBuilder Links(Action<MvcCoreBootstrapListGroupLinksBuilder> configAction)
        {
            if(configAction == null)
            {
                throw(new NullReferenceException(nameof(configAction)));
            }
            configAction(new MvcCoreBootstrapListGroupLinksBuilder(_config));
            return(this);
        }

        /// <summary>
        /// Adds button items to the list group.
        /// </summary>
        /// <param name="configAction">List group items configuration action.</param>
        /// <returns>The list group builder instance.</returns>
        public MvcCoreBootstrapListGroupBuilder Buttons(Action<MvcCoreBootstrapListGroupButtonsBuilder> configAction)
        {
            if(configAction == null)
            {
                throw(new NullReferenceException(nameof(configAction)));
            }
            configAction(new MvcCoreBootstrapListGroupButtonsBuilder(_config));
            return(this);
        }

        /// <summary>
        /// Sets a css class for the list group element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the button element.</param>
        /// <returns>The list group builder instance.</returns>
        public MvcCoreBootstrapListGroupBuilder CssClass(string cssClass, bool condition = true)
        {
            if(condition)
            {
                _config.CssClasses.Add(cssClass);
            }
            return(this);
        }
    }
}