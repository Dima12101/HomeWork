using System;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Input digit: ");
			int digit = Int32.Parse(Console.ReadLine());
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Result: ");
			Console.Write(Factorial(digit));
			Console.ReadKey();
		}
		private static int Factorial(int digit)
		{
			if (digit <= 1)
			{
				return 1;
			}
			else
			{
				return digit * Factorial(digit - 1);
			}
		}
	}
}
