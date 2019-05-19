using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MathStat
{
	delegate double Func(double X);

	static class Integral
	{
		public static double MiddleRectangle(Func F, double a, double b, int n)
		{
			double result = 0.0;

			double h = (b - a) / n;
			for (int i = 1; i <= n; i++)
			{
				result += F(a + (i - 1 / 2) * h);
			}
			result *= h;
			return result;
		}
	}
}