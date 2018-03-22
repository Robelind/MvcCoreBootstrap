using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using MvcCoreBootstrap;
using MvcCoreBootstrapListGroup.Builders;
using MvcCoreBootstrapListGroup.Config;
using MvcCoreBootstrapListGroup.Rendering;
using MvcCoreBootstrapPanel.Config;
using MvcCoreBootstrapTable.Builders;
using MvcCoreBootstrapTable.Config;
using MvcCoreBootstrapTable.Rendering;

namespace MvcCoreBootstrapPanel.Builders
{
    public class MvcCoreBootstrapPanelBuilder
    {
        private readonly PanelConfig _config;
        private readonly ITableConfigHandler _tableConfigHandler;
        private readonly HttpContext _httpContext;
        private ITableRenderer _tableRenderer;
        private TableConfig _tableConfig;
        private IEnumerable<object> _tableEntities;
        private ListGroupRenderer _listGroupRenderer;

        internal MvcCoreBootstrapPanelBuilder(PanelConfig config, ITableConfigHandler tableConfigHandler,
            HttpContext httpContext)
        {
            _config = config;
            _tableConfigHandler = tableConfigHandler;
            _httpContext = httpContext;
        }

        internal ITableRenderer TableRenderer => _tableRenderer;
        internal IListGroupRenderer ListGroupRenderer => _listGroupRenderer;

        /// <summary>
        /// Sets the id attribute for the panel.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The panel builder instance.</returns>
        public MvcCoreBootstrapPanelBuilder Id(string id)
        {
            _config.Id = id;
            return(this);
        }

        /// <summary>
        /// Sets the name attribute for the panel.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The panel builder instance.</returns>
        public MvcCoreBootstrapPanelBuilder Name(string name)
        {
            _config.Name = name;
            return(this);
        }

        /// <summary>
        /// Sets the heading for the panel.
        /// </summary>
        /// <param name="heading">Heading</param>
        /// <returns>The panel builder instance.</returns>
        public MvcCoreBootstrapPanelBuilder Heading(string heading)
        {
            _config.Heading = heading;
            return(this);
        }

        /// <summary>
        /// Sets the title for the panel.
        /// </summary>
        /// <param name="title">Title</param>
        /// <returns>The panel builder instance.</returns>
        public MvcCoreBootstrapPanelBuilder Title(string title)
        {
            _config.Title = title;
            return(this);
        }

        /// <summary>
        /// Sets the body content for the panel.
        /// </summary>
        /// <param name="body">Body</param>
        /// <returns>The panel builder instance.</returns>
        public MvcCoreBootstrapPanelBuilder Content(string body)
        {
            _config.Body = body;
            return(this);
        }

        /// <summary>
        /// Sets the footer for the panel.
        /// </summary>
        /// <param name="footer">Footer</param>
        /// <returns>The panel builder instance.</returns>
        public MvcCoreBootstrapPanelBuilder Footer(string footer)
        {
            _config.Footer = footer;
            return(this);
        }

        /// <summary>
        /// Sets the <see cref="ContextualState"/> of the panel.
        /// </summary>
        /// <param name="state">Contextual state</param>
        /// <param name="condition">If true, the contextual state will be applied.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapPanelBuilder Contextual(ContextualState state, bool condition = true)
        {
            _config.State = condition ? state : ContextualState.Default;
            return(this);
        }

        /// <summary>
        /// Sets a css class for the panel element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the button element.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapPanelBuilder CssClass(string cssClass, bool condition = true)
        {
            if(condition)
            {
                _config.CssClasses.Add(cssClass);
            }
            return(this);
        }

        /// <summary>
        /// Adds a table to the panel.
        /// </summary>
        /// <param name="configAction">Action to perform table configuration.</param>
        /// <returns>The panel builder instance.</returns>
        public MvcCoreBootstrapPanelBuilder Table<T>(TableModel<T> model, Action<MvcCoreBootstrapTableBuilder<T>> configAction) where T : new()
        {
            TableState tableState = new TableStateParser().Parse(_httpContext);

            _tableEntities = model.ProcessedEntities as IEnumerable<object>;
            _tableConfig = new TableConfig();
            _tableRenderer = new TableRenderer<T>(model, _tableConfig, tableState, new TableNodeParser());
            configAction(new MvcCoreBootstrapTableBuilder<T>(model, new BuilderFactory(), _tableConfig));

            _tableConfigHandler.Check(_tableConfig, _tableEntities);
            _tableConfig.ContainerId = _config.Id;

            return(this);
        }

        /// <summary>
        /// Adds a list group to the panel.
        /// </summary>
        /// <param name="configAction">Action to perform list group configuration.</param>
        /// <returns>The panel builder instance.</returns>
        public MvcCoreBootstrapPanelBuilder ListGroup(Action<MvcCoreBootstrapListGroupBuilder> configAction)
        {
            ListGroupConfig config = new ListGroupConfig();

            _listGroupRenderer = new ListGroupRenderer(config);
            configAction(new MvcCoreBootstrapListGroupBuilder(config));

            return (this);
        }

        /// <summary>
        /// Sets the panels content from an AJAX call.
        /// </summary>
        /// <param name="url">AJAX call url</param>
        /// <returns>The panel builder instance.</returns>
        public MvcCoreBootstrapPanelBuilder Ajax(string url, Action<MvcCoreBootstrapPanelAjaxBuilder> configAction = null)
         {
             _config.Ajax = new Config.AjaxConfig { Url = url};
             configAction?.Invoke(new MvcCoreBootstrapPanelAjaxBuilder(_config.Ajax));
             return(this);
        }

    }
}