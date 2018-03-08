using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackCalculatorProgram;

namespace StackCalculatorTests
{
	[TestClass]
	public class StackArrayTest
	{
		IStack<int> stack;

		[TestInitialize]
		public void Initialize()
		{
			stack = new StackArray<int>();
		}

		[TestMethod]
		public void TestPush()
		{
			stack.Push(1);
			Assert.IsFalse(stack.IsEmpty());
		}

		[TestMethod]
		public void TestPop()
		{
			stack.Push(1);
			Assert.AreEqual(1, stack.Pop());
		}

		[TestMethod]
		public void TestPopTwoElements()
		{
			stack.Push(1);
			stack.Push(2);
			Assert.AreEqual(2, stack.Pop());
			Assert.AreEqual(1, stack.Pop());
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestPopWithEmptyStack()
		{
			stack.Pop();
		}
	}
}
