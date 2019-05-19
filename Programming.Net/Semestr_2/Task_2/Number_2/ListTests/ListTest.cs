using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListProgram;

namespace ListTests
{
	[TestClass]
	public class ListTest
	{
		private List list;

		[TestInitialize]
		public void Initialize()
		{
			list = new List();
		}

		[TestMethod]
		public void TestAddElementEnd()
		{
			list.AddElementEnd(1);
			Assert.AreEqual(1, list.GetElementEnd());
		}

		[TestMethod]
		public void TestAddElementBegin()
		{
			list.AddElementBegin(1);
			Assert.AreEqual(1, list.GetElementBegin());
		}

		[TestMethod]
		public void TestAddTwoElementBegin()
		{
			list.AddElementBegin(1);
			list.AddElementBegin(2);
			Assert.AreEqual(2, list.GetElementValueIndex(0));
			Assert.AreEqual(1, list.GetElementValueIndex(1));
		}

		[TestMethod]
		public void TestAddTwoElementEnd()
		{
			list.AddElementEnd(1);
			list.AddElementEnd(2);
			Assert.AreEqual(1, list.GetElementValueIndex(0));
			Assert.AreEqual(2, list.GetElementValueIndex(1));
		}

		[TestMethod]
		public void TestAddElementIndex()
		{
			list.AddElementEnd(1);
			list.AddElementEnd(3);
			list.AddElementIndex(2, 1);
			Assert.AreEqual(2, list.GetElementValueIndex(1));
		}

		[TestMethod]
		public void TestDeleteElementEnd()
		{
			list.AddElementEnd(1);
			list.DeleteElementEnd();
			Assert.IsTrue(list.IsEmpty);
		}

		[TestMethod]
		public void TestDeleteElementBegin()
		{
			list.AddElementBegin(1);
			list.DeleteElementBegin();
			Assert.IsTrue(list.IsEmpty);
		}

		[TestMethod]
		public void TestDeleteElementIndex()
		{
			list.AddElementEnd(1);
			list.AddElementEnd(2);
			list.AddElementEnd(3);
			list.DeleteElementIndex(1);
			Assert.AreEqual(1, list.GetElementValueIndex(0));
			Assert.AreEqual(3, list.GetElementValueIndex(1));
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestDeleteElementListIsEmpty()
		{
			list.DeleteElementBegin();
			list.DeleteElementEnd();
			list.DeleteElementIndex(0);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestAddForNotCorrectIndex()
		{
			list.AddElementEnd(1);
			list.AddElementEnd(2);
			list.AddElementEnd(3);

			list.AddElementIndex(4, -1);
			list.AddElementIndex(4, list.GetSize);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestDeleteForNotCorrectIndex()
		{
			list.AddElementEnd(1);
			list.AddElementEnd(2);
			list.AddElementEnd(3);
			
			list.DeleteElementIndex(-1);
			list.DeleteElementIndex(list.GetSize);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void TestGetForNotCorrectIndex()
		{
			list.AddElementEnd(1);
			list.AddElementEnd(2);
			list.AddElementEnd(3);

			list.GetElementValueIndex(-1);
			list.GetElementValueIndex(list.GetSize);
		}
	}
}
