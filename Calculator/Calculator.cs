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
        public static double Evaluate(string expression)
        {
            var lexer = new MathLexer(new AntlrInputStream(expression));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowExceptionErrorListener());

            var tokens = new CommonTokenStream(lexer);

            var parser = new MathParser(tokens);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(new ThrowExceptionErrorListener());

            var tree = parser.compileUnit();

            var visitor = new MathVisitor();

            return visitor.Visit(tree);
        }
    }
}
