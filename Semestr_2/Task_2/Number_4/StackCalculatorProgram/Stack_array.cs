using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculatorProgram
{
	public class Stack_array<T> : IStack<T>
	{
		private T[] arr = null;
		private int size = 0;

		private void SizeUp()
		{
			T[] newArr = new T[size + 1];
			for (int i = 0; i < size; i++)
			{
				newArr[i] = arr[i];
			}
			arr = newArr;
		}

		private void SizeDown()
		{
			if (size - 1 == 0)
			{
				arr = null;
			}
			else
			{
				T[] newArr = new T[size - 1];
				for (int i = 0; i < size - 1; i++)
				{
					newArr[i] = arr[i];
				}
				arr = newArr;
			}
		}

		public void Push(T element)
		{
			if(IsEmpty())
			{
				arr = new T[1];
				arr[0] = element;
				size++;
			}
			else
			{
				SizeUp();
				arr[size] = element;
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
				T temp = arr[size - 1];
				SizeDown();
				size--;
				return temp;
			}
		}

		public bool IsEmpty() => size == 0;

		public int GetSize() => size;
	}
}
