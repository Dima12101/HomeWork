using System;


namespace SortingColumns
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Lines: ");
			int countLines = Int32.Parse(Console.ReadLine());
			Console.Write("Columns: ");
			int countColumns = Int32.Parse(Console.ReadLine());
			int[,] matrix = new int[countLines, countColumns];
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Matrix:");
			FillingMatrix(matrix);
			SortingColumns(matrix);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("SortingMatrix:");
			for (int i = 0; i < countLines; i++)
			{
				for (int j = 0; j < countColumns; j++)
				{
					Console.Write(matrix[i, j]);
					Console.Write(" ");
				}
				Console.WriteLine();
			}
			Console.ReadKey();
		}

		static void FillingMatrix (int[,] matrix)
		{
			Random rand = new Random();
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					matrix[i, j] = rand.Next(1, 100);
					Console.Write(matrix[i, j]);
					Console.Write(" ");
				}
				Console.WriteLine();
			}
		}

		static void SortingColumns(int[,] matrix)
		{
			bool stop = false;
			while (!stop)
			{
				stop = true;
				for (var j = 1; j < matrix.GetLength(1); j++)
				{
					if (matrix[0, j] < matrix[0, j - 1]) 
					{
						for (int i = 0; i < matrix.GetLength(0); i++)
						{
							var a = matrix[i, j];
							matrix[i, j] = matrix[i, j - 1];
							matrix[i, j - 1] = a;
						}	
						stop = false;
					}
				}
			}
		}
	}
}
