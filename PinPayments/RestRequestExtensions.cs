using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using RestSharp;

namespace PinPayments
{
    internal static class RestRequestExtensions
    {
        public static IRestRequest AddParameter<TModel>(this IRestRequest request, TModel model, Expression<Func<TModel, object>> expression,
            ParameterType paramaterType)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
            {
                return AddParameter(request, model, expression, paramaterType, memberExpression);
            }
            
            var unaryExpression = expression.Body as UnaryExpression;
            if (unaryExpression == null)
            {
                throw new ArgumentException("Only MemberExpression and UnaryExpression supported");
            }
            return AddParameter(request, model, expression, paramaterType, unaryExpression);
        }

        private static IRestRequest AddParameter<TModel>(IRestRequest request, TModel model, Expression<Func<TModel, object>> expression,
            ParameterType paramaterType, UnaryExpression unaryExpression)
        {
            var memberExpression = unaryExpression.Operand as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("UnaryExpression operand must be a MemberExpression");
            }
            return AddParameter(request, model, expression, paramaterType, memberExpression);
        }

        private static IRestRequest AddParameter<TModel>(IRestRequest request, TModel model, Expression<Func<TModel, object>> expression,
            ParameterType paramaterType, MemberExpression memberExpression)
        {
            var name = GetParameterName(memberExpression);
            var value = expression.Compile()(model);
            return AddParameterNotNull(request, name, value, paramaterType);
        }

        public static IRestRequest AddParameterNotNull(this IRestRequest request, string name, object value, ParameterType paramaterType)
        {
            return value == null
                ? request
                : request.AddParameter(name, value, paramaterType);
        }

        private static string GetParameterName(MemberExpression memberExpression)
        {
            var dataMemberAttribute =
                (DataMemberAttribute)
                    memberExpression.Member.GetCustomAttributes(typeof (DataMemberAttribute), false).SingleOrDefault();
            return (dataMemberAttribute != null)
                ? dataMemberAttribute.Name 
                : memberExpression.Member.Name;
        }
    }
}
