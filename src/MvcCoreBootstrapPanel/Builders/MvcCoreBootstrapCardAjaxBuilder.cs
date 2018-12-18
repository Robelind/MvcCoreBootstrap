using MvcCoreBootstrapCard.Config;

namespace MvcCoreBootstrapCard.Builders
{
    public class MvcCoreBootstrapCardAjaxBuilder
    {
        private readonly AjaxConfig _config;

        internal MvcCoreBootstrapCardAjaxBuilder(AjaxConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the path to an icon etc to used to indicate AJAX activity.
        /// </summary>
        /// <param name="path">Path to indicator icon.</param>
        /// <param name="cssClass">CSS class to apply to the indicator element.</param>
        /// <returns>The ajax builder instance.</returns>
        public MvcCoreBootstrapCardAjaxBuilder IndicatorPath(string path, string cssClass = null)
        {
            _config.IndicatorPath = path;
            _config.IndicatorCss = cssClass;
            return(this);
        }

        /// <summary>
        /// Sets the content to display if the AJAX activity fails.
        /// </summary>
        /// <param name="content">Content.</param>
        /// <param name="cssClass">CSS class to apply to the error content element.</param>
        /// <returns>The ajax builder instance.</returns>
        public MvcCoreBootstrapCardAjaxBuilder ErrorContent(string content, string cssClass = null)
        {
            _config.ErrorContent = content;
            _config.ErrorContentCss = cssClass;
            return(this);
        }
    }
}
