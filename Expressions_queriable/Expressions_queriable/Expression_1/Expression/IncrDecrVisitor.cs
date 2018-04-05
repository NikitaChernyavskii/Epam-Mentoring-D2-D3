using System.Linq.Expressions;
using Expression1 = System.Linq.Expressions.Expression;

namespace Expression_1.Expression
{
    public class IncrDecrVisitor : ExpressionVisitor
    {
        protected override Expression1 VisitBinary(BinaryExpression node)
        {
            if (node.NodeType != ExpressionType.Add && node.NodeType != ExpressionType.Subtract)
            {
                return base.VisitBinary(node);
            }
            if (node.Left.NodeType != ExpressionType.Parameter && node.Left.NodeType != ExpressionType.Constant)
            {
                return base.VisitBinary(node);
            }

            ParameterExpression param = (ParameterExpression)node.Left;
            ConstantExpression constant = (ConstantExpression)node.Right;
            if (constant.Type != typeof(int) || (int) constant.Value != 1)
            {
                return base.VisitBinary(node);
            }

            if (node.NodeType == ExpressionType.Add)
            {
                return Expression1.Increment(param);
            }

            return Expression1.Decrement(param);
        }
    }
}
