using System;

namespace StackCalculatorProgram
{
	public interface IStack <T>
	{
		void Push(T element);

		T Pop();

		bool IsEmpty();

		int GetSize();
	}
}
