using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCoreBootstrapTable.Builders;
using MvcCoreBootstrapTable.Config;
using MvcCoreBootstrapTable.Rendering;

namespace MvcCoreBootstrapTable
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders an Mvc Core Bootstrap Table.
        /// </summary>
        /// <typeparam name="T">Model entity type.</typeparam>
        /// <param name="htmlHelper">Html helper instance.</param>
        /// <param name="model">The model used to render the contents of the table.</param>
        /// <param name="configAction">Action that implements table configuration.</param>
        /// <returns>Table html markup.</returns>
        public static IHtmlContent MvcCoreBootstrapTable<T>(this IHtmlHelper htmlHelper, TableModel<T> model,
            Action<MvcCoreBootstrapTableBuilder<T>> configAction = null) where T: class, new()
        {
            if(model == null)
            {
                throw(new ArgumentNullException(nameof(model)));
            }

            TableState tableState = new TableStateParser().Parse(htmlHelper.ViewContext.HttpContext);
            TableConfig config = new TableConfig();
            TableConfigHandler configHandler = new TableConfigHandler();
            MvcCoreBootstrapTableBuilder<T> builder = new MvcCoreBootstrapTableBuilder<T>(model, new BuilderFactory(), config);

            configAction?.Invoke(builder);
            configHandler.Check(config, model.Entities);

            return(new TableRenderer<T>(model.Entities, config, tableState, new TableNodeParser()).Render());
        }
    }
}
