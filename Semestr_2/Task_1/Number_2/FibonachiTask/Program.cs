using System;

namespace FibonachiTask
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

		static int Fibonachi(int index)
		{
			var result = 0;
			var prevOne = 1;
			var prevTwo = 1;
			if (index <= 1)
			{
				return 1;
			}
			else
			{
				for (var i = 2; i <= index; i++)
				{
					result = prevOne + prevTwo;
					prevOne = prevTwo;
					prevTwo = result;
				}
				return result;
			}
		}
	}
}
