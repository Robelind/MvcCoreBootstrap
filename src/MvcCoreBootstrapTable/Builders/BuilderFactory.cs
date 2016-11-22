using System.Collections.Generic;
using MvcCoreBootstrapTable.Config;

namespace MvcCoreBootstrapTable.Builders
{
    internal interface IBuilderFactory
    {
        MvcCoreBootstrapTableFooterBuilder FooterBuilder(FooterConfig config);
        MvcCoreBootstrapTablePagingBuilder PagingBuilder(PagingConfig config);
        MvcCoreBootstrapTableUpdateBuilder UpdateBuilder(UpdateConfig config);
        MvcCoreBootstrapTableRowBuilder<T> RowBuilder<T>(T entity);
        MvcCoreBootstrapTableColumnsBuilder<T> ColumnsBuilder<T>(Dictionary<string, ColumnConfig> columnConfigs, SortingConfig sortConfig);
        MvcCoreBootstrapTableColumnBuilder ColumnBuilder(ColumnConfig config);
        MvcCoreBootstrapTableFilteringBuilder FilteringBuilder(FilteringConfig config);
        MvcCoreBootstrapTableCellBuilder CellBuilder(CellConfig config);
        MvcCoreBootstrapTableCellsBuilder CellsBuilder(Dictionary<string, CellConfig> configs);
    }

    internal class BuilderFactory : IBuilderFactory
    {
        public MvcCoreBootstrapTableFooterBuilder FooterBuilder(FooterConfig config)
        {
            return(new MvcCoreBootstrapTableFooterBuilder(config));
        }

        public MvcCoreBootstrapTablePagingBuilder PagingBuilder(PagingConfig config)
        {
            return(new MvcCoreBootstrapTablePagingBuilder(config));
        }

        public MvcCoreBootstrapTableUpdateBuilder UpdateBuilder(UpdateConfig config)
        {
            return(new MvcCoreBootstrapTableUpdateBuilder(config));
        }

        public MvcCoreBootstrapTableRowBuilder<T> RowBuilder<T>(T entity)
        {
            return(new MvcCoreBootstrapTableRowBuilder<T>(entity, this));
        }

        public MvcCoreBootstrapTableColumnsBuilder<T> ColumnsBuilder<T>(Dictionary<string, ColumnConfig> columnConfigs, SortingConfig sortConfig)
        {
            return(new MvcCoreBootstrapTableColumnsBuilder<T>(columnConfigs, sortConfig, this));
        }

        public MvcCoreBootstrapTableColumnBuilder ColumnBuilder(ColumnConfig config)
        {
            return(new MvcCoreBootstrapTableColumnBuilder(config, this));
        }

        public MvcCoreBootstrapTableFilteringBuilder FilteringBuilder(FilteringConfig config)
        {
            return(new MvcCoreBootstrapTableFilteringBuilder(config));
        }

        public MvcCoreBootstrapTableCellsBuilder CellsBuilder(Dictionary<string, CellConfig> configs)
        {
            return(new MvcCoreBootstrapTableCellsBuilder(configs, this));
        }

        public MvcCoreBootstrapTableCellBuilder CellBuilder(CellConfig config)
        {
            return(new MvcCoreBootstrapTableCellBuilder(config));
        }
    }
}
