using System;
using System.Collections.Generic;

namespace Test_1
{
	public class Queue<Tvalue>
	{
		private class Node
		{
			public Tvalue value;
			public int priority;
			public Node next;
		}

		private Node head;
		private Node tail;

		public bool IsEmpty => (head == null || tail == null);

		public void Enqueue(Tvalue _value, int _priority)
		{
			if (IsEmpty)
			{
				head = tail = new Node
				{
					value = _value,
					priority = _priority,
				};
			}
			else
			{
				Node temp = head;
				bool insertInCenter = false;
				while (temp != null && _priority < temp.priority)
				{
					if (temp.next != null)
					{
						if (temp.next.priority < _priority)
						{
							insertInCenter = true;
							break;
						}
					}
					temp = temp.next;
				}
				Node newNode = new Node
				{
					value = _value,
					priority = _priority,
				};
				if (temp == head && !insertInCenter)
				{
					newNode.next = head;
					head = newNode;
				}
				else if (temp == null)
				{
					tail.next = newNode;
					tail = newNode;
				}
				else
				{
					newNode.next = temp.next;
					temp.next = newNode;
				}
			}
		}

		public Tvalue Dequeue()
		{
			if (IsEmpty)
			{
				throw new EmptyContainerException("Queue is empty!");
			}
			Node temp = head;
			head = temp.next;
			if (head == null)
			{
				tail = null;
			}
			temp.next = null;
			return temp.value;
		}
	}
}
