using System;
using System.Linq.Expressions;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Builders
{
    public class MvcCoreBootstrapRadioButtonsBuilder<TModel> : BuilderBase
    {
        private readonly RadioButtonsConfig<TModel> _config;

        internal MvcCoreBootstrapRadioButtonsBuilder(RadioButtonsConfig<TModel> config)
        {
            _config = config;
        }

        /// <summary>
        /// Renders the radio buttons horizontally instead of vertically.
        /// </summary>
        /// <returns>The radion buttons builder instance.</returns>
        public MvcCoreBootstrapRadioButtonsBuilder<TModel> Horizontal()
        {
            return(this.SetConfigProp<MvcCoreBootstrapRadioButtonsBuilder<TModel>>(() => _config.Horizontal = true));
        }

        /// <summary>
        /// Configures a radio button.
        /// </summary>
        /// <param name="expression">Model property expression.</param>
        /// <param name="value">Radio button value.</param>
        /// <param name="configAction">Action that implements radio button configuration.</param>
        /// <returns>The radion buttons builder instance.</returns>
        public MvcCoreBootstrapRadioButtonsBuilder<TModel> RadioButtonFor(Expression<Func<TModel, bool>> expression,
            object value, Action<MvcCoreBootstrapRadioButtonBuilder> configAction = null)
        {
            RadioButtonConfig<TModel> config = new RadioButtonConfig<TModel> {Value = value, Expression = expression};
            MvcCoreBootstrapRadioButtonBuilder builder = new MvcCoreBootstrapRadioButtonBuilder(config);

            if(expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            configAction?.Invoke(builder);
            _config.RadioButtons.Add(config);

            return(this);
        }
    }
}