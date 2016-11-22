using MvcCoreBootstrap.Building;
using MvcCoreBootstrap.Config;
using MvcCoreBootstrapButton.Config;

namespace MvcCoreBootstrapButton.Builders
{
    public class MvcCoreBootstrapButtonAjaxBuilder : BuilderBase
    {
        private readonly AjaxConfig _config;

        internal MvcCoreBootstrapButtonAjaxBuilder(AjaxConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the target url for the AJAX operation.
        /// </summary>
        /// <param name="url">Url</param>
        /// <returns>The AJAX builder instance.</returns>
        public MvcCoreBootstrapButtonAjaxBuilder Url(string url)
        {
            this.CheckNullPar(url, () => nameof(url));
            _config.Url = url;
            return(this);
        }

        /// <summary>
        /// Id of the element to update with the result of
        /// the AJAX operation, when using AJAX for
        /// retrieving html.
        /// </summary>
        /// <param name="id">Element id</param>
        /// <returns>The AJAX builder instance.</returns>
        public MvcCoreBootstrapButtonAjaxBuilder UpdateId(string id)
        {
            this.CheckNullPar(id, () => nameof(id));
            _config.UpdateId = id;
            return(this);
        }

        /// <summary>
        /// Id of an element to show when the AJAX operation
        /// is initiated and hide when it is finished.
        /// </summary>
        /// <param name="id">Element id</param>
        /// <returns>The AJAX builder instance.</returns>
        public MvcCoreBootstrapButtonAjaxBuilder BusyIndicatorId(string id)
        {
            this.CheckNullPar(id, () => nameof(id));
            _config.BusyIndicatorId = id;
            return(this);
        }

        /// <summary>
        /// Sets the target update mode when using AJAX for
        /// retrieving html.
        /// Default mode is 'Replace';
        /// </summary>
        /// <param name="mode">Update mode</param>
        /// <returns>The AJAX builder instance.</returns>
        public MvcCoreBootstrapButtonAjaxBuilder UpdateMode(AjaxUpdateMode mode)
        {
            _config.UpdateMode = mode;
            return(this);
        }

        /// <summary>
        /// Name of java script function to call when
        /// the AJAX operation is initiated.
        /// </summary>
        /// <param name="func">Javascript function</param>
        /// <returns>The AJAX builder instance.</returns>
        public MvcCoreBootstrapButtonAjaxBuilder Start(string func)
        {
            _config.Start = func;
            return(this);
        }

        /// <summary>
        /// Name of java script function to call when
        /// the AJAX operation is successful.
        /// The function will recieve the data received from
        /// the AJAX call as a parameter.
        /// </summary>
        /// <param name="func">Javascript function</param>
        /// <returns>The AJAX builder instance.</returns>
        public MvcCoreBootstrapButtonAjaxBuilder Success(string func)
        {
            _config.Success = func;
            return(this);
        }

        /// <summary>
        /// Name of java script function to call when
        /// the AJAX operation fails.
        /// </summary>
        /// <param name="func">Javascript function</param>
        /// <returns>The AJAX builder instance.</returns>
        public MvcCoreBootstrapButtonAjaxBuilder Error(string func)
        {
            _config.Error = func;
            return(this);
        }

        /// <summary>
        /// Name of java script function to call when
        /// the AJAX operation is complete.
        /// </summary>
        /// <param name="func">Javascript function</param>
        /// <returns>The AJAX builder instance.</returns>
        public MvcCoreBootstrapButtonAjaxBuilder Complete(string func)
        {
            _config.Complete = func;
            return(this);
        }

        /// <summary>
        /// Disables the button when the AJAX operation is initiated
        /// and enables it when it is finished.
        /// </summary>
        /// <returns>The AJAX builder instance.</returns>
        public MvcCoreBootstrapButtonAjaxBuilder ButtonState()
        {
            _config.ButtonState = true;
            return(this);
        }

        /// <summary>
        /// Wheather tho clear the update area when the AJAX operation
        /// is initiated.
        /// </summary>
        /// <returns>The AJAX builder instance.</returns>
        public MvcCoreBootstrapButtonAjaxBuilder ClearUpdateArea(bool condition = true)
        {
            _config.ClearUpdateArea = condition;
            return(this);
        }
    }
}