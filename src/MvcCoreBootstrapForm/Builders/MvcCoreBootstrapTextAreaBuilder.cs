using System;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Builders
{
    public class MvcCoreBootstrapTextAreaBuilder : BuilderBase
    {
        private readonly TextAreaConfig _config;

        internal MvcCoreBootstrapTextAreaBuilder(TextAreaConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the id attribute for the text area.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The text area builder instance.</returns>
        public MvcCoreBootstrapTextAreaBuilder Id(string id)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextAreaBuilder>(() => _config.Id = id));
        }

        /// <summary>
        /// Sets the name attribute for the text area.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The text area builder instance.</returns>
        public MvcCoreBootstrapTextAreaBuilder Name(string name)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextAreaBuilder>(() => _config.Name = name));
        }

        /// <summary>
        /// Do not generate a label automatically for the text area.
        /// </summary>
        /// <returns>The text area builder instance.</returns>
        public MvcCoreBootstrapTextAreaBuilder NoLabel()
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextAreaBuilder>(() => _config.AutoLabel = false));
        }

        /// <summary>
        /// Sets the label for the text area.
        /// </summary>
        /// <param name="label">text area label.</param>
        /// <returns>The text area builder instance.</returns>
        public MvcCoreBootstrapTextAreaBuilder Label(string label)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextAreaBuilder>(() => _config.Label = label));
        }

        /// <summary>
        /// Sets the disabled state for the text area.
        /// </summary>
        /// <param name="disabled">If true, the text area is disabled</param>
        /// <returns>The text area builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder Disabled(bool disabled = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.Disabled = disabled));
        }

        /// <summary>
        /// Sets the read only state for the text area.
        /// </summary>
        /// <param name="conditon">If true, the text area is read only.</param>
        /// <returns>The text area builder instance.</returns>
        public MvcCoreBootstrapTextInputBuilder ReadOnly(bool conditon = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapTextInputBuilder>(() => _config.ReadOnly = conditon));
        }

        /// <summary>
        /// Sets a css class for the text area element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the text area element.</param>
        /// <returns>The text area builder instance.</returns>
        public MvcCoreBootstrapTextAreaBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapTextAreaBuilder>(_config.CssClasses, cssClass, condition));
        }

        /// <summary>
        /// Tooltip for the text area.
        /// </summary>
        /// <param name="content">Tooltip content.</param>
        /// <param name="configAction">Action that implements tooltip configuration.</param>
        /// <returns>The text area builder instance.</returns>
        public MvcCoreBootstrapTextAreaBuilder Tooltip(string content, Action<MvcCoreBootstrapTooltipBuilder> configAction = null)
        {
            this.Tooltip(content, _config, configAction);
            return(this);
        }
    }
}