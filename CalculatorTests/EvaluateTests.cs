using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rubberduck.Math;

namespace CalculatorTests
{
    [TestClass]
    public class EvaluateTests
    {
        private const double acceptableDelta = 0.000001;

        [TestMethod]
        public void OrderedOperation()
        {
            var expr = "1 + 6 - 2 * 3 / 2";
            // 2 * 3 = 6
            // 6 / 2 = 3
            // 1 + 6 = 7
            // 7 - 3 = 4
            Assert.AreEqual(4, Calculator.Evaluate(expr));
        }

        [TestMethod]
        public void Parens_EvaluateBeforeExponent()
        {
            var expr = "2 ^ (2 + 1)";
            Assert.AreEqual(8, Calculator.Evaluate(expr));
        }

        [TestMethod]
        public void Parens_EvaluatedBeforeDivision()
        {
            var expr = "(5 + 5) / (2 + 3)";
            Assert.AreEqual(2, Calculator.Evaluate(expr));
        }

        [TestMethod]
        public void Integer_Exponentation()
        {
            var expr = "2 ^ 3";
            Assert.AreEqual(8, Calculator.Evaluate(expr));
        }

        [TestMethod]
        public void Integer_Exponentation_Before_Addition()
        {
            var expr = "2 ^ 3 + 2";
            Assert.AreEqual(10, Calculator.Evaluate(expr));
        }

        [TestMethod]
        public void Interger_Exponentation_Before_Multiplication()
        {
            var expr = "2 ^ 3 * 2";
            Assert.AreEqual(16, Calculator.Evaluate(expr));
        }

        [TestMethod]
        public void MultiplicationAndDivisionShouldHaveSamePrecedence()
        {
            var expr = "10 / 2 * 5";
            Assert.AreEqual(25, Calculator.Evaluate(expr));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AcceptOnlyOneExpression()
        {
            var expr = "1+1 2-2 3*3";
            var answer = Calculator.Evaluate(expr);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsArgExceptionForInvalidTokens()
        {
            var expr = "1 + 5 + 2(3)";
            var value = Calculator.Evaluate(expr);
        }

        [TestMethod]
        public void Integer_Addition()
        {
            Assert.AreEqual(2, Calculator.Evaluate("1 + 1"));
        }

        [TestMethod]
        public void Integer_Addition_Repeated()
        {
            Assert.AreEqual(10, Calculator.Evaluate("1 + 2 + 3 + 4"));
        }

        [TestMethod]
        public void Integer_Subtraction()
        {
            Assert.AreEqual(2, Calculator.Evaluate("4 - 2"));
        }

        [TestMethod]
        public void Integer_Subtraction_Repeated()
        {
            Assert.AreEqual(5, Calculator.Evaluate("10 - 3 - 2"));
        }

        [TestMethod]
        public void Integer_Multiplication()
        {
            Assert.AreEqual(4, Calculator.Evaluate("2 * 2"));
        }

        [TestMethod]
        public void Integer_Multiplication_Repeated()
        {
            Assert.AreEqual(8, Calculator.Evaluate("2 * 2 * 2"));
        }

        [TestMethod]
        public void Integer_Division()
        {
            Assert.AreEqual(2, Calculator.Evaluate("4 / 2"));
        }

        [TestMethod]
        public void Integer_Division_Repeated()
        {
            Assert.AreEqual(2, Calculator.Evaluate("8 / 2 / 2"));
        }

        [TestMethod]
        public void Decimal_Addition()
        {
            var expr = "1.3 + 1.21";
            Assert.AreEqual(2.51, Calculator.Evaluate(expr));
        }

        [TestMethod]
        public void Decimal_Subtraction()
        {
            var expr = "2.22 - 1.01";
            Assert.AreEqual(1.21, Calculator.Evaluate(expr), acceptableDelta);
        }

        [TestMethod]
        public void Decimal_Multiplication()
        {
            var expr = "2 * 1.5";
            Assert.AreEqual(3, Calculator.Evaluate(expr));
        }

        [TestMethod]
        public void Decimal_Division()
        {
            var expr = "5 / 10";
            Assert.AreEqual(.5, Calculator.Evaluate(expr));
        }

        [TestMethod]
        public void Decimal_Exponent()
        {
            var expr = "1.1 ^ 2";
            Assert.AreEqual(1.21, Calculator.Evaluate(expr), acceptableDelta);
        }
    }
}
