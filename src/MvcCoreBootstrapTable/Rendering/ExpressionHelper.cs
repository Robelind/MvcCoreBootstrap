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

        public static Expression<Func<T, bool>> EqualsExpr<T>(string propName, string value) where T : new()
        {
            Type type = new T().GetType();
            PropertyInfo p = type.GetProperties().Single(pi => pi.Name == propName);
            var parameter = Expression.Parameter(type);
            MemberExpression m = Expression.MakeMemberAccess(parameter, p);
            MethodInfo toString = typeof(object).GetMethod("ToString");
            var equals = Expression.Equal(Expression.Call(m, toString), Expression.Constant(value));

            return(Expression.Lambda<Func<T, bool>>(equals, parameter));
        }

        public static Expression<Func<T, bool>> StartsWithExpr<T>(string propName, string value) where T : new()
        {
            Type type = new T().GetType();
            PropertyInfo p = type.GetProperties().Single(pi => pi.Name == propName);
            var parameter = Expression.Parameter(type);
            MemberExpression m = Expression.MakeMemberAccess(parameter, p);
            MethodInfo toString = typeof(object).GetMethod("ToString");
            MethodInfo startsWith = typeof(string).GetMethod("StartsWith", new[] { typeof(string), typeof(StringComparison) });
            Expression callToString = Expression.Call(m, toString);
            Expression callStartsWith = Expression.Call(callToString, startsWith, Expression.Constant(value),
                Expression.Constant(StringComparison.InvariantCultureIgnoreCase));

            return(Expression.Lambda<Func<T, bool>>(callStartsWith, parameter));
        }
    }
}
