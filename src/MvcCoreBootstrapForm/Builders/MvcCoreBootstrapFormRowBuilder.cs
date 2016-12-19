using Microsoft.AspNetCore.Html;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Builders
{
    public class MvcCoreBootstrapFormRowBuilder : BuilderBase
    {
        private readonly RowConfig _config;

        internal MvcCoreBootstrapFormRowBuilder(RowConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets a place holder text for the text input.
        /// </summary>
        /// <param name="placeHolder">Place holder text.</param>
        /// <returns>The form row builder instance.</returns>
        public MvcCoreBootstrapFormRowBuilder Column(ColumnWidth width, IHtmlContent content)
        {
            ColumnConfig column = new ColumnConfig {Width = width, Content = content};

            _config.Columns.Add(column);
            
            return(this);
        }
    }
}