using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace СomputationMath
{
	class Matrix
	{
		public double[][] data { get; private set; }

		public int N { get; private set; }

		public void Show()
		{
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
				{
					Console.Write(data[i][j].ToString("0.000000") + ' ');
				}
				Console.Write('\n');
			}
		}

		public Matrix(int _N)
		{
			N = _N;
			data = new double[N][];
			for (int i = 0; i < N; i++)
			{
				data[i] = new double[N];
			}


		}

		#region Операции с матрицами
		public double Norm()
		{
			double normMatrix = 0.0;
			for (int i = 0; i < N; i++)
			{
				var tempSum = 0.0;
				for (int j = 0; j < N; j++)
				{
					tempSum += Math.Abs(data[i][j]);
				}
				normMatrix = Math.Max(normMatrix, tempSum);
			}
			return normMatrix;
		}

		public static Matrix operator *(Matrix matrix1, Matrix matrix2)
		{
			if (matrix1.N != matrix2.N)
			{
				return null;
			}
			else
			{
				var N_res = matrix1.N;
				var resMatrix = new Matrix(N_res);
				for (int i = 0; i < N_res; i++)
				{
					for (int j = 0; j < N_res; j++)
					{
						var valueCell = 0.0;
						for (int k = 0; k < matrix2.N; k++)
						{
							valueCell += matrix1.data[i][k] * matrix2.data[k][j];
						}
						resMatrix.data[i][j] = valueCell;
					}

				}
				return resMatrix;
			}
		}

		public static Vector operator *(Matrix matrix, Vector vector)
		{
			if (matrix.N != vector.data.Length)
			{
				return null;
			}
			else
			{
				var resultVector = new Vector(vector.data.Length);
				for (int i = 0; i < vector.data.Length; i++)
				{
					var value = 0.0;
					for (int j = 0; j < matrix.N; j++)
					{
						value += matrix.data[i][j] * vector.data[j];
					}
					resultVector.data[i] = value;
				}
				return resultVector;
			}
		}

		public void Row1_minus_Row2(int indexRow1, int indexRow2, double alphaRow2 = 1)
		{
			for (int i = 0; i < N; i++)
			{
				data[indexRow1][i] -= data[indexRow2][i] * alphaRow2;
			}
		}

		public void SwapRow(int index1, int index2)
		{
			for (int i = 0; i < N; i++)
			{
				var temp = data[index1][i];
				data[index1][i] = data[index2][i];
				data[index2][i] = temp;
			}
		}
		#endregion

		public void GenByDiagonalPred(double q)
		{
			var rnd = new Random();
			var range = N * 25;
			for (int i = 0; i < N; i++)
			{
				var sumValues = 0.0;
				for (int j = 0; j < N; j++)
				{
					if (i != j)
					{
						double value = rnd.Next() % range;
						sumValues += value;
						data[i][j] = value * Math.Pow(-1, 1 + rnd.Next() % 2);
					}
				}
				data[i][i] = sumValues * Math.Pow(-1, 1 + rnd.Next() % 2) * q;
			}
		}

		public void Copy(Matrix matrix)
		{
			this.N = matrix.N;
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
				{
					this.data[i][j] = matrix.data[i][j];
				}
			}
		}

		public void SetE()
		{
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
				{
					data[i][j] = (i == j) ? 1 : 0;
				}
			}
		}

		public void SetValueByFunks(ScalarFunk_N[][] matrixFunks, double[] X)
		{
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
				{
					data[i][j] = matrixFunks[i][j](X);
				}
			}
		}
	}
}