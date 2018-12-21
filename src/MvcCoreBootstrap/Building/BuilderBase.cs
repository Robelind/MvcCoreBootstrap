using System;
using System.Collections.Generic;
using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrap.Building
{
    public class BuilderBase
    {
        protected T SetConfigProp<T>(Action configAction, object notNullProperty  = null, string notNullPropertyName = null) where T : BuilderBase
        {
            if(notNullProperty == null && notNullPropertyName != null)
            {
                throw new ArgumentNullException($"\"{notNullPropertyName}\" cannot be null");
            }
            configAction();
            return(this as T);
        }

        protected T AddCssClass<T>(IList<string> cssClasses, string cssClass, bool condition) where T : BuilderBase
        {
            if(condition)
            {
                cssClasses.Add(cssClass);
            }
            return(this as T);
        }

        protected void CheckNullPar(object parameter, Func<string> paramterNameFunc)
        {
            if(parameter == null)
            {
                throw(new ArgumentNullException(paramterNameFunc()));
            }
        }

        protected void Tooltip(string content, TooltipConfigBase config, Action<MvcCoreBootstrapTooltipBuilder> configAction = null)
        {
            this.CheckNullPar(content, () => nameof(content));
            config.Tooltip = new TooltipConfig(content);
            configAction?.Invoke(new MvcCoreBootstrapTooltipBuilder(config.Tooltip));
        }
    }
}
