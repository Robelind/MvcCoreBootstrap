using System;
using System.Collections.Generic;

namespace MvcCoreBootstrap.Building
{
    public class BuilderBase
    {
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
    }
}
