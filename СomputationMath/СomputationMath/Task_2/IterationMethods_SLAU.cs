using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace СomputationMath.Task_2
{
	static class IterationMethods_SLAU
	{
		public enum Method { Yacoby, Zeydel }

		static private Vector NextVectorX_Yacoby(Matrix matrixB, Vector vectorD, Vector vectorX) => matrixB * vectorX + vectorD;

		static private Vector NextVectorX_Zeydel(Matrix matrixB, Vector vectorD, Vector vectorX)
		{
			var nextVectorX = new Vector(vectorX.data.Length);
			for (int i = 0; i < vectorX.data.Length; i++)
			{
				var tempVal = 0.0;
				for (int j = 0; j < vectorX.data.Length; j++)
				{
					tempVal += (j < i) ? nextVectorX.data[j] * matrixB.data[i][j] : vectorX.data[j] * matrixB.data[i][j];
				}
				nextVectorX.data[i] = tempVal + vectorD.data[i];
			}
			return nextVectorX;
		}

		static public Vector SLAU(Matrix A, Vector b, Vector beginX, Method method, double eps = 1e-10)
		{
			#region Составление B и d
			var matrixB = new Matrix(A.N);
			for (int i = 0; i < matrixB.N; i++)
			{
				for (int j = 0; j < matrixB.N; j++)
				{
					matrixB.data[i][j] = (i != j) ? ((-1) * (A.data[i][j] / A.data[i][i])) : 0.0;
				}
			}

			var vectorD = new Vector(A.N);
			for (int i = 0; i < vectorD.data.Length; i++)
			{
				vectorD.data[i] = b.data[i] / A.data[i][i];
			}
			#endregion

			var normB = matrixB.Norm();
			var coef = (normB >= 0.5) ? (1 - normB) / normB : 1.0;

			var vectorX = new Vector(A.N);
			vectorX.Copy(beginX);

			var nextVectorX = new Vector(A.N);
			nextVectorX.Copy(beginX);

			//var mayBeCountIter = Math.Log(eps * (1 - normB) / ((NextVectorX_Yacoby(matrixB, vectorD, vectorX) - vectorX).Norm()), normB);

			var countIter = 0;
			do
			{
				countIter++;
				vectorX.Copy(nextVectorX);
				Console.Write("Temp vector: ");
				vectorX.Show();

				switch (method)
				{
					case Method.Yacoby:
						{
							nextVectorX = NextVectorX_Yacoby(matrixB, vectorD, vectorX);
							break;
						}
					case Method.Zeydel:
						{
							nextVectorX = NextVectorX_Zeydel(matrixB, vectorD, vectorX);
							break;
						}
				}
			} while ((nextVectorX - vectorX).Norm() >= coef * eps);

			Console.Write("Count iteration: " + countIter.ToString() + '\n');

			//Console.Write("Check result (Ax - b): ");
			//var vectorCheck = this * nextVectorX - b;
			//vectorCheck.Show();

			return nextVectorX;
		}
	}
}