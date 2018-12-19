using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrap.Building
{
    public class MvcCoreBootstrapTooltipBuilder : BuilderBase
    {
        private readonly TooltipConfig _config;

        internal MvcCoreBootstrapTooltipBuilder(TooltipConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the tooltip placement.
        /// </summary>
        /// <param name="placement">Placement</param>
        /// <returns>The tooltip builder instance.</returns>
        public MvcCoreBootstrapTooltipBuilder Placement(Placement placement)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTooltipBuilder>(() => _config.Placement = placement));
        }

        /// <summary>
        /// Sets the tooltip trigger.
        /// </summary>
        /// <param name="trigger">Trigger</param>
        /// <returns>The tooltip builder instance.</returns>
        public MvcCoreBootstrapTooltipBuilder Trigger(ToolTipTrigger trigger)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTooltipBuilder>(() => _config.Trigger = trigger));
        }

        /// <summary>
        /// Sets the tooltip show delay.
        /// </summary>
        /// <param name="delay">Delay in ms</param>
        /// <returns>The tooltip builder instance.</returns>
        public MvcCoreBootstrapTooltipBuilder ShowDelay(int delay)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTooltipBuilder>(() => _config.ShowDelay = delay));
        }

        /// <summary>
        /// Sets the tooltip hide delay.
        /// </summary>
        /// <param name="delay">Delay in ms</param>
        /// <returns>The tooltip builder instance.</returns>
        public MvcCoreBootstrapTooltipBuilder HideDelay(int delay)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTooltipBuilder>(() => _config.HideDelay = delay));
        }
    }
}