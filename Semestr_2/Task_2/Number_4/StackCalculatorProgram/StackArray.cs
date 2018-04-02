using System;

namespace StackCalculatorProgram
{
	public class StackArray<T> : IStack<T>
	{
		private T[] arr;
		private int size = 0;
		private int capacity = 100;

		public StackArray()
		{
			arr = new T[capacity];
		}

		private void SizeUp()
		{
			T[] newArr = new T[capacity * 2];
			for (int i = 0; i < capacity; i++)
			{
				newArr[i] = arr[i];
			}
			capacity *= 2;
			arr = newArr;
		}

		public void Push(T element)
		{
			if (size == capacity)
			{
				SizeUp();
			}
			arr[size] = element;
			size++;
		}

		public T Pop()
		{
			if (IsEmpty())
			{
				throw new Exception("Error.Stack is empty!");
			}
			else
			{
				size--;
				return arr[size];
			}
		}

		public bool IsEmpty() => size == 0;

		public int GetSize() => size;
	}
}
