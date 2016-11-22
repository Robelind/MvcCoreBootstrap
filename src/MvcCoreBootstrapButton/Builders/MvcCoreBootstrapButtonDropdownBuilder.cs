using System;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapButton.Config;

namespace MvcCoreBootstrapButton.Builders
{
    public class MvcCoreBootstrapButtonDropdownBuilder : BuilderBase
    {
        private readonly DropdownConfig _config;

        internal MvcCoreBootstrapButtonDropdownBuilder(DropdownConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Configures an item in the dropdown.
        /// </summary>
        /// <param name="configAction">Configuration action</param>
        /// <returns>The dropdown builder instance.</returns>
        public MvcCoreBootstrapButtonDropdownBuilder Item(Action<MvcCoreBootstrapButtonDropdownItemBuilder> configAction)
        {
            DropdownItemConfig itemConfig = new DropdownItemConfig();
            MvcCoreBootstrapButtonDropdownItemBuilder builder = new MvcCoreBootstrapButtonDropdownItemBuilder(itemConfig);
            
            this.CheckNullPar(configAction, () => nameof(configAction));
            configAction(builder);
            _config.Items.Add(itemConfig);

            return(this);
        }
    }
}