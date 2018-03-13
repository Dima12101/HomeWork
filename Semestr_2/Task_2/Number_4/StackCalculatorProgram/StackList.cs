using System;

namespace StackCalculatorProgram
{
	public class StackList<T> : IStack<T>
	{
		private Node head;
		private int size = 0;

		private class Node
		{		
			public T Element { get; set; }
			public Node Next { get; set; }
		}

		public void Push(T element)
		{
			if (IsEmpty())
			{
				head = new Node()
				{
					Element = element,
					Next = null
				};
				size++;
			}
			else
			{
				var newNode = new Node()
				{
					Element = element,
					Next = head
				};
				head = newNode;
				size++;
			}
		}

		public T Pop()
		{
			if (IsEmpty())
			{
				throw new Exception("Error.Stack is empty!");
			}
			else
			{
				T temp = head.Element;
				head = head.Next;
				size--;
				return temp;
			}
		}

		public bool IsEmpty() => head == null;

		public int GetSize() => size;
	}
}
