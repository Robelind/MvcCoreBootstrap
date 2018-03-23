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
            Type type = new T().GetType();
            PropertyInfo p = type.GetProperties().Single(pi => pi.Name == propName);
            var parameter = Expression.Parameter(type);
            var property = Expression.Property(parameter, p);
            var conversion = Expression.Convert(property, typeof(object));

            return(Expression.Lambda<Func<T, object>>(conversion, parameter));
        }

        public static Expression<Func<T, bool>> ComparisonExpr<T>(string propName, string value) where T : new()
        {
            Type type = new T().GetType();
            PropertyInfo p = type.GetProperties().Single(pi => pi.Name == propName);
            var parameter = Expression.Parameter(type);
            var property = Expression.Property(parameter, p);
            var comparison = Expression.Equal(property, Expression.Constant(value));

            return(Expression.Lambda<Func<T, bool>>(comparison, parameter));
        }

        public static Expression<Func<T, bool>> StartsWithExpr<T>(string propName, string value) where T : new()
        {
            Type type = new T().GetType();
            PropertyInfo p = type.GetProperties().Single(pi => pi.Name == propName);
            var parameter = Expression.Parameter(type);
            MemberExpression m = Expression.MakeMemberAccess(parameter, p);
            MethodInfo mi = typeof(string).GetMethod("StartsWith", new[] { typeof(string), typeof(StringComparison) });
            Expression call = Expression.Call(m, mi, Expression.Constant(value),
                Expression.Constant(StringComparison.InvariantCultureIgnoreCase));

            return(Expression.Lambda<Func<T, bool>>(call, parameter));
        }
    }
}
