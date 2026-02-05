using System.Linq.Expressions;

namespace Nextflow.Application.Utils;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var param = Expression.Parameter(typeof(T), "e");

        var leftBody = new ParameterReplaceVisitor(left.Parameters[0], param).Visit(left.Body)!;
        var rightBody = new ParameterReplaceVisitor(right.Parameters[0], param).Visit(right.Body)!;

        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftBody, rightBody), param);
    }

    private sealed class ParameterReplaceVisitor(Expression from, Expression to) : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node)
            => node == from ? to : base.VisitParameter(node);
    }
}

