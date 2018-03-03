using System;

namespace StackProrgam
{
	public class Stack
	{
		private class Node
		{
			public int Value { get; set; }
			public Node Next { get; set; }
		}
		private Node head = null;

		public void Push(int value)
		{
			Node newNode = new Node();
			newNode.Next = head;
			newNode.Value = value;
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

		public bool IsEmpty()
		{
			if (head == null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
