using System;

namespace StackProgram
{
	public class Stack
	{
		private class Node
		{
			public int Value { get; set; }
			public Node Next { get; set; }
		}

		private Node head;

		public void Push(int value)
		{
			var newNode = new Node()
			{
				Next = head,
				Value = value
			};
			head = newNode;
		}

		public int Pop()
		{
			if (head == null)
			{
				throw new Exception("Error.Stack is empty!");
			}
			int valueInHeadStack = head.Value;
			head = head.Next;
			return valueInHeadStack;
		}

		public bool IsEmpty => head == null;
	}
}
