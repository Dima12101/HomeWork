using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace СomputationMath.Task_5
{
	class OduCalculation
	{
		public delegate Vector MethodRK(double X0, double[] Y0, double h);
		
		public static MethodRK CreateMethod(double[] C, double[] B, double[][] A, VectorFunk f)
		{
			if (C.Length != B.Length || B.Length != A.Length) throw new Exception("Not correct size!");
			int s = B.Length; //шаги
			return (double X0, double[] Y0, double h) =>
			{
				var beginVector = new Vector(Y0);

				var K = new Vector[s];
				K[0] = new Vector(f(X0, Y0));

				var result = B[0] * new Vector(K[0].data);
				for (int i = 1; i < s; i++)
				{
					var tempX = X0 + C[i] * h;
					var tempY = new Vector(beginVector.data);
					for (int j = 0; j < i; j++)
					{
						tempY += h * A[i][j] * K[j];
					}
					K[i] = new Vector(f(tempX, tempY.data));

					result += B[i] * K[i];
				}
				return result;
			};
		}

		public static MethodRK CreateMethod_byC2_P2S2(double c2, VectorFunk f)
		{
			var C = new double[2];
			C[0] = 0;
			C[1] = c2;
			var B = new double[2];
			B[1] = 1.0 / (2 * c2);
			B[0] = 1 - B[1];
			var A = new double[2][];
			A[0] = new double[2] { 0, 0 };
			A[1] = new double[2] { c2, 0 };

			return CreateMethod(C, B, A, f);
		}

		#region Стандартные методы
		public static MethodRK GetMethod_MiddlePoint_P2S2(VectorFunk f)
		{
			var C = new double[2];
			C[0] = 0;
			C[1] = 0.5;
			var B = new double[2];
			B[0] = 0;
			B[1] = 1;
			var A = new double[2][];
			A[0] = new double[2] { 0, 0 };
			A[1] = new double[2] { 0.5, 0 };

			return CreateMethod(C, B, A, f);
		}

		public static MethodRK GetMethod_Trapeze_P2S2(VectorFunk f)
		{
			var C = new double[2];
			C[0] = 0;
			C[1] = 1;
			var B = new double[2];
			B[0] = 0.5;
			B[1] = 0.5;
			var A = new double[2][];
			A[0] = new double[2] { 0, 0 };
			A[1] = new double[2] { 1, 0 };

			return CreateMethod(C, B, A, f);
		}

		public static MethodRK GetMethod_Hoyna_P3S3(VectorFunk f)
		{
			var C = new double[3];
			C[0] = 0;
			C[1] = 1.0 / 3.0;
			C[2] = 2.0 / 3.0;
			var B = new double[3];
			B[0] = 1.0 / 4.0;
			B[1] = 0;
			B[2] = 1.0 / 4.0;
			var A = new double[3][];
			A[0] = new double[3] { 0, 0, 0 };
			A[1] = new double[3] { 1.0 / 3.0, 0, 0 };
			A[2] = new double[3] { 0, 2.0 / 3.0, 0 };

			return CreateMethod(C, B, A, f);
		}

		public static MethodRK GetMethod_Sympson_P3S3(VectorFunk f)
		{
			var C = new double[3];
			C[0] = 0;
			C[1] = 1.0 / 2.0;
			C[2] = 1;
			var B = new double[3];
			B[0] = 1.0 / 6.0;
			B[1] = 4.0 / 6.0;
			B[2] = 1.0 / 6.0;
			var A = new double[3][];
			A[0] = new double[3] { 0, 0, 0 };
			A[1] = new double[3] { 1.0 / 2.0, 0, 0 };
			A[2] = new double[3] { -1, 2, 0 };

			return CreateMethod(C, B, A, f);
		}

		public static MethodRK GetMethod_TheRK_P4S4(VectorFunk f)
		{
			var C = new double[4];
			C[0] = 0;
			C[1] = 1.0 / 2.0;
			C[2] = 1.0 / 2.0;
			C[3] = 1;
			var B = new double[4];
			B[0] = 1.0 / 6.0;
			B[1] = 2.0 / 6.0;
			B[2] = 2.0 / 6.0;
			B[3] = 1.0 / 6.0;
			var A = new double[4][];
			A[0] = new double[4] { 0, 0, 0, 0 };
			A[1] = new double[4] { 1.0 / 2.0, 0, 0, 0 };
			A[2] = new double[4] { 0, 1.0 / 2.0, 0, 0 };
			A[3] = new double[4] { 0, 0, 1, 0 };

			return CreateMethod(C, B, A, f);
		}

		public static MethodRK GetMethod_Rule38_P4S4(VectorFunk f)
		{
			var C = new double[4];
			C[0] = 0;
			C[1] = 1.0 / 3.0;
			C[2] = 2.0 / 3.0;
			C[3] = 1;
			var B = new double[4];
			B[0] = 1.0 / 8.0;
			B[1] = 3.0 / 8.0;
			B[2] = 3.0 / 8.0;
			B[3] = 1.0 / 8.0;
			var A = new double[4][];
			A[0] = new double[4] { 0, 0, 0, 0 };
			A[1] = new double[4] { 1.0 / 3.0, 0, 0, 0 };
			A[2] = new double[4] { -1.0 / 3.0, 1, 0, 0 };
			A[3] = new double[4] { 1, -1, 1, 0 };

			return CreateMethod(C, B, A, f);
		}
		#endregion

		public static Pair<double, Vector>[] Result_ConstH(double x0, double xN, double[] Y0, MethodRK rk, double h)
		{
			//Кол-во шагов
			int n = (int)((xN - x0) / h);

			var result = new Pair<double, Vector>[n];

			var tempVectorY = new Vector(Y0);
			double tempX;

			for (int i = 0; i < n; i++)
			{
				tempX = x0 + h * i;

				result[i].FirstElement = tempX;
				result[i].SecondElement = new Vector(tempVectorY.data);
				
				//Получаем следующие значение в текущем x
				tempVectorY += h * rk(tempX, tempVectorY.data, h);
			}
			return result;
		}

		public static Pair<double, Vector>[] Result_VariableH(double x0, double xN, double[] Y0, MethodRK rk, int p, double h0,
			ref List<Pair<double, Pair<double, List<double>>>> accH,
			double rtol = 1e-6, double atol = 1e-12)
		{
			accH = new List< Pair<double, Pair<double, List<double> > > >();

			var result = new List<Pair<double, Vector>>();

			var tempVectorY = new Vector(Y0);
			double tempX = x0;

			//Текущий обычный шаг
			double h1 = h0;
			//Текущий половинный шаг
			double h2;

			//Текущий результат при обычном шаге
			var resultY_H1 = new Vector(Y0.Length);
			//Текущий результат при половинном шаге
			var resultY_H2_1 = new Vector(Y0.Length);
			//Текущий результат при двойном половинном шаге
			var resultY_H2_2 = new Vector(Y0.Length);

			var infoH = new Pair<double, Pair<double, List<double>>>();
			var AcceptH = double.NaN;
			var notAcceptHs = new List<double>();
			var isAcceptH = true;

			do
			{
				if (isAcceptH)
				{
					infoH = new Pair<double, Pair<double, List<double>>>
					{
						FirstElement = tempX,
						SecondElement = new Pair<double, List<double>>()
					};
					AcceptH = double.NaN;
					notAcceptHs = new List<double>();
					isAcceptH = false;
				}

				h2 = h1 / 2;

				//Рез на x и h1 (s1)
				resultY_H1.Copy(tempVectorY + h1 * rk(tempX, tempVectorY.data, h1));

				//Рез на x и h2 (s1/2)
				resultY_H2_1.Copy(tempVectorY + h2 * rk(tempX, tempVectorY.data, h2));
				//Рез на (x + h2) и h2 (s2)
				resultY_H2_2.Copy(resultY_H2_1 + h2 * rk(tempX + h2, resultY_H2_1.data, h2));

				//Погрешность по Рунге
				var R1 =  ((1.0 / (1 - Math.Pow(2, -p))) * (resultY_H2_2 - resultY_H1)).Norm();

				//Вычисляем tol = rtol*s1 + atol
				var tol = rtol * resultY_H1.Norm() + atol;

				//Проверяем все случаи
				//1. tol * 2^p < R
				if (R1 > tol * Math.Pow(2, p))
				{
					//Потворяем с этими значениями (нет занесения результата)
					tempVectorY = new Vector(resultY_H2_1.data); //берём s1/2

					notAcceptHs.Add(h1);
					isAcceptH = false;

					h1 /= 2; // уменьшаем шаг

				}
				//2.  tol < R <= tol * 2^p
				if (tol < R1 && R1 <= tol * Math.Pow(2, p))
				{
					result.Add(new Pair<double, Vector>
					{
						FirstElement = tempX,
						SecondElement = tempVectorY
					});	

					tempX += h1; //сдвигаемся
					tempVectorY = new Vector(resultY_H2_2.data); //берём s2

					AcceptH = h1;
					isAcceptH = true;

					h1 /= 2;// уменьшаем шаг
				}
				//3.  tol * (1 / 2^(p+1)) <= R <= tol
				if (tol * (1 / Math.Pow(2, p + 1)) <= R1 && R1 <= tol)
				{
					result.Add(new Pair<double, Vector>
					{
						FirstElement = tempX,
						SecondElement = tempVectorY
					});

					tempX += h1;//сдвигаемся
					tempVectorY = new Vector(resultY_H1.data);//берём s1

					AcceptH = h1;
					isAcceptH = true;
				}
				//4.  R < tol * (1 / 2^(p+1))
				if (R1 < tol * (1 / Math.Pow(2, p + 1)))
				{
					result.Add(new Pair<double, Vector>
					{
						FirstElement = tempX,
						SecondElement = tempVectorY
					});

					tempX += h1;//сдвигаемся
					tempVectorY = new Vector(resultY_H1.data);//берём s1

					AcceptH = h1;
					isAcceptH = true;

					h1 *= 2;// увеличиваем шаг
				}

				if (isAcceptH)
				{
					infoH.SecondElement = new Pair<double, List<double>>
					{
						FirstElement = AcceptH,
						SecondElement = notAcceptHs
					};
					accH.Add(infoH);
				}
			} while (tempX < xN);

			return result.ToArray();
		}

		public static Vector GetRs(double x0, double xN, double[] Y0, MethodRK rk, int k, int p)
		{
			/*
			 Получение точных полных погрешностей R на шагах h = 1 / (2^i) при i = 0..k
			 */

			var R = new Vector(k);

			double h = 1 / Math.Pow(2, 0); // начальная велечина шага

			//Значение Y на конце отрезка на предыдущей велечине шага
			var resPred = Result_ConstH(x0, xN, Y0, rk, h).Last().SecondElement;
			for (int i = 1; i < k; i++)
			{
				//Текущая велечина шага
				h = 1 / Math.Pow(2, i);
				//Значение Y на конце отрезка на текущей велечине шага
				var resTemp = Result_ConstH(x0, xN, Y0, rk, h).Last().SecondElement;

				//Погрешность для пред.шага
				var R1 = ((1 / (1 - Math.Pow(2, -p))) * (resTemp - resPred)).Norm();
				//Погрешность для тек.шага
				var R2 = ((1 / (Math.Pow(2, p) - 1)) * (resTemp - resPred)).Norm();

				R.data[i - 1] = Math.Max(R.data[i - 1], R1);
				R.data[i] = R2;

				resPred.Copy(resTemp);
			}
			return R;
		}

		public static double H_Opt(double x0, double xN, double[] Y0, MethodRK rk, int p, double tol)
		{
			var k = 10;

			var h1 = 1.0 / Math.Pow(2, k);
			var resultH1 = OduCalculation.Result_ConstH(x0, xN, Y0, rk, h1);

			var h2 = 1.0 / Math.Pow(2, k + 1);
			var resultH2 = OduCalculation.Result_ConstH(x0, xN, Y0, rk, h2);

			var R2 = ((1 / (Math.Pow(2, p) - 1)) * (resultH2.Last().SecondElement - resultH1.Last().SecondElement)).Norm();

			return h2 * Math.Pow(tol / R2, 1.0 / p); 
		}

		public static Pair<double, double>[] RealRsOnX(Pair<double, Vector>[] points, VectorFunk1 realValues)
		{
			var result = new Pair<double, double>[points.Length];

			for (int i = 0; i < points.Length; i++)
			{
				result[i] = new Pair<double, double>
				{
					FirstElement = points[i].FirstElement,
					SecondElement = (new Vector(realValues(points[i].FirstElement)) - points[i].SecondElement).Norm()
				};
			}

			return result;
		}

		public static Pair<int, int>[] CountCallF_onRtol(double x0, double xN, double[] Y0, MethodRK rk, int p, int s)
		{
			var h_begin = 1 / Math.Pow(2, 6);

			var rtol = 1e-4;

			var tempDegree = 4;

			var result = new Pair<int, int>[5];

			for (int i = 0; i < 5; i++)
			{
				var accH = new List<Pair<double, Pair<double, List<double>>>>();
				Result_VariableH(x0, xN, Y0, rk, p, h_begin, ref accH, rtol);

				var countAddCallRK = accH.Sum(infoH => infoH.SecondElement.SecondElement.Count());

				result[i] = new Pair<int, int>
				{
					FirstElement = tempDegree,
					SecondElement = s * (accH.Count() + countAddCallRK)
				};

				tempDegree++;
				rtol /= 10;
			}
			return result;
		}

	}

}