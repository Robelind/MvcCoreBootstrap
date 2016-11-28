using System;
using System.Linq.Expressions;

namespace MvcCoreBootstrapForm.Config
{
    public class RadioButtonConfig<TModel> : ControlConfig
    {
        public RadioButtonConfig()
        {
            AutoLabel = false;
        }

        public Expression<Func<TModel, bool>> Expression { get; set; }
        public object Value { get; set; }
    }
}
