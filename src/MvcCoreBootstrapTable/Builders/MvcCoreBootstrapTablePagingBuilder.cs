using System;
using MvcCoreBootstrapTable.Config;

namespace MvcCoreBootstrapTable.Builders
{
    public class MvcCoreBootstrapTablePagingBuilder
    {
        private readonly PagingConfig _config;

        internal MvcCoreBootstrapTablePagingBuilder(PagingConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the page size, i.e. the number of rows to display in the table.
        /// </summary>
        /// <param name="size">Page size.</param>
        /// <param name="condition">If true, paging will be activated.</param>
        /// <returns>Paging builder instance.</returns>
        public MvcCoreBootstrapTablePagingBuilder PageSize(int size, bool condition = true)
        {
            if(size <= 0)
            {
                throw(new ArgumentException("Page size must be larger than zero."));
            }
            _config.PageSize = condition ? size : 0;
            return(this);
        }

        /// <summary>
        /// Sets whether page information should be displayed in the table footer.
        /// </summary>
        /// <param name="pageInfo">If true, page information will be displayed in the table footer.</param>
        /// <returns>Paging builder instance.</returns>
        public MvcCoreBootstrapTablePagingBuilder PageInfo(bool pageInfo = true)
        {
            _config.PageInfo = pageInfo;
            return(this);
        }

        /// <summary>
        /// Sets whether there should be the possibility to select a
        /// page number to display in the table.
        /// </summary>
        /// <param name="access">If true, a page number can be selected.</param>
        /// <returns>Paging builder instance.</returns>
        public MvcCoreBootstrapTablePagingBuilder DirectPageAccess(bool access = true)
        {
            _config.DirectPageAccess = access;
            return(this);
        }

        /// <summary>
        /// Sets the icon library to be used for the paging navigation buttons.
        /// </summary>
        /// <param name="iconLib">Name of icon library or null if no icons should be used.</param>
        /// <returns>Paging builder instance.</returns>
        /// <remarks>
        /// Default is "glyphicon".
        /// </remarks>
        public MvcCoreBootstrapTablePagingBuilder IconLib(string iconLib = null)
        {
            _config.IconLib = iconLib;
            return(this);
        }

        /// <summary>
        /// Sets the css class for the button to navigate to first page.
        /// </summary>
        /// <param name="iconClass">Name of css class.</param>
        /// <returns>Paging builder instance.</returns>
        /// <remarks>
        /// Default is "glyphicon-fast-backward".
        /// </remarks>
        public MvcCoreBootstrapTablePagingBuilder First(string iconClass)
        {
            _config.FirstCssClass = iconClass;
            return(this);
        }

        /// <summary>
        /// Sets the css class for the button to navigate to previous page.
        /// </summary>
        /// <param name="iconClass">Name of css class.</param>
        /// <returns>Paging builder instance.</returns>
        /// <remarks>
        /// Default is "glyphicon-step-backward".
        /// </remarks>
        public MvcCoreBootstrapTablePagingBuilder Previous(string iconClass)
        {
            _config.PreviousCssClass = iconClass;
            return(this);
        }

        /// <summary>
        /// Sets the css class for the button to navigate to next page.
        /// </summary>
        /// <param name="iconClass">Name of css class.</param>
        /// <returns>Paging builder instance.</returns>
        /// <remarks>
        /// Default is "glyphicon-step-forward".
        /// </remarks>
        public MvcCoreBootstrapTablePagingBuilder Next(string iconClass)
        {
            _config.NextCssClass = iconClass;
            return(this);
        }

        /// <summary>
        /// Sets the css class for the button to navigate to last page.
        /// </summary>
        /// <param name="iconClass">Name of css class.</param>
        /// <returns>Paging builder instance.</returns>
        /// <remarks>
        /// Default is "glyphicon-fast-forward".
        /// </remarks>
        public MvcCoreBootstrapTablePagingBuilder Last(string iconClass)
        {
            _config.LastCssClass = iconClass;
            return(this);
        }
    }
}
