using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Rubberduck.Math
{
    internal class MathVisitor : MathBaseVisitor<double>
    {
        public override double VisitCompileUnit(MathParser.CompileUnitContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitNumberExpr(MathParser.NumberExprContext context)
        {
            var result = double.Parse(context.GetText());
            Debug.WriteLine(result);

            return result;
        }

        public override double VisitParenthesizedExpr(MathParser.ParenthesizedExprContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitExponentialExpr(MathParser.ExponentialExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            Debug.WriteLine("{0} ^ {1}", left, right);
            return System.Math.Pow(left, right);
        }

        public override double VisitAdditiveExpr(MathParser.AdditiveExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == MathLexer.ADD)
            {
                Debug.WriteLine("{0} + {1}", left, right);
                return left + right;
            }
            else //MathLexer.SUBTRACT
            {
                Debug.WriteLine("{0} - {1}", left, right);
                return left - right;
            }
        }

        public override double VisitMultiplicativeExpr(MathParser.MultiplicativeExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == MathLexer.MULTIPLY)
            {
                Debug.WriteLine("{0} * {1}", left, right);
                return left * right;
            }
            else //MathLexer.DIVIDE
            {
                Debug.WriteLine("{0} / {1}", left, right);
                return left / right;
            }
        }

        private double WalkLeft(MathParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<MathParser.ExpressionContext>(0));
        }

        private double WalkRight(MathParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<MathParser.ExpressionContext>(1));
        }
    }
}
