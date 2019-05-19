using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test_1;

namespace Test1_UnitTests
{
	[TestClass]
	public class UnitTestForQueue
	{
		private Queue<int> queue;

		[TestInitialize]
		public void Initialize()
		{
			queue = new Queue<int>();
		}

		[TestMethod]
		public void TestEnqueueOneElement()
		{
			queue.Enqueue(5, 1);
			Assert.AreEqual(5, queue.Dequeue());
		}

		[TestMethod]
		public void TestEnqueueWithNegativePriority()
		{
			queue.Enqueue(5, -1);
			queue.Enqueue(6, 5);
			Assert.AreEqual(6, queue.Dequeue());
		}

		[TestMethod]
		public void TestEnqueueSomeElement()
		{
			queue.Enqueue(1, 1);
			queue.Enqueue(5, 5);
			queue.Enqueue(4, 4);
			queue.Enqueue(7, 7);
			Assert.AreEqual(7, queue.Dequeue());
			Assert.AreEqual(5, queue.Dequeue());
			Assert.AreEqual(4, queue.Dequeue());
			Assert.AreEqual(1, queue.Dequeue());
		}

		[TestMethod]
		public void TestDequeue()
		{
			queue.Enqueue(1, 1);
			queue.Enqueue(5, 5);
			queue.Enqueue(7, 7);
			queue.Enqueue(4, 4);
			Assert.AreEqual(7, queue.Dequeue());
		}

		[TestMethod]
		[ExpectedException(typeof(EmptyContainerException))]
		public void TestDequeueException()
		{
			queue.Dequeue();
		}
	}
}
