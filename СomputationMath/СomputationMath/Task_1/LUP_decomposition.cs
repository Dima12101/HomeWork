using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace СomputationMath.Task_1
{
	static class LUP_decomposition
	{
		#region LU разложение
		static private int SearchIndexKeyElement(Matrix matrix, int indexColumn, int indexRow)
		{
			var indexKey = indexRow;
			for (int i = indexRow; i < matrix.N; i++)
			{
				if (Math.Abs(matrix.data[i][indexColumn]) > Math.Abs(matrix.data[indexKey][indexColumn]))
				{
					indexKey = i;

				}
			}
			return indexKey;
		}

		static public void LUP(Matrix A, Matrix L, Matrix U, Matrix P)
		{
			U.Copy(A);
			P.SetE();

			for (int i = 0; i < A.N; i++)
			{
				var indexKey = SearchIndexKeyElement(U, i, i);
				if (Math.Abs(U.data[indexKey][i]) < 1e-10)
				{
					//Особая
				}
				else
				{
					U.SwapRow(i, indexKey);
					L.SwapRow(i, indexKey);
					P.SwapRow(i, indexKey);
					for (int k = i; k < A.N; k++)
					{
						var alpha = U.data[k][i] / U.data[i][i];
						L.data[k][i] = alpha;
						if (k != i)
						{
							U.Row1_minus_Row2(k, i, alpha);
						}

					}
				}
			}
		}
		#endregion

		static public Matrix Reverse(Matrix L, Matrix U, Matrix P)
		{
			Matrix L_reverse = new Matrix(L.N);
			for (int i = 0; i < L.N; i++)
			{
				for (int j = L.N - 1; j >= 0; j--)
				{
					if (i < j) L_reverse.data[i][j] = 0;
					if (i == j) L_reverse.data[i][j] = 1 / L.data[i][j];
					if (i > j)
					{
						double tempSum = 0;
						for (int k = j; k <= i - 1; k++)
						{
							tempSum += L.data[i][k] * L_reverse.data[k][j];
						}
						L_reverse.data[i][j] = (-1) * (tempSum / L.data[i][i]);
					}
				}
			}

			Matrix U_reverse = new Matrix(U.N);
			for (int i = U.N - 1; i >= 0; i--)
			{
				for (int j = 0; j < U.N; j++)
				{
					if (i > j) U_reverse.data[i][j] = 0;
					if (i == j) U_reverse.data[i][j] = 1 / U.data[i][j];
					if (i < j)
					{
						double tempSum = 0;
						for (int k = j; k >= i + 1; k--)
						{
							tempSum += U.data[i][k] * U_reverse.data[k][j];
						}
						U_reverse.data[i][j] = (-1) * (tempSum / U.data[i][i]);
					}
				}
			}
			return U_reverse * L_reverse * P;
		}

		static public Vector SLAU(Matrix L, Matrix U, Matrix P, Vector b)
		{
			//Ax = b => LUx = Pb = b_swap
			Vector b_swap = P * b;

			//Ly = b_swap => найдём y
			var y = new double[L.N];
			for (int i = 0; i < L.N; i++)
			{
				var tempSum = 0.0;
				for (int j = 0; j < i; j++)
				{
					tempSum += y[j] * L.data[i][j];
				}
				y[i] = b_swap.data[i] - tempSum;
			}

			//Ux = y => найдём x
			var x = new Vector(U.N);
			for (int i = U.N - 1; i >= 0; i--)
			{
				var tempSum = 0.0;
				for (int j = i + 1; j < U.N; j++)
				{
					tempSum += x.data[j] * U.data[i][j];
				}
				x.data[i] = (y[i] - tempSum) / U.data[i][i];
			}

			return x;
		}

		static public double Det(Matrix L, Matrix U, Matrix P)
		{
			Matrix P_copy = new Matrix(P.N);
			P_copy.Copy(P);

			double detA = 1;
			int sign = 1;

			for (int i = 0; i < L.N; i++)
			{
				detA *= U.data[i][i];
				if (P_copy.data[i][i] != 1)
				{
					sign *= -1;

					int i_swap = i;
					for (; i_swap < L.N; i_swap++)
					{
						if (P_copy.data[i_swap][i] == 1)
						{
							break;
						}
					}
					P_copy.SwapRow(i, i_swap);

				}
			}
			return detA * sign;
		}
	}
}