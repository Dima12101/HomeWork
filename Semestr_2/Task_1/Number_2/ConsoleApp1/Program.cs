using System;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Input index: ");
			int index = Int32.Parse(Console.ReadLine());
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Result: ");
			Console.Write(Fibonachi(index));
			Console.ReadKey();
		}
		private static int Fibonachi(int index)
		{
			if (index <= 1)
			{
				return 1;
			}
			else
			{
				return Fibonachi(index - 1) + Fibonachi(index - 2);
			}
		}
	}
}
