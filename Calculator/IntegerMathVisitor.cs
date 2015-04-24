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
            return Visit(context.expression(0));
        }

        public override int VisitNumber(BasicMathParser.NumberContext context)
        {
            return int.Parse(context.GetText());
        }

        public override int VisitAddition(BasicMathParser.AdditionContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            return left + right;
        }

        public override int VisitSubtraction(BasicMathParser.SubtractionContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
      
            return left - right;
        }

        public override int VisitMultiplication(BasicMathParser.MultiplicationContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            return left * right;
        }

        public override int VisitDivision(BasicMathParser.DivisionContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

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
