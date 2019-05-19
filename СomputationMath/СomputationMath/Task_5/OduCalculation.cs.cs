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
				var beginVector = new Vector(Y0.Length);
				beginVector.SetValues(Y0);

				var result = new Vector(Y0.Length);

				var K = new Vector[s];
				K[0] = new Vector(Y0.Length);
				K[0].SetValues(f(X0, Y0));

				result.Copy(K[0]);
				for (int i = 1; i < s; i++)
				{
					var tempX = X0 + C[i] * h;
					var tempY = beginVector;
					for (int j = 0; j < i; j++)
					{
						tempY += A[i][j] * K[j];
					}
					K[i] = new Vector(Y0.Length);
					K[i].SetValues(f(tempX, tempY.data));

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
			B[1] = 1 / (2 * c2);
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

		public static Vector[] Result_ConstH(double x0, double xN, double[] Y0, MethodRK rk, double h)
		{
			int n = (int)((xN - x0) / h);

			var result = new Vector[n];

			var vectorY = new Vector(Y0.Length);
			vectorY.SetValues(Y0);

			double X;

			for (int i = 0; i < n; i++)
			{
				result[i] = new Vector(vectorY.data);
				X = x0 + h * i;
				vectorY += h * rk(X, vectorY.data, h);
			}
			return result;
		}

		public static Vector GetRs(double x0, double xN, double[] Y0, MethodRK rk, int k, int p)
		{
			var R = new Vector(k);

			double h = 1 / Math.Pow(2, 0);
			var resPred = Result_ConstH(x0, xN, Y0, rk, h).Last();
			for (int i = 1; i < k; i++)
			{
				h = 1 / Math.Pow(2, i);
				var resTemp = Result_ConstH(x0, xN, Y0, rk, h).Last();

				R.data[i - 1] = ((1 / (1 - Math.Pow(2, -p))) * (resTemp - resPred)).Norm();
				if (i == k - 1)
				{
					R.data[i] = ((1 / (Math.Pow(2, p) - 1)) * (resTemp - resPred)).Norm();
				}
				resPred.Copy(resTemp);
			}
			return R;
		}
	}

}