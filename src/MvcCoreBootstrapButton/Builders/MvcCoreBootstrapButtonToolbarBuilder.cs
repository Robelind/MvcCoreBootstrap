using System;
using MvcCoreBootstrap;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapButton.Config;

namespace MvcCoreBootstrapButton.Builders
{
    public class MvcCoreBootstrapButtonToolbarBuilder : BuilderBase
    {
        private readonly GroupConfig _config;

        internal MvcCoreBootstrapButtonToolbarBuilder(GroupConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Configures a group in the toolbar.
        /// </summary>
        /// <param name="configAction">Configuration action</param>
        /// <returns>The toolbar builder instance.</returns>
        public MvcCoreBootstrapButtonToolbarBuilder Group(Action<MvcCoreBootstrapButtonGroupBuilder> configAction)
        {
            GroupConfig group = new GroupConfig();
            MvcCoreBootstrapButtonGroupBuilder builder = new MvcCoreBootstrapButtonGroupBuilder(group);
            
            this.CheckNullPar(configAction, () => nameof(configAction));
            configAction(builder);
            _config.Groups.Add(group);

            return(this);
        }

        /// <summary>
        /// Sets the size for all buttons in the toolbar.
        /// </summary>
        /// <param name="size">Size</param>
        /// <param name="condition">If true, the size will be applied.</param>
        /// <returns>The toolbar builder instance.</returns>
        public MvcCoreBootstrapButtonToolbarBuilder ButtonSize(MvcCoreBootstrapButtonSize size, bool condition = true)
        {
            _config.ButtonSize = condition ? size : MvcCoreBootstrapButtonSize.Default;
            return(this);
        }

        /// <summary>
        /// Sets the <see cref="ContextualState"/> for the buttons in the toolbar.
        /// </summary>
        /// <param name="state">Contextual state</param>
        /// <param name="condition">If true, the contextual state will be applied.</param>
        /// <returns>The toolbar builder instance.</returns>
        public MvcCoreBootstrapButtonToolbarBuilder Contextual(ContextualState state, bool condition = true)
        {
            _config.State = condition ? state : ContextualState.Default;
            return(this);
        }
    }
}