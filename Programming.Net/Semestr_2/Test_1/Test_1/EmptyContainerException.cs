using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_1
{
	public class EmptyContainerException : Exception
	{
		public EmptyContainerException(string textException)
			: base(textException)
		{ 
		}
	}
}
