using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MvcCoreBootstrapTable.Rendering
{
    internal static class ExpressionHelper
    {
        public static Expression<Func<T, object>> PropertyExpr<T>(string propName) where T : new()
        {
            T e = new T();
            PropertyInfo p = e.GetType().GetProperties().Single(pi => pi.Name == propName);
            var parameter = Expression.Parameter(e.GetType());
            var property = Expression.Property(parameter, p);
            var conversion = Expression.Convert(property, typeof(object));

            return(Expression.Lambda<Func<T, object>>(conversion, parameter));
        }

        public static Expression<Func<T, bool>> ComparisonExpr<T>(string propName, string value) where T : new()
        {
            T e = new T();
            PropertyInfo p = e.GetType().GetProperties().Single(pi => pi.Name == propName);
            var parameter = Expression.Parameter(e.GetType());
            var property = Expression.Property(parameter, p);
            var comparison = Expression.Equal(property, Expression.Constant(value));

            return(Expression.Lambda<Func<T, bool>>(comparison, parameter));
        }
    }
}
