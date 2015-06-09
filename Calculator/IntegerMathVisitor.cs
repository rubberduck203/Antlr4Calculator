using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubberduck.Math
{
    class IntegerMathVisitor : BasicMathBaseVisitor<int>
    {
        public override int VisitCompileUnit(BasicMathParser.CompileUnitContext context)
        {
            // There can only ever be one expression in a compileUnit. The other node is EOF.
            return Visit(context.expression());
        }

        public override int VisitNumber(BasicMathParser.NumberContext context)
        {
            var result = int.Parse(context.GetText());
            System.Diagnostics.Debug.WriteLine(result);

            return result;
        }

        public override int VisitAddition(BasicMathParser.AdditionContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            System.Diagnostics.Debug.WriteLine("{0} + {1}", left, right);
            return left + right;
        }

        public override int VisitSubtraction(BasicMathParser.SubtractionContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            System.Diagnostics.Debug.WriteLine("{0} - {1}", left, right);
            return left - right;
        }

        public override int VisitMultiplication(BasicMathParser.MultiplicationContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            System.Diagnostics.Debug.WriteLine("{0} * {1}", left, right);
            return left * right;
        }

        public override int VisitDivision(BasicMathParser.DivisionContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            System.Diagnostics.Debug.WriteLine("{0} / {1}", left, right);
            return left / right;
        }

        private int WalkLeft(BasicMathParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<BasicMathParser.ExpressionContext>(0));
        }

        private int WalkRight(BasicMathParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<BasicMathParser.ExpressionContext>(1));
        }
    }
}
