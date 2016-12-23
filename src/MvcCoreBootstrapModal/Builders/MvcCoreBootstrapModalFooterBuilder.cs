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
            _config.Buttons.Add(new ModalButton { Text = text, State = state });
            return(this);
        }

        /// <summary>
        /// Adds an action button to the modal footer.
        /// </summary>
        /// <param name="text">Close button text.</param>
        /// <param name="jsFunc">Name of java script method to call when button is pressed.</param>
        /// <param name="state">Close button contextual state.</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalFooterBuilder ActionButton(string text, string jsFunc,
            ContextualState state = ContextualState.Default)
        {
            _config.Buttons.Add(new ModalButton { Text = text, State = state, JsFunc = jsFunc });
            return(this);
        }
    }
}