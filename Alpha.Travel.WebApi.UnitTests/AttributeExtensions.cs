namespace Alpha.Travel.WebApi.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class AttributeExtensions
    {
        public static IEnumerable<Attribute> GetAttributes<T>(this T obj)
        {
            var type = typeof(T);
            return type.GetTypeInfo().GetCustomAttributes();
        }

        public static IEnumerable<Attribute> GetAttributesOn<T>(this T obj, Expression<Func<T, object>> expression)
        {
            IEnumerable<Attribute> attributes = new List<Attribute>();

            if (expression.Body is MethodCallExpression callExpression)
            {
                attributes = callExpression.Method.GetCustomAttributes();
            }

            var body = expression.Body as UnaryExpression;
            if (body?.Operand is MemberExpression memberExpression)
            {
                attributes = memberExpression.Member.GetCustomAttributes();
            }

            return attributes;
        }
    }
}