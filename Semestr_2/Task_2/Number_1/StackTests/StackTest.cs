﻿namespace StackTests
{
	using System;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using StackProrgam;
	[TestClass]
	public class StackTest
	{
		private Stack stack;
		[TestInitialize]
		public void Initialize()
		{
			stack = new Stack();
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
