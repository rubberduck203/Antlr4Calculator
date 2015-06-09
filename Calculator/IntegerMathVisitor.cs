using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Rubberduck.Math
{
    class IntegerMathVisitor : BasicMathBaseVisitor<long>
    {
        public override long VisitCompileUnit(BasicMathParser.CompileUnitContext context)
        {
            // There can only ever be one expression in a compileUnit. The other node is EOF.
            return Visit(context.expression());
        }

        public override long VisitNumber(BasicMathParser.NumberContext context)
        {
            var result = int.Parse(context.GetText());
            Debug.WriteLine(result);

            return result;
        }

        public override long VisitExponent(BasicMathParser.ExponentContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            Debug.WriteLine("{0} ^ {1}", left, right);
            return (int)System.Math.Pow(left, right);
        }

        public override long VisitAdditive(BasicMathParser.AdditiveContext context)
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

        public override long VisitMultiplicative(BasicMathParser.MultiplicativeContext context)
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

        private long WalkLeft(BasicMathParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<BasicMathParser.ExpressionContext>(0));
        }

        private long WalkRight(BasicMathParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<BasicMathParser.ExpressionContext>(1));
        }
    }
}
