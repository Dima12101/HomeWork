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
		public void Test_AddElement_End()
		{
			list.AddElement_End(1);
			Assert.AreEqual(1, list.GetElement_End());
		}

		[TestMethod]
		public void Test_AddElement_Begin()
		{
			list.AddElement_Begin(1);
			Assert.AreEqual(1, list.GetElement_Begin());
		}

		[TestMethod]
		public void Test_AddTwoElement_Begin()
		{
			list.AddElement_Begin(1);
			list.AddElement_Begin(2);
			Assert.AreEqual(2, list.GetElementValue_Index(0));
			Assert.AreEqual(1, list.GetElementValue_Index(1));
		}

		[TestMethod]
		public void Test_AddTwoElement_End()
		{
			list.AddElement_End(1);
			list.AddElement_End(2);
			Assert.AreEqual(1, list.GetElementValue_Index(0));
			Assert.AreEqual(2, list.GetElementValue_Index(1));
		}

		[TestMethod]
		public void Test_AddElement_Index()
		{
			list.AddElement_End(1);
			list.AddElement_End(3);
			list.AddElement_Index(2, 1);
			Assert.AreEqual(2, list.GetElementValue_Index(1));
		}

		[TestMethod]
		public void Test_DeleteElement_End()
		{
			list.AddElement_End(1);
			list.DeleteElement_End();
			Assert.IsTrue(list.IsEmpty);
		}

		[TestMethod]
		public void Test_DeleteElement_Begin()
		{
			list.AddElement_Begin(1);
			list.DeleteElement_Begin();
			Assert.IsTrue(list.IsEmpty);
		}

		[TestMethod]
		public void Test_DeleteElement_Index()
		{
			list.AddElement_End(1);
			list.AddElement_End(2);
			list.AddElement_End(3);
			list.DeleteElement_Index(1);
			Assert.AreEqual(1, list.GetElementValue_Index(0));
			Assert.AreEqual(3, list.GetElementValue_Index(1));
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void Test_DeleteElement_ListIsEmpty()
		{
			list.DeleteElement_Begin();
			list.DeleteElement_End();
			list.DeleteElement_Index(0);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void Test_NotCorrectIndex_Add()
		{
			list.AddElement_End(1);
			list.AddElement_End(2);
			list.AddElement_End(3);

			list.AddElement_Index(4, -1);
			list.AddElement_Index(4, list.GetSize);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void Test_NotCorrectIndex_Delete()
		{
			list.AddElement_End(1);
			list.AddElement_End(2);
			list.AddElement_End(3);
			
			list.DeleteElement_Index(-1);
			list.DeleteElement_Index(list.GetSize);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void Test_NotCorrectIndex_Get()
		{
			list.AddElement_End(1);
			list.AddElement_End(2);
			list.AddElement_End(3);

			list.GetElementValue_Index(-1);
			list.GetElementValue_Index(list.GetSize);
		}
	}
}
