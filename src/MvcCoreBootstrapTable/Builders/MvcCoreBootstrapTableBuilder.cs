﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MvcCoreBootstrap;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapTable.Config;
using MvcCoreBootstrapTable.Rendering;

namespace MvcCoreBootstrapTable.Builders
{
    public class MvcCoreBootstrapTableBuilder<T> : BuilderBase where T : new()
    {
        private readonly TableModel<T> _model;
        private readonly IBuilderFactory _builderFactory;
        private readonly ITableConfig _config;

        internal MvcCoreBootstrapTableBuilder(TableModel<T> model, IBuilderFactory builderFactory, ITableConfig config)
        {
            _model = model;
            _builderFactory = builderFactory;
            _config = config;
        }

        /// <summary>
        /// Sets the id attribute for the table.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> Id(string id)
        {
            _config.Id = id;
            return(this);
        }

        /// <summary>
        /// Sets the name attribute for the table.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> Name(string name)
        {
            _config.Name = name;
            return(this);
        }

        /// <summary>
        /// Sets the name caption for the table.
        /// </summary>
        /// <param name="caption">Caption</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> Caption(string caption)
        {
            _config.Caption = caption;
            return(this);
        }

        /// <summary>
        /// Sets whether the table should be rendered in a striped fashion.
        /// </summary>
        /// <param name="striped">If true, the table is rendered in a striped fashion.</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> Striped(bool striped = true)
        {
            _config.Striped = striped;
            return(this);
        }

        /// <summary>
        /// Sets whether the table should be rendered bordered.
        /// </summary>
        /// <param name="bordered">If true, the table is rendered bordered.</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> Bordered(bool bordered = true)
        {
            _config.Bordered = bordered;
            return(this);
        }

        /// <summary>
        /// Sets whether the table rows should have a hover state.
        /// </summary>
        /// <param name="hoverState">If true, the table rows will have a hover state.</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> HoverState(bool hoverState = true)
        {
            _config.HoverState = hoverState;
            return(this);
        }

        /// <summary>
        /// Sets whether the table should be rendered in a small fashion.
        /// </summary>
        /// <param name="small">If true, the table is rendered in a small fashion.</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> Small(bool small = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTableBuilder<T>>(() => _config.Small = small));
        }

        /// <summary>
        /// Sets whether the table should be rendered in a dark fashion.
        /// </summary>
        /// <param name="dark">If true, the table is rendered in a dark fashion.</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> Dark(bool dark = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTableBuilder<T>>(() => _config.Dark = dark));
        }

        /// <summary>
        /// Sets the <see cref="ContextualState"/> of the table.
        /// </summary>
        /// <param name="state">Contextual state</param>
        /// <param name="condition">If true, the contextual state will be applied.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> Contextual(ContextualState state, bool condition = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTableBuilder<T>>(() => _config.State = condition ? state : ContextualState.Default));
        }

        /// <summary>
        /// Sets a css class for the table element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the table element.</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapTableBuilder<T>>(_config.CssClasses, cssClass, condition));
        }

        /// <summary>
        /// Configures a java script function to be called when a row in
        /// the table is clicked.
        /// </summary>
        /// <param name="jsFunc">Name of java script function.</param>
        /// <param name="condition">If true, the java script function will be called.</param>
        /// <returns>The table builder instance.</returns>
        /// <remarks>
        /// The java script function will be passed the row instance as a parameter.
        /// </remarks>
        public MvcCoreBootstrapTableBuilder<T> RowClick(string jsFunc, bool condition = true)
        {
            if(condition)
            {
                _config.RowClick = jsFunc;
            }
            return(this);
        }

        /// <summary>
        /// Configures the table footer.
        /// </summary>
        /// <param name="configAction">Configuration action</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> Footer(Action<MvcCoreBootstrapTableFooterBuilder> configAction)
        {
            configAction(_builderFactory.FooterBuilder(_config.Footer));
            return(this);
        }

        /// <summary>
        /// Configured paging.
        /// </summary>
        /// <param name="configAction">Configuration action</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> Paging(Action<MvcCoreBootstrapTablePagingBuilder> configAction)
        {
            configAction(_builderFactory.PagingBuilder(_config.Paging));
            return(this);
        }

        /// <summary>
        /// Configures table updating.
        /// </summary>
        /// <param name="configAction">Configuration action</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> Update(Action<MvcCoreBootstrapTableUpdateBuilder> configAction)
        {
            configAction(_builderFactory.UpdateBuilder(_config.Update));
            if(_config.Update.Url == null)
            {
                throw new ArgumentNullException("Update url");
            }
            
            return(this);
        }

        /// <summary>
        /// Configures the rows of the table.
        /// </summary>
        /// <param name="configAction">Configuration action</param>
        /// <returns>The table builder instance.</returns>
        /// <remarks>
        /// If using paging or initial filtering, configure it before doing row configuration.
        /// </remarks>
        public MvcCoreBootstrapTableBuilder<T> Rows(Action<MvcCoreBootstrapTableRowBuilder<T>, T> configAction)
        {
            IQueryable<T> entities = _model.ProcessedEntities;

            if(!_model.Processed)
            {
                KeyValuePair<string, ColumnConfig> initialFilterColumn = _config.Columns
                    .FirstOrDefault(c => c.Value.Filtering.Initial != null);
                KeyValuePair<string, ColumnConfig> initialSortColumn = _config.Columns
                    .FirstOrDefault(c => c.Value.SortState.HasValue);

                // Initial rendering of the table, apply initial filteringm sorting and paging.
                if(initialFilterColumn.Key != null)
                {
                    Expression<Func<T, bool>> whereExpr = ExpressionHelper.EqualsExpr<T>(initialFilterColumn.Key,
                        initialFilterColumn.Value.Filtering.Initial);

                    entities = entities.Where(whereExpr);
                }

                entities = _config.Paging.PageSize > 0
                    ? entities.Take(_config.Paging.PageSize)
                    : entities;

                if(initialSortColumn.Key != null)
                {
                    var sortExpr = ExpressionHelper.PropertyExpr<T>(initialSortColumn.Key);

                    entities = initialSortColumn.Value.SortState == SortState.Ascending
                        ? entities.OrderBy(sortExpr)
                        : entities.OrderByDescending(sortExpr);
                }
            }

                foreach(T entity in entities)
            {
                MvcCoreBootstrapTableRowBuilder<T> builder = _builderFactory.RowBuilder(entity);

                configAction(builder, entity);
                _config.Rows.Add(builder.Config);
            }

            return(this);
        }

        /// <summary>
        /// Configures the columns of the table.
        /// </summary>
        /// <param name="configAction">Configuration action</param>
        /// <returns>The table builder instance.</returns>
        public MvcCoreBootstrapTableBuilder<T> Columns(Action<MvcCoreBootstrapTableColumnsBuilder<T>> configAction)
        {
            configAction(_builderFactory.ColumnsBuilder<T>(_config.Columns, _config.Sorting));
            return(this);
        }
    }
}