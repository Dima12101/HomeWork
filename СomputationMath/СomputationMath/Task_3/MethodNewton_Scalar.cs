using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace СomputationMath.Task_2
{
	static class MethodNewton_Scalar
	{
		static private Pair<double[], double[]> Localization(double a, double b, ScalarFunk1 funk, double countArea = 10)
		{
			var leftBorders = new List<double>();
			var rightBorders = new List<double>();
			double step = (b - a) / countArea;
			double tempLeftBorder = a;
			double tempRightBorder = a + step;
			for (int i = 0; i < countArea - 1; i++)
			{
				if (funk(tempLeftBorder) * funk(tempRightBorder) < 0)
				{
					leftBorders.Add(tempLeftBorder);
					rightBorders.Add(tempRightBorder);
				}
				tempLeftBorder = tempRightBorder;
				tempRightBorder += step;
			}

			return new Pair<double[], double[]>
			{
				FirstElement = leftBorders.ToArray(),
				SecondElement = rightBorders.ToArray()
			};
		}

		//Модифицированный
		static public Pair<double[], int[]> DifferenceMethod(double a, double b,
			ScalarFunk1 funk, double Eps = 10e-4)
		{
			double countArea = 10;
			var localization = Localization(a, b, funk, countArea);
			var leftBorders = localization.FirstElement;
			var rightBorders = localization.SecondElement;

			var results = new double[leftBorders.Length];
			var iters = new int[leftBorders.Length];

			//Малая велечина по X
			double h = 10e-2;

			for (int i = 0; i < results.Length; i++)
			{
				int countIter = 0;
				double xCurrent;
				double xNext = (rightBorders[i] + leftBorders[i]) / 2;
				do
				{
					countIter++;
					xCurrent = xNext;
					xNext = xCurrent - h * (funk(xCurrent) / (funk(xCurrent + h) - funk(xCurrent)));
				} while (Math.Abs(xNext - xCurrent) > Eps);
				iters[i] = countIter;
				results[i] = xNext;
			}
			return new Pair<double[], int[]> { FirstElement = results, SecondElement = iters };
		}

		static public Pair<double[], int[]> SimplifiedMethod(double a, double b,
			ScalarFunk1 funk, ScalarFunk1 funkDerivative, double Eps = 10e-4)
		{
			double countArea = 10;
			var localization = Localization(a, b, funk, countArea);
			var leftBorders = localization.FirstElement;
			var rightBorders = localization.SecondElement;

			var results = new double[leftBorders.Length];
			var iters = new int[leftBorders.Length];

			for (int i = 0; i < results.Length; i++)
			{
				int countIter = 0;
				double xCurrent;
				double xNext = (rightBorders[i] + leftBorders[i]) / 2;
				double staticFunkDerivative = funkDerivative(xNext);
				do
				{
					countIter++;
					xCurrent = xNext;
					xNext = xCurrent - (funk(xCurrent) / staticFunkDerivative);
				} while (Math.Abs(xNext - xCurrent) > Eps);
				iters[i] = countIter;
				results[i] = xNext;
			}
			return new Pair<double[], int[]> { FirstElement = results, SecondElement = iters };
		}

		//Стандартный
		static public Pair<double[], int[]> DefualtMethod(double a, double b,
			ScalarFunk1 funk, ScalarFunk1 funkDerivative, double Eps = 10e-4)
		{
			double countArea = 10;
			var localization = Localization(a, b, funk, countArea);
			var leftBorders = localization.FirstElement;
			var rightBorders = localization.SecondElement;

			var results = new double[leftBorders.Length];
			var iters = new int[leftBorders.Length];

			for (int i = 0; i < results.Length; i++)
			{
				int countIter = 0;
				double xCurrent;
				double xNext = (rightBorders[i] + leftBorders[i]) / 2;
				do
				{
					countIter++;
					xCurrent = xNext;
					xNext = xCurrent - (funk(xCurrent) / funkDerivative(xCurrent));
				} while (Math.Abs(xNext - xCurrent) > Eps);
				iters[i] = countIter;
				results[i] = xNext;
			}
			return new Pair<double[], int[]> { FirstElement = results, SecondElement = iters };
		}
	}
}