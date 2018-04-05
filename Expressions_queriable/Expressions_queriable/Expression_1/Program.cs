using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Expression_1.Expression;

namespace Expression_1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestIncrDecrCustomExpressionVisitor();
            TestLambdaExpressionVisitor();

            Console.ReadLine();
        }

        private static void TestIncrDecrCustomExpressionVisitor()
        {
            Expression<Func<int, int>> incrExpression = i => i + 1;
            var customIncrExpression = new IncrDecrVisitor().VisitAndConvert(incrExpression, "");
            Console.WriteLine(customIncrExpression);
            Console.WriteLine(customIncrExpression.Compile().Invoke(1));


            Expression<Func<int, int>> decrExpression = i => i - 1;
            var customDecrExpression = new IncrDecrVisitor().VisitAndConvert(decrExpression, "");
            Console.WriteLine(customDecrExpression);
            Console.WriteLine(customDecrExpression.Compile().Invoke(1));


            Console.WriteLine("Check when i != 1");
            Expression<Func<int, int>> addTwoExpression = i => i + 2;
            var customaddTwoExpressionExpression = new IncrDecrVisitor().VisitAndConvert(addTwoExpression, "");
            Console.WriteLine(customaddTwoExpressionExpression);
            Console.WriteLine(customaddTwoExpressionExpression.Compile().Invoke(1));
        }

        private static void TestLambdaExpressionVisitor()
        {
            Expression<Action<string>> printNameExpression = i => Console.WriteLine(i);
            var lambdaVisitor = new LambdaVisitor(new LambdaNameValue { Name = "i", Value = "Mikita" })
                .VisitAndConvert(printNameExpression, "");

            Console.WriteLine(lambdaVisitor);
            lambdaVisitor.Compile().Invoke("asdsd");
        }
    }
}
