using System;

namespace ListProgram
{
	public class List
	{
		private class Node
		{
			public int Value { get; set; }
			public Node Next { get; set; }
			public Node Prev { get; set; }
		}
		private Node head = null;
		private Node tail = null;
		private int size = 0;

		public int GetSize => size;

		public bool IsEmpty => size == 0;

		public void AddElement_End(int value)
		{
			if(IsEmpty)
			{
				head = tail = new Node()
				{
					Value = value,
					Next = null,
					Prev = null
				};
			}
			else
			{
				Node newNode = new Node()
				{
					Value = value,
					Next = null,
					Prev = tail
				};
				tail.Next = newNode;
				tail = newNode;
			}
			size++;
		}

		public void AddElement_Begin(int value)
		{
			if (IsEmpty)
			{
				head = tail = new Node()
				{
					Value = value,
					Next = null,
					Prev = null
				};
			}
			else
			{
				Node newNode = new Node()
				{
					Value = value,
					Next = head,
					Prev = null
				};
				head.Prev = newNode;
				head = newNode;
			}
			size++;
		}

		public void AddElement_Index(int value, int index)
		{
			if (IsEmpty)
			{
				head = tail = new Node()
				{
					Value = value,
					Next = null,
					Prev = null
				};
				size++;
			}
			else
			{
				if (index < 0 || index > size - 1)
				{
					throw new Exception("Error.Index is not correct!");
				}
				else
				{
					if (index != 0)
					{
						int i = 0;
						Node curr = head;
						while (i != index)
						{
							i++;
							curr = curr.Next;
						}
						Node newNode = new Node()
						{
							Value = value,
							Prev = curr.Prev,
							Next = curr
						};
						newNode.Prev.Next = newNode;
						newNode.Next.Prev = newNode;
						size++;
					}
					else
					{
						AddElement_Begin(value);
					}
				}
				
			}	
		}

		public void DeleteElement_End()
		{
			if (IsEmpty)
			{
				throw new Exception("Error. List is empty!");
			}
			else
			{
				tail = tail.Prev;
				if(size != 1)
				{
					tail.Next = null;
				}
				size--;
			}
		}

		public void DeleteElement_Begin()
		{
			if (IsEmpty)
			{
				throw new Exception("Error. List is empty!");
			}
			else
			{
				head = head.Next;
				if(size != 1)
				{
					head.Prev = null;
				}
				size--;
			}
		}

		public void DeleteElement_Index(int index)
		{
			if (IsEmpty)
			{
				throw new Exception("Error.List is empty!");
			}
			else
			{
				if (index < 0 || index > size - 1)
				{
					throw new Exception("Error.Index is not correct!");
				}
				else
				{
					if (index != 0 && index != size - 1)
					{
						int i = 0;
						Node curr = head;
						while (i != index)
						{
							i++;
							curr = curr.Next;
						}
						curr.Prev.Next = curr.Next;
						curr.Next.Prev = curr.Prev;
						curr.Next = null;
						curr.Prev = null;
						size--;
					}
					else
					{
						if (index == 0)
						{
							DeleteElement_Begin();
						}
						else if(index == size - 1)
						{
							DeleteElement_End();
						}
					}
				}	
			}
		}

		public int GetElement_End() => tail.Value;

		public int GetElement_Begin() => head.Value;

		public int GetElementValue_Index(int index)
		{
			if (IsEmpty)
			{
				throw new Exception("Error.List is empty!");
			}
			else
			{
				if (index < 0 || index > size - 1)
				{
					throw new Exception("Error.Index is not correct!");
				}
				else
				{
					int i = 0;
					Node curr = head;
					while (i != index)
					{
						i++;
						curr = curr.Next;
					}
					return curr.Value;
				}
			}
		}	
	}
}
