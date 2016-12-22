using System;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapModal.Config;

namespace MvcCoreBootstrapModal.Builders
{
    public class MvcCoreBootstrapModalBuilder : BuilderBase
    {
        private readonly ModalConfig _config;

        internal MvcCoreBootstrapModalBuilder(ModalConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the id attribute for the modal.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Id(string id)
        {
            return(this.SetConfigProp<MvcCoreBootstrapModalBuilder>(() => _config.Id = id));
        }

        /// <summary>
        /// Sets the name attribute for the modal.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Name(string name)
        {
            return(this.SetConfigProp<MvcCoreBootstrapModalBuilder>(() => _config.Name = name));
        }

        /// <summary>
        /// Configures the modal header.
        /// </summary>
        /// <param name="configAction">Action that implements modal header configuration.</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Header(Action<MvcCoreBootstrapModalHeaderBuilder> configAction)
        {
            configAction(new MvcCoreBootstrapModalHeaderBuilder(_config));
            return(this);
        }

        /// <summary>
        /// Sets the body of the modal.
        /// </summary>
        /// <param name="body">Body content.</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Body(string body)
        {
            return(this.SetConfigProp<MvcCoreBootstrapModalBuilder>(() => _config.Body = body));
        }
    }
}