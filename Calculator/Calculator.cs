using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4;
using Antlr4.Runtime;

namespace Rubberduck.Math
{
    public static class Calculator
    {
        public static int Evaluate(string expression)
        {
            var lexer = new BasicMathLexer(new AntlrInputStream(expression));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowExceptionErrorListener());

            var tokens = new CommonTokenStream(lexer);
            var parser = new BasicMathParser(tokens);

            var tree = parser.compileUnit();

            var exprCount = tree.expression().Count;
            if (exprCount > 1)
            {
                throw new ArgumentException(String.Format("Too many expressions. Only one can be evaluated. {0} expressions were entered.", exprCount));
            }

            var visitor = new IntegerMathVisitor();

            return visitor.Visit(tree);
        }
    }
}
