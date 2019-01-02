using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using MvcCoreBootstrap;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapCard.Config;
using MvcCoreBootstrapListGroup.Builders;
using MvcCoreBootstrapListGroup.Config;
using MvcCoreBootstrapListGroup.Rendering;
using MvcCoreBootstrapTable.Builders;
using MvcCoreBootstrapTable.Config;
using MvcCoreBootstrapTable.Rendering;

namespace MvcCoreBootstrapCard.Builders
{
    public class MvcCoreBootstrapCardBuilder : BuilderBase
    {
        private readonly CardConfig _config;
        private readonly ITableConfigHandler _tableConfigHandler;
        private readonly HttpContext _httpContext;
        private ITableRenderer _tableRenderer;
        private TableConfig _tableConfig;
        private IEnumerable<object> _tableEntities;
        private ListGroupRenderer _listGroupRenderer;

        internal MvcCoreBootstrapCardBuilder(CardConfig config, ITableConfigHandler tableConfigHandler, HttpContext httpContext)
        {
            _config = config;
            _tableConfigHandler = tableConfigHandler;
            _httpContext = httpContext;
        }

        internal ITableRenderer TableRenderer => _tableRenderer;
        internal IListGroupRenderer ListGroupRenderer => _listGroupRenderer;

        /// <summary>
        /// Sets the id attribute for the card.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The card builder instance.</returns>
        public MvcCoreBootstrapCardBuilder Id(string id)
        {
            _config.Id = id;
            return(this);
        }

        /// <summary>
        /// Sets the name attribute for the card.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The card builder instance.</returns>
        public MvcCoreBootstrapCardBuilder Name(string name)
        {
            _config.Name = name;
            return(this);
        }

        /// <summary>
        /// Sets a header for the card.
        /// </summary>
        /// <param name="header">Header text</param>
        /// <returns>The card builder instance.</returns>
        public MvcCoreBootstrapCardBuilder Header(string header, bool collapse = false, bool initiallyVisible = true)
        {
            _config.Header = header;
            _config.Collapse = collapse;
            _config.InitiallyVisible = initiallyVisible;
            return(this);
        }

        /// <summary>
        /// Sets the title for the card.
        /// </summary>
        /// <param name="title">Title</param>
        /// <returns>The card builder instance.</returns>
        public MvcCoreBootstrapCardBuilder Title(string title)
        {
            _config.Title = title;
            return(this);
        }

        /// <summary>
        /// Sets the subtitle for the card.
        /// </summary>
        /// <param name="subTitle">Subtitle</param>
        /// <returns>The card builder instance.</returns>
        public MvcCoreBootstrapCardBuilder SubTitle(string subTitle)
        {
            _config.SubTitle = subTitle;
            return(this);
        }

        /// <summary>
        /// Sets the content of the card.
        /// </summary>
        /// <param name="content">Content</param>
        /// <returns>The card builder instance.</returns>
        public MvcCoreBootstrapCardBuilder Content(string content)
        {
            return(this.SetConfigProp<MvcCoreBootstrapCardBuilder>(() => _config.Content = content));
        }

        /// <summary>
        /// Sets the content of the card.
        /// </summary>
        /// <param name="content">Content</param>
        /// <returns>The card builder instance.</returns>
        public MvcCoreBootstrapCardBuilder Content(IHtmlContent content)
        {
            return(this.SetConfigProp<MvcCoreBootstrapCardBuilder>(() => _config.HtmlContent = content));
        }

        /// <summary>
        /// Sets the footer for the card.
        /// </summary>
        /// <param name="footer">Footer</param>
        /// <returns>The card builder instance.</returns>
        public MvcCoreBootstrapCardBuilder Footer(string footer)
        {
            _config.Footer = footer;
            return(this);
        }

        /// <summary>
        /// Sets the <see cref="ContextualState"/> of the card.
        /// </summary>
        /// <param name="state">Contextual state</param>
        /// <param name="condition">If true, the contextual state will be applied.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapCardBuilder Contextual(ContextualState state, bool condition = true)
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
        public MvcCoreBootstrapCardBuilder CssClass(string cssClass, bool condition = true)
        {
            if(condition)
            {
                _config.CssClasses.Add(cssClass);
            }
            return(this);
        }

        /// <summary>
        /// Adds a table to the card.
        /// </summary>
        /// <param name="configAction">Action to perform table configuration.</param>
        /// <returns>The card builder instance.</returns>
        public MvcCoreBootstrapCardBuilder Table<T>(TableModel<T> model, Action<MvcCoreBootstrapTableBuilder<T>> configAction) where T : new()
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
        /// Adds a list group to the card.
        /// </summary>
        /// <param name="configAction">Action to perform list group configuration.</param>
        /// <returns>The card builder instance.</returns>
        public MvcCoreBootstrapCardBuilder ListGroup(Action<MvcCoreBootstrapListGroupBuilder> configAction)
        {
            ListGroupConfig config = new ListGroupConfig();

            _listGroupRenderer = new ListGroupRenderer(config);
            configAction(new MvcCoreBootstrapListGroupBuilder(config));

            return(this);
        }

        /// <summary>
        /// Sets the panels content from an AJAX call.
        /// </summary>
        /// <param name="url">AJAX call url</param>
        /// <param name="configAction">Action to perform AJAX configuration.</param>
        /// <returns>The card builder instance.</returns>
        public MvcCoreBootstrapCardBuilder Ajax(string url, Action<MvcCoreBootstrapCardAjaxBuilder> configAction = null)
         {
             _config.Ajax = new Config.AjaxConfig { Url = url};
             configAction?.Invoke(new MvcCoreBootstrapCardAjaxBuilder(_config.Ajax));
             return(this);
        }

    }
}