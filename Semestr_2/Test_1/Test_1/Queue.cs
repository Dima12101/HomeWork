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
			public Node prev;
		}

		private Node head;
		private Node tail;

		public bool isEmpty => (head == null || tail == null);

		public void Enqueue(Tvalue _value, int _priority)
		{
			if (isEmpty)
			{
				head = tail = new Node
				{
					value = _value,
					priority = _priority,
				};
			}
			else
			{
				Node temp = tail;
				while (temp != null && _priority > temp.priority)
				{
					temp = temp.prev;
				}
				Node newNode = new Node
				{
					value = _value,
					priority = _priority,
				};
				if (temp == null)
				{
					newNode.next = head;
					head.prev = newNode;
					head = newNode;
				}
				else if (temp == tail)
				{
					newNode.prev = tail;
					tail.next = newNode;
					tail = newNode;
				}
				else
				{
					newNode.prev = temp;
					newNode.next = temp.next;
					temp.next.prev = newNode;
					temp.next = newNode;
				}
			}
		}

		public Tvalue Dequeue()
		{
			if (isEmpty)
			{
				throw new Exception("Queue is empty!");
			}
			Node temp = head;
			head = temp.next;
			if (head == null)
			{
				tail = null;
			}
			else
			{
				head.prev = null;
			}
			temp.next = null;
			return temp.value;
		}
	}
}
