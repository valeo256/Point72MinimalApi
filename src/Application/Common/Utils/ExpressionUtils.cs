using Domain.Enums;
using System.Linq.Expressions;

namespace Application.Common.Utils;

public static class ExpressionUtils
{
    public static Expression<Func<T, bool>> CombineExpressions<T>(
    Expression<Func<T, bool>> left,
    Expression<Func<T, bool>> right,
    CombinationConditionOfEntities combinationCondition)
    {
        if (combinationCondition == CombinationConditionOfEntities.And)
        {
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    left.Body,
                    new ExpressionParameterReplacer(right.Parameters, left.Parameters).Visit(right.Body)),
                left.Parameters);
        }
        else if (combinationCondition == CombinationConditionOfEntities.Or)
        {
            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(
                    left.Body,
                    new ExpressionParameterReplacer(right.Parameters, left.Parameters).Visit(right.Body)),
                left.Parameters);
        }

        return left;
    }

    internal class ExpressionParameterReplacer : ExpressionVisitor
    {
        private readonly IDictionary<ParameterExpression, ParameterExpression> _parameterMap;
        internal ExpressionParameterReplacer(
            IList<ParameterExpression> fromParameters,
            IList<ParameterExpression> toParameters)
        {
            _parameterMap = new Dictionary<ParameterExpression, ParameterExpression>();

            for (var i = 0; i != fromParameters.Count && i != toParameters.Count; i++)
            {
                _parameterMap[fromParameters[i]] = toParameters[i];
            }
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_parameterMap.TryGetValue(node, out var replacement))
            {
                node = replacement;
            }
            return base.VisitParameter(node);
        }
    }
}

