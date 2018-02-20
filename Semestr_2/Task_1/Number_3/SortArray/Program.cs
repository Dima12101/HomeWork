using System;

namespace SortArray
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write("Input Size Array: ");
			var sizeArr = Int32.Parse(Console.ReadLine());
			int[] arr = new int[sizeArr];
			GenerationArray(arr);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Generation Array: ");
			for (int i = 0; i < arr.Length; i++)
			{
				Console.Write(arr[i]);
				Console.Write(" ");
			}
			BubbleSort(arr);
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("Sorting Array: ");
			for (int i = 0; i < arr.Length; i++)
			{
				Console.Write(arr[i]);
				Console.Write(" ");
			}
			Console.ReadKey();
		}

		static void GenerationArray(int[] arr)
		{
			Random rand = new Random();
			for (var i = 0; i < arr.Length; i++)
			{
				arr[i] = rand.Next(1, 30);
			}
		}

		static void BubbleSort(int[] arr)
		{
			bool stop = false;
			while (!stop)
			{
				stop = true;
				for (var i = 1; i < arr.Length; i++)
				{
					if (arr[i] < arr[i - 1])
					{
						var a = arr[i];
						arr[i] = arr[i - 1];
						arr[i - 1] = a;
						stop = false;
					}
				}
			}
		}
	}
}
