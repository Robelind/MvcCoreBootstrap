using MvcCoreBootstrap;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapModal.Config;

namespace MvcCoreBootstrapModal.Builders
{
    public class MvcCoreBootstrapModalFooterBuilder : BuilderBase
    {
        private readonly ModalConfig _config;

        internal MvcCoreBootstrapModalFooterBuilder(ModalConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Adds a close/cancel button to the modal footer.
        /// </summary>
        /// <param name="text">Close button text.</param>
        /// <param name="state">Close button contextual state.</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalFooterBuilder CloseButton(string text, ContextualState state = ContextualState.Default)
        {
            this.SetConfigProp<MvcCoreBootstrapModalFooterBuilder>(() => _config.CloseBtnText = text);
            return (this.SetConfigProp<MvcCoreBootstrapModalFooterBuilder>(() => _config.CloseBtnState = state));
        }
    }
}