using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrap.Rendering;
using MvcCoreBootstrapTable.Builders;
using MvcCoreBootstrapTable.Config;

namespace MvcCoreBootstrapTable.Rendering
{
    internal interface ITableRenderer
    {
        IHtmlContent Render();
    }

    internal class TableRenderer : RenderBase, ITableRenderer
    {
        private readonly TableState _tableState;
        private readonly object _entity;
        private readonly ITableNodeParser _nodeParser;
        private readonly ITableConfig _config;
        private readonly int _entityCount;
        private string _containerId;

        public TableRenderer(TableConfig config, int entityCount, TableState tableState, object entity, ITableNodeParser nodeParser)
        {
            _config = config;
            _entityCount = entityCount;
            _tableState = tableState;
            _entity = entity;
            _nodeParser = nodeParser;
        }

        public IHtmlContent Render()
        {
            List<TableNode> nodes = new List<TableNode>();
            
            _containerId = _tableState.ContainerId;

            TableNode table = this.Table();
            TableNode innerContainer = this.Container(nodes, table);

            if(_tableState.ContainerId != null)
            {
                KeyValuePair<string, ColumnConfig> column = _config.Columns.FirstOrDefault(c =>
                    c.Value.SortState.HasValue && c.Value.SortState != SortState.None &&
                        c.Key != _tableState.SortProp);

                // Reset sort state for any column that was initially sorted.
                if(column.Value != null)
                {
                    column.Value.SortState = SortState.None;
                }
            }

            this.Caption(table);
            this.Header(table);
            this.Body(table);
            this.Footer(table);
            innerContainer.Element.InnerHtml.AppendHtml(string.Format(JsCode.Code, _containerId));
            this.FilteringLinkTemplate(innerContainer);
            
            return(_nodeParser.Parse(nodes));
        }

        private TableNode Container(List<TableNode> nodes, TableNode table)
        {
            TableNode innerContainer = _config.ContainerId == null ? new TableNode("div", table) : table;

            _containerId = _tableState.ContainerId;
            if(_containerId == null)
            {
                if(_config.ContainerId == null)
                {
                    TableNode container = new TableNode("div", innerContainer);

                    // First time the table is rendered, so render the surrounding
                    // container also.
                    _containerId = Guid.NewGuid().ToString();
                    container.Element.Attributes.Add("id", _containerId);
                    container.Element.AddCssClass("TableContainer");
                    nodes.Add(container);
                }
                else
                {
                    _containerId = _config.ContainerId;
                    nodes.Add(table);
                }
            }
            else
            {
                nodes.Add(_config.ContainerId == null ? innerContainer : table);
            }
            _config.Update.UpdateId = _containerId;

            return(innerContainer);
        }

        private void Body(TableNode table)
        {
            TableNode body = this.CreateAndAppend("tbody", table);

            // Rows.
            foreach(RowConfig rowConfig in _config.Rows)
            {
                TableNode row = this.CreateAndAppend("tr", body);

                this.AddContextualState(row.Element, rowConfig.State);
                this.AddCssClasses(row.Element, rowConfig.CssClasses);
                if(rowConfig.NavigationUrl != null)
                {
                    row.Element.Attributes.Add("style", "cursor: pointer");
                    row.Element.Attributes.Add("onclick", $"window.location.href = '{rowConfig.NavigationUrl}'");
                }
                else if(!string.IsNullOrEmpty(_config.RowClick) || !string.IsNullOrEmpty(rowConfig.RowClick))
                {
                    string jsCall = !string.IsNullOrEmpty(rowConfig.RowClick)
                        ? rowConfig.RowClick
                        : $"{_config.RowClick}(this)";

                    row.Element.Attributes.Add("style", "cursor: pointer");
                    row.Element.Attributes.Add("onclick", jsCall);
                }

                // Cells.
                this.IterateProperties(rowConfig.Entity, (property, _) =>
                {
                    CellConfig cellConfig = rowConfig.CellConfigs.ContainsKey(property.Name)
                        ? rowConfig.CellConfigs[property.Name]
                        : null;
                    TableNode cell = this.CreateAndAppend("td", row);
                    object cellValue = property.GetValue(rowConfig.Entity);

                    cell.Element.InnerHtml.Append(cellValue?.ToString() ?? string.Empty);
                    if(cellConfig != null)
                    {
                        this.AddContextualState(cell.Element, cellConfig.State);
                        this.AddCssClasses(cell.Element, cellConfig.CssClasses);
                    }
                });
            }
        }

        private void Header(TableNode table)
        {
            if(_config.Columns.Values.Any(c => !string.IsNullOrEmpty(c.Header)))
            {
                TableNode header = this.CreateAndAppend("thead", table);
                TableNode headerRow = this.CreateAndAppend("tr", header);
                TableNode filterRow = _config.Columns.Any(c => c.Value.Filtering.Threshold > 0)
                    ? this.CreateAndAppend("tr", header)
                    : null;

                // Columns.
                this.IterateProperties(_entity, (propInfo, config) =>
                {
                    TableNode headerCol = this.CreateAndAppend("th", headerRow);
                    TableNode filter = filterRow != null ? this.CreateAndAppend("td", filterRow) : null; 

                    if(config != null)
                    {
                        headerCol.Element.InnerHtml.Append(config.Header);
                        this.AddCssClasses(headerCol.Element, config.CssClasses);

                        // Sorting.
                        if(config.SortState.HasValue)
                        {
                            TableNode sortAsc = this.CreateAndAppend("a", headerCol, new []{"SortIcon"});
                            TableNode sortDesc = this.CreateAndAppend("a", headerCol, new []{"SortIcon"});
                            TableNode ascIcon = this.CreateAndAppend("span", sortAsc);
                            TableNode descIcon = this.CreateAndAppend("span", sortDesc);

                            ascIcon.Element.AddCssClass(_config.Sorting.IconLib);
                            ascIcon.Element.AddCssClass(_config.Sorting.AscendingCssClass);
                            descIcon.Element.AddCssClass(_config.Sorting.IconLib);
                            descIcon.Element.AddCssClass(_config.Sorting.DescendingCssClass);
                            this.SetupAjaxAttrs(sortAsc.Element,
                                $"&sort={propInfo.Name}&asc=True{this.PagingQueryAttrs()}");
                            this.SetupAjaxAttrs(sortDesc.Element,
                                $"&sort={propInfo.Name}&asc=False{this.PagingQueryAttrs()}");
                            if(propInfo.Name == _tableState.SortProp ||
                                (config.SortState != SortState.None && _tableState.SortProp == null))
                            {
                                bool ascending = propInfo.Name == _tableState.SortProp
                                    ? _tableState.AscSort
                                    : config.SortState == SortState.Ascending;

                                sortAsc.Element.AddCssClass(ascending ? "ActiveSort" : null);
                                sortDesc.Element.AddCssClass(!ascending ? "ActiveSort" : null);
                            }
                        }

                        // Filtering.
                        if(config.Filtering.Threshold > 0)
                        {
                            TableNode input = this.CreateAndAppend("input", filter);

                            input.Element.Attributes.Add("type", "text");
                            input.Element.Attributes.Add("data-filter-prop", propInfo.Name);
                            input.Element.Attributes.Add("data-filter-threshold", config.Filtering.Threshold.ToString());
                            input.Element.AddCssClass("form-control");
                            this.AddCssClasses(input.Element, config.Filtering.CssClasses);
                            if(_tableState.Filter.ContainsKey(propInfo.Name))
                            {
                                input.Element.Attributes.Add("value", _tableState.Filter[propInfo.Name]);
                            }
                            if(propInfo.Name == _tableState.CurrentFilter)
                            {
                                // Filter input should be focused.
                                input.Element.Attributes.Add("data-filter-focus", string.Empty);
                            }
                        }
                    }
                });
            }
        }

        private TableNode Table()
        {
            TableNode table = new TableNode("table");

            this.AddAttribute(table.Element, "id", _config.Id);
            this.AddAttribute(table.Element, "name", _config.Name);
            table.Element.AddCssClass("table");
            table.Element.AddCssClass(_config.Striped ? "table-striped" : null);
            table.Element.AddCssClass(_config.Bordered ? "table-bordered" : null);
            table.Element.AddCssClass(_config.HoverState ? "table-hover" : null);
            table.Element.AddCssClass(_config.Condensed ? "table-condensed" : null);
            this.AddCssClasses(table.Element, _config.CssClasses);

            return(table);
        }

        private void Caption(TableNode table)
        {
            if(!string.IsNullOrEmpty(_config.Caption))
            {
                this.CreateAndAppend("caption", table).Element.InnerHtml.Append(_config.Caption);
            }
        }

        private void Footer(TableNode table)
        {
            if(_config.Paging.PageSize > 0 || !string.IsNullOrEmpty(_config.Footer.Text))
            {
                List<TableNode> footerContent = new List<TableNode>();
                int pageCount = _config.Paging.PageSize > 0
                    ? _entityCount / _config.Paging.PageSize + (_entityCount%_config.Paging.PageSize > 0 ? 1 : 0)
                    : 0;

                if(_config.Paging.PageInfo)
                {
                    TableNode container = this.FooterContainer("NavInfoContainer");
                    
                    this.CreateAndAppend("span", container).Element.InnerHtml.Append($"{_tableState.Page}/{pageCount}");
                    footerContent.Add(container);
                }

                // Paging.
                if(_config.Paging.PageSize > 0 && _entityCount > _config.Paging.PageSize)
                {
                    TableNode firstContainer = this.FooterContainer("NavBtnContainer");
                    TableNode prevContainer = this.FooterContainer("NavBtnContainer");
                    TableNode nextContainer = this.FooterContainer("NavBtnContainer");
                    TableNode lastContainer = this.FooterContainer("NavBtnContainer");
                    TableNode first = this.NavCtrl(firstContainer, _config.Paging.IconLib, _config.Paging.FirstCssClass, "NavFirst", _tableState.Page > 1);
                    TableNode prev = this.NavCtrl(prevContainer, _config.Paging.IconLib, _config.Paging.PreviousCssClass, "NavPrevious", _tableState.Page > 1);
                    TableNode next = this.NavCtrl(nextContainer, _config.Paging.IconLib, _config.Paging.NextCssClass, "NavNext", pageCount > _tableState.Page);
                    TableNode last = this.NavCtrl(lastContainer, _config.Paging.IconLib, _config.Paging.LastCssClass, "NavLast", pageCount > _tableState.Page);

                    this.SetupAjaxAttrs(first.Element, this.PagingLinkQueryAttrs(1));
                    this.SetupAjaxAttrs(prev.Element, this.PagingLinkQueryAttrs(_tableState.Page - 1));
                    this.SetupAjaxAttrs(next.Element, this.PagingLinkQueryAttrs(_tableState.Page + 1));
                    this.SetupAjaxAttrs(last.Element, this.PagingLinkQueryAttrs(pageCount));

                    if(_config.Paging.DirectPageAccess)
                    {
                        TableNode container = this.FooterContainer("NavAccessContainer");
                        TableNode pages = this.CreateAndAppend("select", container);
                        TableNode pageSelector = this.CreateAndAppend("a", container);
                        string pageSelectorId = Guid.NewGuid().ToString();

                        pages.Element.Attributes.Add("data-pageselector-id", $"#{pageSelectorId}");
                        for(int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                        {
                            TableNode page = this.CreateAndAppend("option", pages);

                            page.Element.InnerHtml.Append(pageNumber.ToString());
                            page.Element.Attributes.Add("value",
                                $"{_config.Update.Url}?{this.CommonQueryAttrs()}{this.FilterQueryAttrs()}{this.PagingLinkQueryAttrs(pageNumber)}");
                            if(pageNumber == _tableState.Page)
                            {
                                page.Element.Attributes.Add("selected", "");
                            }
                        }

                        pageSelector.Element.Attributes.Add("id", pageSelectorId);
                        this.SetupAjaxAttrs(pageSelector.Element);
                        footerContent.Add(container);
                    }

                    footerContent.Add(lastContainer);
                    footerContent.Add(nextContainer);
                    footerContent.Add(prevContainer);
                    footerContent.Add(firstContainer);
                }

                // Text.
                if(!string.IsNullOrEmpty(_config.Footer.Text))
                {
                    TableNode container = new TableNode("div");
                    TableNode footerText = this.CreateAndAppend("span", container);

                    container.Element.AddCssClass("FooterTextContainer");
                    footerText.Element.AddCssClass("FooterText");
                    footerText.Element.InnerHtml.Append(_config.Footer.Text);
                    footerContent.Add(container);
                }

                if(footerContent.Any())
                {
                    TableNode footer = this.CreateAndAppend("tfoot", table);
                    TableNode row = this.CreateAndAppend("tr", footer);
                    TableNode content = this.CreateAndAppend("td", row);
                    int colCount = _entity.GetType().GetProperties()
                        .Count(pi => !_config.Columns.ContainsKey(pi.Name) || _config.Columns[pi.Name].Visible);

                    content.Element.Attributes.Add("colspan", colCount.ToString());
                    this.AddContextualState(content.Element, _config.Footer.State);
                    this.AddCssClasses(content.Element, _config.Footer.CssClasses);
                    content.InnerContent.AddRange(footerContent);
                }
            }
        }

        private TableNode FooterContainer(string cssClass)
        {
            TableNode container = new TableNode("div");

            container.Element.AddCssClass("pull-right");
            container.Element.AddCssClass(cssClass);

            return(container);
        }

        private TableNode NavCtrl(TableNode container, string iconLib, string icon, string navClass, bool enabled = true)
        {
            TableNode nav = this.CreateAndAppend("a", container, new [] {"btn", "btn-default", navClass});

            if(!enabled)
            {
                nav.Element.Attributes.Add("disabled", "disabled");
            }

            if(iconLib != null)
            {
                this.CreateAndAppend("span", nav, new [] {iconLib, icon});
            }

            return(nav);
        }

        private void FilteringLinkTemplate(TableNode container)
        {
            if(_config.Columns.Any(c => c.Value.Filtering.Threshold > 0))
            {
                TableNode filterLink = this.CreateAndAppend("a", container);
                TableNode linkTemplate = this.CreateAndAppend("input", container);
                string url = $"{_config.Update.Url}?{this.CommonQueryAttrs()}{this.SortQueryAttrs()}{this.PagingQueryAttrs()}";

                this.SetupAjaxAttrs(filterLink.Element);
                filterLink.Element.Attributes.Add("id", "FilterLink");
                linkTemplate.Element.Attributes.Add("id", "FilterLinkTemplate");
                linkTemplate.Element.Attributes.Add("type", "hidden");
                linkTemplate.Element.Attributes.Add("value", url);
            }
        }

       private void SetupAjaxAttrs(TagBuilder builder, string queryAttr = null)
        {
            string url = queryAttr != null
                ? $"{_config.Update.Url}?{this.CommonQueryAttrs()}{this.FilterQueryAttrs()}{queryAttr}"
                : "";

            this.ConfigAjax(builder, _config.Update, url, true, _config.Id);
            //builder.Attributes.Add("data-ajax", "true");
            //builder.Attributes.Add("data-ajax-update", $"#{_containerId}");
            //builder.Attributes.Add("data-ajax-mode", "replace");
            //builder.Attributes.Add("data-ajax-url", url);
            //builder.Attributes.Add("data-ajax-loading", "#" + _config.Update.BusyIndicatorId);
            //builder.Attributes.Add("data-ajax-begin", $"{_config.Update.Start}");
            //builder.Attributes.Add("data-ajax-success", $"{_config.Update.Success}");
            //builder.Attributes.Add("data-ajax-failure", $"{_config.Update.Error}");
            //builder.Attributes.Add("data-ajax-complete", $"{_config.Update.Complete}");
            //builder.Attributes.Add("data-ajax-loading-duration", "100");
        }

        private string PagingLinkQueryAttrs(int pageNumber)
        {
            return($"{this.PagingQueryAttrs(pageNumber)}{this.SortQueryAttrs()}");
        }

        private string FilterQueryAttrs()
        {
            return(_tableState.Filter.Aggregate("", (attrs, f) => attrs + $"&filter[]={f.Key}&filter[]={f.Value}"));
        }

        private string SortQueryAttrs()
        {
            return($"&sort={_tableState.SortProp}&asc={_tableState.AscSort}");
        }

        private string PagingQueryAttrs(int pageNumber = 0)
        {
            int pageNum = pageNumber != 0 ? pageNumber : _tableState.Page;
            return($"&page={pageNum}&pageSize={_config.Paging.PageSize}");
        }

        private string CommonQueryAttrs()
        {
            string separator = _config.Update.CustomQueryPars.Any() ? "&" : "";
            string customAttrs = separator + string.Join("&", _config.Update.CustomQueryPars.Select(qp =>
                $"{qp.Key}={qp.Value}").ToList());

            return($"containerId={_containerId}" + customAttrs);
        }

        private void IterateProperties(object entity, Action<PropertyInfo, ColumnConfig> action)
        {
            foreach(var property in entity.GetType().GetProperties())
            {
                ColumnConfig columnConfig = _config.Columns.ContainsKey(property.Name)
                    ? _config.Columns[property.Name]
                    : null;

                if(columnConfig == null || columnConfig.Visible)
                {
                    action(property, columnConfig);
                }
            }
        }

        private TableNode CreateAndAppend(string tag, TableNode parent, IEnumerable<string> cssClasses = null)
        {
            TableNode node = new TableNode(tag);

            parent.InnerContent.Add(node);
            if(cssClasses != null)
            {
                this.AddCssClasses(node.Element, cssClasses);
            }

            return(node);
        }
    }
}