using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using СomputationMath.Task_1;
using СomputationMath.Task_2;

namespace СomputationMath.Task_4
{
	static class IntegralCalculation
	{
		static public double MiddleRectangleRule (ScalarFunk1 F,double a, double b, int n)
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

		private delegate double MomentFunk(double a, double c, double d, double alpha);
		//Для p(x) = 1 / (x - a)^alpha
		static private MomentFunk momentBase = (double a, double c, double d, double alpha) => (Math.Pow(d - a, 1 - alpha) - Math.Pow(c - a, 1 - alpha)) / (1 - alpha);
		static private double CountMoment(double a, double c, double d, double alpha, int index) =>
			(index == 0) ? momentBase(a, c, d, alpha) : CountMoment(a, c, d, alpha - 1, index - 1) + a * CountMoment(a, c, d, alpha, index - 1);

		static private void FillMatrixX(Matrix matrixX, Vector vectorX)
		{
			for (int i = 0; i < matrixX.N; i++)
			{
				for (int j = 0; j < matrixX.N; j++)
				{
					matrixX.data[i][j] = Math.Pow(vectorX.data[j], i);
				}
			}
		}


		static public double IKF_NewtonCots(ScalarFunk1 f, double defA, double a, double b, double alpha, int n)
		{
			//Получаем вектор узлов
			Vector vectorX = new Vector(n);
			double step = (b - a) / (n - 1);
			for (int i = 0; i < n; i++)
			{
				vectorX.data[i] = a + i * step;
			}

			//Вычисляем моменты от 0 до n - 1
			Vector vectorMoments = new Vector(n);
			for (int i = 0; i < n; i++)
			{
				vectorMoments.data[i] = CountMoment(defA, a, b, alpha, i);
			}

			//Решаем СЛАУ и находим Aj
			Matrix matrixX = new Matrix(n);
			FillMatrixX(matrixX, vectorX);
			Matrix matrixX_L = new Matrix(n);
			Matrix matrixX_U = new Matrix(n);
			Matrix matrixX_P = new Matrix(n);
			LUP_decomposition.LUP(matrixX, matrixX_L, matrixX_U, matrixX_P);
			Vector vectorA = LUP_decomposition.SLAU(matrixX_L, matrixX_U, matrixX_P, vectorMoments);

			//Получем ответ по КФ
			double result = 0.0;
			for (int i = 0; i < n; i++)
			{
				result += vectorA.data[i] * f(vectorX.data[i]);
			}

			return result;
		}


		static private void FillMatrixCoeff(Matrix matrixCoeff, Vector vectorMoments)
		{
			for (int i = 0; i < matrixCoeff.N; i++)
			{
				for (int j = 0; j < matrixCoeff.N; j++)
				{
					matrixCoeff.data[i][j] = vectorMoments.data[j + i];
				}
			}
		}

		static public double KF_Gauss(ScalarFunk1 f, double defA, double a, double b, double alpha, int n)
		{
			//Вычисляем моменты от 0 до 2n - 1
			Vector vectorMoments = new Vector(2 * n);
			for (int i = 0; i < 2 * n; i++)
			{
				vectorMoments.data[i] = CountMoment(defA, a, b, alpha, i);
			}

			//Находим коеффициенты для W(x)
			Matrix matrixCoeff = new Matrix(n);
			FillMatrixCoeff(matrixCoeff, vectorMoments);
			Matrix matrixCoeff_L = new Matrix(n);
			Matrix matrixCoeff_U = new Matrix(n);
			Matrix matrixCoeff_P = new Matrix(n);
			LUP_decomposition.LUP(matrixCoeff, matrixCoeff_L, matrixCoeff_U, matrixCoeff_P);
			Vector vectorB_coeff = new Vector(n);
			for (int i = 0; i < n; i++)
			{
				vectorB_coeff.data[i] = -vectorMoments.data[n + i];
			}
			Vector vectorCoeff = LUP_decomposition.SLAU(matrixCoeff_L, matrixCoeff_U, matrixCoeff_P, vectorB_coeff);

			//Находим узлы из W(x)
			Vector vectorX = new Vector(n);
			ScalarFunk1 funkW = (double X) =>
			{
				double resultFunc = 0.0;
				for (int i = 0; i <= n; i++)
				{
					resultFunc += (i != n) ? vectorCoeff.data[i] * Math.Pow(X, i) : Math.Pow(X, n);
				}
				return resultFunc;
			};
			ScalarFunk1 funkW_Derivative = (double X) =>
			{
				double resultFuncDerivative = 0.0;
				for (int i = 0; i <= n; i++)
				{
					resultFuncDerivative += (i != n) ? vectorCoeff.data[i] * Math.Pow(X, i - 1) * i : Math.Pow(X, n - 1) * n;
				}
				return resultFuncDerivative;
			};
			var resultCount = MethodNewton_Scalar.DefualtMethod(a, b, funkW, funkW_Derivative);
			vectorX.SetValues(resultCount.FirstElement);

			//Решаем СЛАУ и находим Aj
			Matrix matrixX = new Matrix(n);
			FillMatrixX(matrixX, vectorX);
			Matrix matrixX_L = new Matrix(n);
			Matrix matrixX_U = new Matrix(n);
			Matrix matrixX_P = new Matrix(n);
			LUP_decomposition.LUP(matrixX, matrixX_L, matrixX_U, matrixX_P);
			Vector vectorB_X = new Vector(n);
			for (int i = 0; i < n; i++)
			{
				vectorB_X.data[i] = vectorMoments.data[i];
			}
			Vector vectorA = LUP_decomposition.SLAU(matrixX_L, matrixX_U, matrixX_P, vectorB_X);

			//Получем ответ по КФ
			double result = 0.0;
			for (int i = 0; i < n; i++)
			{
				result += vectorA.data[i] * f(vectorX.data[i]);
			}

			return result;
		}

		public enum TypeKF { NewtonCots , Gauss }
		static public double SKF(TypeKF typeKF, ScalarFunk1 f, double defA, double a, double b, double alpha, int n, double h)
		{
			int k = (Int32)((b - a) / h);
			double result = 0.0;
			for (int i = 0; i < k; i++)
			{
				switch (typeKF)
				{
					case TypeKF.NewtonCots:
						result += IKF_NewtonCots(f, defA, a + i * h, a + (i + 1) * h, alpha, n);
						break;
					case TypeKF.Gauss:
						result += KF_Gauss(f, defA, a + i * h, a + (i + 1) * h, alpha, n);
						break;
				}
			}

			return result;
		}

		static public double AnalysisMethod(TypeKF typeKF, ScalarFunk1 f, double defA, double a, double b, double alpha, int n, double Eps)
		{
			double L = 2;
			Vector vectorH = new Vector(3);
			vectorH.data[1] = b - a;
			vectorH.data[2] = (b - a) / L;

			int countIter = 2;

			double result = 0.0;

			double tempR = Double.PositiveInfinity;
			do
			{
				countIter++;
				vectorH.data[0] = vectorH.data[1];
				vectorH.data[1] = vectorH.data[2];
				vectorH.data[2] = vectorH.data[1] / L;
				//Эйткен
				Vector vectorS = new Vector(3);
				vectorS.data[0] = SKF(typeKF, f, defA, a, b, alpha, n, vectorH.data[0]);
				vectorS.data[1] = SKF(typeKF, f, defA, a, b, alpha, n, vectorH.data[1]);
				vectorS.data[2] = SKF(typeKF, f, defA, a, b, alpha, n, vectorH.data[2]);

				double m = -Math.Log((vectorS.data[2] - vectorS.data[1]) / (vectorS.data[1] - vectorS.data[0])) / Math.Log(L);

				//Рижардсон
				
				Matrix matrix_Cs_J = new Matrix(3);
				for (int i = 0; i < matrix_Cs_J.N; i++)
				{
					for (int j = 0; j < matrix_Cs_J.N - 1; j++)
					{
						matrix_Cs_J.data[i][j] = Math.Pow(vectorH.data[i], m + j);
					}
					matrix_Cs_J.data[i][matrix_Cs_J.N - 1] = -1;
				}

				Matrix matrix_Cs_J_L = new Matrix(n);
				Matrix matrix_Cs_J_U = new Matrix(n);
				Matrix matrix_Cs_J_P = new Matrix(n);
				LUP_decomposition.LUP(matrix_Cs_J, matrix_Cs_J_L, matrix_Cs_J_U, matrix_Cs_J_P);

				Vector vector_Cs_J = LUP_decomposition.SLAU(matrix_Cs_J_L, matrix_Cs_J_U, matrix_Cs_J_P, -1 * vectorS);

				result = vector_Cs_J.data[2];
				tempR = vectorS.data[2] - result;
				//for (int i = 0; i < 2; i++)
				//{
				//	tempR += vector_Cs_J.data[i] * matrix_Cs_J.data[0][i];
				//}
			} while (Math.Abs(tempR) > Eps);
			Console.WriteLine($"Count iter: {countIter}");
			return result;
		}
	}
}