using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Rubberduck.Math
{
    class MathVisitor : BasicMathBaseVisitor<double>
    {
        public override double VisitCompileUnit(BasicMathParser.CompileUnitContext context)
        {
            // There can only ever be one expression in a compileUnit. The other node is EOF.
            return Visit(context.expression());
        }

        public override double VisitNumber(BasicMathParser.NumberContext context)
        {
            var result = double.Parse(context.GetText());
            Debug.WriteLine(result);

            return result;
        }

        public override double VisitParenthesized(BasicMathParser.ParenthesizedContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitExponent(BasicMathParser.ExponentContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            Debug.WriteLine("{0} ^ {1}", left, right);
            return System.Math.Pow(left, right);
        }

        public override double VisitAdditive(BasicMathParser.AdditiveContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == BasicMathLexer.ADD)
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

        public override double VisitMultiplicative(BasicMathParser.MultiplicativeContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == BasicMathLexer.MULTIPLY)
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

        private double WalkLeft(BasicMathParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<BasicMathParser.ExpressionContext>(0));
        }

        private double WalkRight(BasicMathParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<BasicMathParser.ExpressionContext>(1));
        }
    }
}
