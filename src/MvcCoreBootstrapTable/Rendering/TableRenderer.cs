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

    internal class TableRenderer<T> : RenderBase, ITableRenderer where T : new()
    {
        private readonly TableState _tableState;
        private readonly object _entity;
        private readonly ITableNodeParser _nodeParser;
        private readonly TableModel<T> _model;
        private readonly ITableConfig _config;
        private string _containerId;

        public TableRenderer(TableModel<T> model, TableConfig config, TableState tableState,
            ITableNodeParser nodeParser)
        {
            _model = model;
            _config = config;
            _tableState = tableState;
            _entity = new T();
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
                this.AddCssClasses(rowConfig.CssClasses, row.Element);
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
                        this.AddCssClasses(cellConfig.CssClasses, cell.Element);
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
                TableNode filterRow = _config.Columns.Any(c => c.Value.Filtering.Threshold > 0 || c.Value.Filtering.Prepopulated)
                    ? this.CreateAndAppend("tr", header)
                    : null;
                IDictionary<string, IEnumerable<string>> filterValues = new Dictionary<string, IEnumerable<string>>();

                // For each column configured for prepopulated filtering, retrieve the possible
                // filter values.
                this.IterateProperties(_entity, (propInfo, config) =>
                {
                    if(config.Filtering.Prepopulated)
                    {
                        List<object> values = _model.Entities
                            .Select(ExpressionHelper.PropertyExpr<T>(propInfo.Name)).Distinct().ToList();
                        filterValues.Add(propInfo.Name, values.Select(v => v.ToString()).OrderBy(v => v));
                    }
                });
                
                // Columns.
                this.IterateProperties(_entity, (propInfo, config) =>
                {
                    TableNode headerCol = this.CreateAndAppend("th", headerRow);
                    TableNode filter = filterRow != null ? this.CreateAndAppend("td", filterRow) : null; 

                    if(config != null)
                    {
                        headerCol.Element.InnerHtml.Append(config.Header);
                        this.AddCssClasses(config.CssClasses, headerCol.Element);

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
                            this.ManualFiltering(config, propInfo, filter);
                        }
                        else if(config.Filtering.Prepopulated)
                        {
                            this.PrepopulatedFiltering(propInfo, filter, filterValues[propInfo.Name]);
                        }
                    }
                });
            }
        }

        private void ManualFiltering(ColumnConfig config, PropertyInfo propInfo, TableNode filter)
        {
            TableNode input = this.CreateAndAppend("input", filter);

            input.Element.Attributes.Add("type", "text");
            input.Element.Attributes.Add("data-filter-prop", propInfo.Name);
            input.Element.Attributes.Add("data-filter-threshold", config.Filtering.Threshold.ToString());
            input.Element.AddCssClass("form-control");
            this.AddCssClasses(config.Filtering.CssClasses, input.Element);
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

        private void PrepopulatedFiltering(PropertyInfo propInfo, TableNode filter,
            IEnumerable<string> filterValues)
        {
            TableNode dropDown = this.CreateAndAppend("div", filter);
            TagBuilder dropDownBtn = new TagBuilder("button");
            TagBuilder dropDownMenu = new TagBuilder("ul");
            TagBuilder dropDownCaret = new TagBuilder("span");

            dropDown.Element.AddCssClass("dropdown");
            dropDown.Element.InnerHtml.AppendHtml(dropDownBtn);
            dropDownBtn.AddCssClass("btn");
            dropDownBtn.AddCssClass("btn-default");
            dropDownBtn.AddCssClass("dropdown-toggle");
            dropDownBtn.Attributes.Add("type", "button");
            dropDownBtn.Attributes.Add("data-toggle", "dropdown");
            dropDownBtn.Attributes.Add("aria-haspopup", "true");
            dropDownBtn.Attributes.Add("aria-expanded", "false");
            if(_tableState.Filter.ContainsKey(propInfo.Name))
            {
                // Currently selected filter value.
                dropDownBtn.InnerHtml.Append(_tableState.Filter[propInfo.Name]);
            }
            dropDownBtn.InnerHtml.AppendHtml(dropDownCaret);
            dropDown.Element.InnerHtml.AppendHtml(dropDownMenu);
            dropDownMenu.AddCssClass("dropdown-menu");
            dropDownCaret.AddCssClass("caret");
            this.AddFilterSelection(dropDownMenu, propInfo.Name, null);
            foreach(var filterValue in filterValues)
            {
                this.AddFilterSelection(dropDownMenu, propInfo.Name, filterValue);
            }
        }

        private void AddFilterSelection(TagBuilder dropDownMenu, string propName, string filterValue)
        {
            TagBuilder valueContainer = new TagBuilder("li");
            TagBuilder value = new TagBuilder("a");

            dropDownMenu.InnerHtml.AppendHtml(valueContainer);
            valueContainer.InnerHtml.AppendHtml(value);
            value.AddCssClass("dropdown-item");
            value.Attributes.Add("href", "#");
            value.InnerHtml.Append(filterValue);
            this.SetupAjaxAttrs(value, $"&filter[]={propName}&filter[]={filterValue}", propName);
        }

        private TableNode Table()
        {
            TableNode table = new TableNode("table");

            this.AddAttribute("id", _config.Id, table.Element);
            this.AddAttribute("name", _config.Name, table.Element);
            table.Element.AddCssClass("table");
            table.Element.AddCssClass(_config.Striped ? "table-striped" : null);
            table.Element.AddCssClass(_config.Bordered ? "table-bordered" : null);
            table.Element.AddCssClass(_config.HoverState ? "table-hover" : null);
            table.Element.AddCssClass(_config.Condensed ? "table-condensed" : null);
            this.AddCssClasses(_config.CssClasses, table.Element);

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
                int entityCount = _model.ProcessedEntities.Count();
                int pageCount = _config.Paging.PageSize > 0
                    ? entityCount / _config.Paging.PageSize + (entityCount%_config.Paging.PageSize > 0 ? 1 : 0)
                    : 0;

                if(_config.Paging.PageInfo)
                {
                    TableNode container = this.FooterContainer("NavInfoContainer");
                    
                    this.CreateAndAppend("span", container).Element.InnerHtml.Append($"{_tableState.Page}/{pageCount}");
                    footerContent.Add(container);
                }

                // Paging.
                if(_config.Paging.PageSize > 0 && entityCount > _config.Paging.PageSize)
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
                    this.AddCssClasses(_config.Footer.CssClasses, content.Element);
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

       private void SetupAjaxAttrs(TagBuilder builder, string queryAttr = null, string ignore = null)
       {
            string url = queryAttr != null
                ? $"{_config.Update.Url}?{this.CommonQueryAttrs()}{this.FilterQueryAttrs(ignore)}{queryAttr}"
                : "";

            this.ConfigAjax(builder, _config.Update, url, true, _config.Id);
        }

        private string PagingLinkQueryAttrs(int pageNumber)
        {
            return($"{this.PagingQueryAttrs(pageNumber)}{this.SortQueryAttrs()}");
        }

        private string FilterQueryAttrs(string ignore = null)
        {
            return(_tableState.Filter.Aggregate("", (attrs, f) => f.Key != ignore
                ? attrs + $"&filter[]={f.Key}&filter[]={f.Value}"
                : attrs));
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
                this.AddCssClasses(cssClasses, node.Element);
            }

            return(node);
        }
    }
}