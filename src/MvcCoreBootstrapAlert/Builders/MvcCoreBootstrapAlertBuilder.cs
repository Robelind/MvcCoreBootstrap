using MvcCoreBootstrap.Building;
using MvcCoreBootstrapAlert.Config;

namespace MvcCoreBootstrapAlert.Builders
{
    public class MvcCoreBootstrapAlertBuilder : BuilderBase
    {
        private readonly AlertConfig _config;

        internal MvcCoreBootstrapAlertBuilder(AlertConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the id attribute for the alert.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The alert builder instance.</returns>
        public MvcCoreBootstrapAlertBuilder Id(string id)
        {
            return(this.SetConfigProp<MvcCoreBootstrapAlertBuilder>(() => _config.Id = id));
        }

        /// <summary>
        /// Sets the name attribute for the alert.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The alert builder instance.</returns>
        public MvcCoreBootstrapAlertBuilder Name(string name)
        {
            return(this.SetConfigProp<MvcCoreBootstrapAlertBuilder>(() => _config.Name = name));
        }

        /// <summary>
        /// Sets the button text.
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>The alert builder instance.</returns>
        public MvcCoreBootstrapAlertBuilder Text(string text)
        {
            return(this.SetConfigProp<MvcCoreBootstrapAlertBuilder>(() => _config.Text = text));
        }

        /// <summary>
        /// Sets whether the alert is dismissable.
        /// </summary>
        /// <param name="condition">If true, alert is dismissable</param>
        /// <returns>The alert builder instance.</returns>
        public MvcCoreBootstrapAlertBuilder Dismissable(bool condition = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapAlertBuilder>(() => _config.Dismissable = condition));
        }
}
}