using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WriteArray
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("Input N: ");
			int N = Int32.Parse(Console.ReadLine());
			int[,] arr = new int[N, N];
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine("Array:");
			FillingArray(arr, N);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("ElementArray for Spiral:");
			WriteArraySpiral(arr, N);
			Console.ReadKey();
		}

		static void FillingArray(int[,] arr, int N)
		{
			int digit = 1;
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
				{
					arr[i, j] = digit;
					digit++;
					Console.Write(arr[i, j]);
					Console.Write(" ");
				}
				Console.WriteLine();
			}
		}

		static void WriteArraySpiral(int[,] arr, int N)
		{
			int x = N / 2;
			int y = N / 2;
			var step = 1;
			var dir = -1;
			var countWriteElement = 0;
			while (countWriteElement != arr.Length)
			{
				for (var i = 0; i < step; i++)
				{
					Console.Write(arr[x, y]);
					Console.Write(" ");
					countWriteElement++;
					y += dir;
				}
				if (countWriteElement != arr.Length)
				{
					for (var i = 0; i < step; i++)
					{
						Console.Write(arr[x, y]);
						Console.Write(" ");
						countWriteElement++;
						x += dir;
					}
				}
				
				step++;
				dir *= -1;
				if (step == N)
				{
					step = arr.Length - countWriteElement;
				}
			}

		}
	}
}
