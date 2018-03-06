using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackCalculatorProgram
{
	interface IStack <T>
	{
		void Push(T element);

		T Pop();

		bool IsEmpty();

		int GetSize();

	}
}
