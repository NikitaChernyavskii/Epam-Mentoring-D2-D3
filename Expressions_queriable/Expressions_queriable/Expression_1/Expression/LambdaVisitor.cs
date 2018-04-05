using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Expression1 = System.Linq.Expressions.Expression;

namespace Expression_1.Expression
{
    public class LambdaVisitor : ExpressionVisitor
    {
        private Dictionary<string, string> _lambdaNameValues;

        public LambdaVisitor(params LambdaNameValue[] lambdaNameValues)
        {
            _lambdaNameValues = lambdaNameValues.ToDictionary(kvp => kvp.Name, kvp => kvp.Value);
        }

        //protected override Expression1 VisitLambda<T>(Expression<T> node)
        //{
        //    var parameter = node.Parameters.First();
        //    string value;
        //    if (!_lambdaNameValues.TryGetValue(parameter.Name, out value))
        //    {
        //        return base.VisitLambda(node);
        //    }

        //    ConstantExpression constant = Expression1.Constant(value);
        //    return base.VisitLambda(node);
        //    //return Expression1.Lambda(node.Body, constant)
        //}

        protected override Expression1 VisitParameter(ParameterExpression node)
        {
            string value;
            if (!_lambdaNameValues.TryGetValue(node.Name, out value))
            {
                return base.VisitParameter(node);
            }

            return Expression1.Constant(value);
        }
    }
}
