using System;

namespace StackCalculatorProgram
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Write("Input arithmetic expression: ");
			string ArExp = Console.ReadLine();
			Console.Write("Result: ");
			Calculator cal = new Calculator();
			Console.WriteLine(cal.Result(ArExp));
			Console.ReadKey();
		}
	}
}
