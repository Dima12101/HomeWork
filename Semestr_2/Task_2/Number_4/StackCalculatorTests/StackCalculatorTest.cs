using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackCalculatorProgram;


namespace StackCalculatorTests
{
	[TestClass]
	public class StackCalculatorTest
	{
		Calculator cal;

		[TestInitialize]
		public void Initialize()
		{
			cal = new Calculator();
		}

		[TestMethod]
		public void TestCalculateMinus()
		{
			Assert.AreEqual(0, cal.Result("5 - 5"));
		}

		[TestMethod]
		public void TestCalculatePlus()
		{
			Assert.AreEqual(10, cal.Result("5 + 5"));
		}

		[TestMethod]
		public void TestCalculateMultiply()
		{
			Assert.AreEqual(25, cal.Result("5 * 5"));
		}

		[TestMethod]
		public void TestCalculateSplit()
		{
			Assert.AreEqual(1, cal.Result("5 / 5"));
		}

		[TestMethod]
		public void TestCheckPriorityMultiply()
		{
			Assert.AreEqual(26, cal.Result("5 * 5 + 1"));
		}

		[TestMethod]
		public void TestCheckPrioritySplit()
		{
			Assert.AreEqual(26, cal.Result("5 * 5 + 1"));
		}

		[TestMethod]
		public void CheckCountingInBrackets()
		{
			Assert.AreEqual(10, cal.Result("2 * (3 + 2)"));
		}

		[TestMethod]
		public void TestHurdArithmeticExpression()
		{
			Assert.AreEqual(20, cal.Result("5 * 2 + ((6 / 2 - 1) + 3) * 2"));
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestArithmeticExpressionIsEmpty()
		{
			int result = cal.Result("");
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestDisadvantageDigits()
		{
			int result = cal.Result("5 + ");
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestDisadvantageSymbols()
		{
			int result = cal.Result("5 5");
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestNotCorrectBrackets_One()
		{
			int result = cal.Result(") 5 + 5 )");
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestNotCorrectBrackets_Two()
		{
			int result = cal.Result(" 5 + 5 )");
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestNotCorrectBrackets_Three()
		{
			int result = cal.Result("( 5 + 5");
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestNotCorrectBrackets_Four()
		{
			int result = cal.Result("2 2 ( 5 + 5");
		}
	}
}