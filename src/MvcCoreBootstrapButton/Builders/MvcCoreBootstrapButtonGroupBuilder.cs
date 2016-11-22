using System;
using System.Collections.Generic;
using MvcCoreBootstrap;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapButton.Config;

namespace MvcCoreBootstrapButton.Builders
{
    public class MvcCoreBootstrapButtonGroupBuilder : BuilderBase
    {
        private readonly GroupConfig _config;

        internal MvcCoreBootstrapButtonGroupBuilder(GroupConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Configures a button in the group.
        /// </summary>
        /// <param name="configAction">Configuration action</param>
        /// <returns>The group builder instance.</returns>
        public MvcCoreBootstrapButtonGroupBuilder Button(Action<MvcCoreBootstrapButtonBuilder> configAction)
        {
            ButtonConfig button = new ButtonConfig();
            MvcCoreBootstrapButtonBuilder builder = new MvcCoreBootstrapButtonBuilder(button);
            
            this.CheckNullPar(configAction, () => nameof(configAction));
            configAction(builder);
            _config.Buttons.Add(button);

            return(this);
        }

        /// <summary>
        /// Sets the size for all buttons in the button group.
        /// </summary>
        /// <param name="size">Size</param>
        /// <param name="condition">If true, the size will be applied.</param>
        /// <returns>The group builder instance.</returns>
        public MvcCoreBootstrapButtonGroupBuilder ButtonSize(MvcCoreBootstrapButtonSize size, bool condition = true)
        {
            _config.ButtonSize = condition ? size : MvcCoreBootstrapButtonSize.Default;
            return(this);
        }

        /// <summary>
        /// Sets the <see cref="ContextualState"/> for the buttons in the group.
        /// </summary>
        /// <param name="state">Contextual state</param>
        /// <param name="condition">If true, the contextual state will be applied.</param>
        /// <returns>The group builder instance.</returns>
        public MvcCoreBootstrapButtonGroupBuilder Contextual(ContextualState state, bool condition = true)
        {
            _config.State = condition ? state : ContextualState.Default;
            return(this);
        }

        /// <summary>
        /// Renders the button group vertically instead of horizontally.
        /// </summary>
        /// <returns>The group builder instance.</returns>
        public MvcCoreBootstrapButtonGroupBuilder Vertical()
        {
            _config.Vertical = true;
            return(this);
        }

        /// <summary>
        /// Configures a toolbar.
        /// </summary>
        /// <returns>Toolbar builder.</returns>
        public MvcCoreBootstrapButtonToolbarBuilder Toolbar()
        {
            _config.Groups = new List<GroupConfig>();
            return(new MvcCoreBootstrapButtonToolbarBuilder(_config));
        }
    }
}