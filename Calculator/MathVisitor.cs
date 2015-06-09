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
            // There can only ever be one expression in a compileUnit. The other node is EOF.
            return Visit(context.expression());
        }

        public override double VisitNumber(MathParser.NumberContext context)
        {
            var result = double.Parse(context.GetText());
            Debug.WriteLine(result);

            return result;
        }

        public override double VisitParenthesized(MathParser.ParenthesizedContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitExponent(MathParser.ExponentContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            Debug.WriteLine("{0} ^ {1}", left, right);
            return System.Math.Pow(left, right);
        }

        public override double VisitAdditive(MathParser.AdditiveContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == MathLexer.ADD)
            {
                Debug.WriteLine("{0} + {1}", left, right);
                return left + right;
            }
            else //BasicMathLexer.SUBTRACT
            {
                Debug.WriteLine("{0} - {1}", left, right);
                return left - right;
            }
        }

        public override double VisitMultiplicative(MathParser.MultiplicativeContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == MathLexer.MULTIPLY)
            {
                Debug.WriteLine("{0} * {1}", left, right);
                return left * right;
            }
            else //BasicMathLexer.DIVIDE
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
