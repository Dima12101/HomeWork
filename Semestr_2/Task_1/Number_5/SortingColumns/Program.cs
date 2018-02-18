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
			FillingMatrix(matrix, countLines, countColumns);
			SortingColumns(matrix, countLines, countColumns);
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

		static void FillingMatrix (int[,] matrix, int countLines, int countColumns)
		{
			Random rand = new Random();
			for (int i = 0; i < countLines; i++)
			{
				for (int j = 0; j < countColumns; j++)
				{
					matrix[i, j] = rand.Next(1, 100);
					Console.Write(matrix[i, j]);
					Console.Write(" ");
				}
				Console.WriteLine();
			}
		}

		static void SortingColumns(int[,] matrix, int countLines, int countColumns)
		{
			bool stop = false;
			while (!stop)
			{
				stop = true;
				for (var j = 1; j < countColumns; j++)
				{
					if (matrix[0, j] < matrix[0,j - 1])
					{
						for (int i = 0; i < countLines; i++)
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
