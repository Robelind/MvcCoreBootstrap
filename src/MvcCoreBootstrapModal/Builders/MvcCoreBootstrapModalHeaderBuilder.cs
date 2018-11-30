using MvcCoreBootstrap.Building;
using MvcCoreBootstrapModal.Config;

namespace MvcCoreBootstrapModal.Builders
{
    public class MvcCoreBootstrapModalHeaderBuilder : BuilderBase
    {
        private readonly ModalConfig _config;

        internal MvcCoreBootstrapModalHeaderBuilder(ModalConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the header title.
        /// </summary>
        /// <param name="title">Id</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalHeaderBuilder Title(string title)
        {
            return(this.SetConfigProp<MvcCoreBootstrapModalHeaderBuilder>(() => _config.Title = title));
        }
    }
}