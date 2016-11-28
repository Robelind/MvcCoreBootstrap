using System;
using System.Linq.Expressions;

namespace MvcCoreBootstrapForm.Config
{
    public class RadioButtonConfig<TModel, TResult> : ControlConfig
    {
        public RadioButtonConfig()
        {
            AutoLabel = false;
        }

        public Expression<Func<TModel, TResult>> Expression { get; set; }
        //public Expression<Func<TModel, Enum>> EnumExpression { get; set; }
        public object Value { get; set; }
    }
}
