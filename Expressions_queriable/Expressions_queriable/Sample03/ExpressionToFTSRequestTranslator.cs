﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample03
{
    public class ExpressionToFTSRequestTranslator : ExpressionVisitor
    {
        StringBuilder resultString;

        public string Translate(Expression exp)
        {
            resultString = new StringBuilder();
            Visit(exp);

            return resultString.ToString();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && (node.Method.Name == "Where"))
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }

            if (node.Method.DeclaringType == typeof(string)
                && (node.Method.Name == "StartsWith"))
            {
                return ProcessStartsWith(node);
            }

            if (node.Method.DeclaringType == typeof(string)
                && (node.Method.Name == "EndsWith"))
            {
                return ProcessEndsWith(node);
            }

            if (node.Method.DeclaringType == typeof(string)
                && (node.Method.Name == "Contains"))
            {
                return ProcessContains(node);
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    if (node.Left.NodeType == ExpressionType.MemberAccess && node.Right.NodeType == ExpressionType.Constant)
                    {
                        Visit(node.Left);
                        resultString.Append("(");
                        Visit(node.Right);
                        resultString.Append(")");
                        break;
                    }

                    if (node.Right.NodeType == ExpressionType.MemberAccess && node.Left.NodeType == ExpressionType.Constant)
                    {
                        Visit(node.Right);
                        resultString.Append("(");
                        Visit(node.Left);
                        resultString.Append(")");
                        break;
                    }

                    break;
                case ExpressionType.AndAlso:
                    Visit(node.Left);
                    resultString.Append(" AND ");
                    Visit(node.Right);
                    break;

                default:
                    throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            resultString.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            resultString.Append(node.Value);

            return node;
        }

        private Expression ProcessStartsWith(MethodCallExpression node)
        {
            var memberAccess = (MemberExpression)node.Object;
            resultString.Append($"{memberAccess.Member.Name}:");
            resultString.Append("(");
            var valueExpression = node.Arguments[0];
            Visit(valueExpression);
            resultString.Append("*)");

            return node;
        }

        private Expression ProcessEndsWith(MethodCallExpression node)
        {
            var memberAccess = (MemberExpression)node.Object;
            resultString.Append($"{memberAccess.Member.Name}:");
            resultString.Append("(*");
            var valueExpression = node.Arguments[0];
            Visit(valueExpression);
            resultString.Append(")");

            return node;
        }

        private Expression ProcessContains(MethodCallExpression node)
        {
            var memberAccess = (MemberExpression)node.Object;
            resultString.Append($"{memberAccess.Member.Name}:");
            resultString.Append("(*");
            var valueExpression = node.Arguments[0];
            Visit(valueExpression);
            resultString.Append("*)");

            return node;
        }
    }
}
