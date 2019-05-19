using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using СomputationMath.Task_1;

namespace СomputationMath.Task_2
{
	static class MethodNewton_SNAU
	{
		#region СНАУ с итерациями через обратную матрицу
		static public Vector Modified_withReverse(ScalarFunk_N[][] matrixFunks, ScalarFunk_N[] vectorFunks, double Eps = 10e-5)
		{
			Console.Write("Begin vector: ");
			var vectorX_next = new Vector(vectorFunks.Length);
			vectorX_next.SetValues(new double[] { 0.5, 0.5, 1.5, -1.0, -0.5, 1.5, 0.5, -0.5, 1.5, -1.5 });
			vectorX_next.Show();

			Console.WriteLine("Fill matrix: ");
			var matrixJ = new Matrix(matrixFunks.Length);
			matrixJ.SetValueByFunks(matrixFunks, vectorX_next.data);
			matrixJ.Show();

			Console.WriteLine("Reverse matrix with LU: ");
			var matrixJ_L = new Matrix(matrixJ.N);
			var matrixJ_U = new Matrix(matrixJ.N);
			var matrixJ_P = new Matrix(matrixJ.N);
			LUP_decomposition.LUP(matrixJ, matrixJ_L, matrixJ_U, matrixJ_P);
			var matrixJ_reverse = LUP_decomposition.Reverse(matrixJ_L, matrixJ_U, matrixJ_P);
			matrixJ_reverse.Show();

			var vectorF = new Vector(vectorFunks.Length);
			var vectorX_current = new Vector(vectorFunks.Length);

			var countIter = 0;
			do
			{
				countIter++;

				vectorX_current.Copy(vectorX_next);
				vectorF.SetValueByFunks(vectorFunks, vectorX_current.data);
				vectorX_next = vectorX_current - matrixJ_reverse * vectorF;

				Console.Write("Next vector: ");
				vectorX_next.Show();
			} while ((vectorX_next - vectorX_current).Norm() >= Eps);

			Console.Write("Count iter: " + countIter.ToString() + '\n');
			return vectorX_next;
		}

		static public Vector Defualt_withReverse(ScalarFunk_N[][] matrixFunks, ScalarFunk_N[] vectorFunks, double Eps = 10e-5)
		{
			Console.Write("Begin vector: ");
			var vectorX_next = new Vector(vectorFunks.Length);
			vectorX_next.SetValues(new double[] { 0.5, 0.5, 1.5, -1.0, -0.5, 1.5, 0.5, -0.5, 1.5, -1.5 });
			vectorX_next.Show();

			var vectorF = new Vector(vectorFunks.Length);
			var vectorX_current = new Vector(vectorFunks.Length);

			var countIter = 0;
			do
			{
				countIter++;
				//Fill Yacoby
				var matrixJ = new Matrix(matrixFunks.Length);
				matrixJ.SetValueByFunks(matrixFunks, vectorX_next.data);
				//Reverse
				var matrixJ_L = new Matrix(matrixJ.N);
				var matrixJ_U = new Matrix(matrixJ.N);
				var matrixJ_P = new Matrix(matrixJ.N);
				LUP_decomposition.LUP(matrixJ, matrixJ_L, matrixJ_U, matrixJ_P);
				var matrixJ_reverse = LUP_decomposition.Reverse(matrixJ_L, matrixJ_U, matrixJ_P);

				vectorX_current.Copy(vectorX_next);
				vectorF.SetValueByFunks(vectorFunks, vectorX_current.data);
				vectorX_next = vectorX_current - matrixJ_reverse * vectorF;

				Console.Write("Next vector: ");
				vectorX_next.Show();
			} while ((vectorX_next - vectorX_current).Norm() >= Eps);

			Console.Write("Count iter: " + countIter.ToString() + '\n');
			return vectorX_next;
		}
		#endregion

		static public Pair<Vector, int> Defualt_withSLAU(Vector vectorStart, ScalarFunk_N[][] matrixFunks, ScalarFunk_N[] vectorFunks,
			Stopwatch stopWatch, int iterExit = Int32.MaxValue, double EpsSwitch = 10e-6, double Eps = 10e-6)
		{
			//Итерации вида: J * deltaX = -F; Матрица J пересчитывается на каждой итерации
			stopWatch.Start();

			var vectorF = new Vector(vectorFunks.Length);

			var matrixJ = new Matrix(matrixFunks.Length);
			var matrixJ_L = new Matrix(matrixJ.N);
			var matrixJ_U = new Matrix(matrixJ.N);
			var matrixJ_P = new Matrix(matrixJ.N);

			//Приближения
			var vectorX_next = new Vector(vectorFunks.Length);
			vectorX_next.Copy(vectorStart);
			var vectorX_current = new Vector(vectorFunks.Length);

			//Прибавка
			var deltaX = new Vector(vectorFunks.Length);

			var countIter = 0;
			do
			{
				//Счётчик итераций
				countIter++;

				vectorX_current.Copy(vectorX_next);

				//Инициализация матрицы J новым приближением
				matrixJ.SetValueByFunks(matrixFunks, vectorX_current.data);

				//LUP разложение матрицы J
				LUP_decomposition.LUP(matrixJ, matrixJ_L, matrixJ_U, matrixJ_P);

				//Инициализация вектора F новым приближением
				vectorF.SetValueByFunks(vectorFunks, vectorX_current.data);

				//Вычисление deltaX решением СЛАУ через LUP разложение
				deltaX = LUP_decomposition.SLAU(matrixJ_L, matrixJ_U, matrixJ_P, -1 * vectorF);

				//Получаем следующие приближение
				vectorX_next = vectorX_current + deltaX;

				//Вывод значений следующего приближения в консоль
				stopWatch.Stop();
				Console.Write($"#{countIter} Next vector: ");
				vectorX_next.Show();
				stopWatch.Start();

			} while (deltaX.Norm() >= Eps && (deltaX.Norm() >= EpsSwitch && countIter < iterExit));

			stopWatch.Stop();
			return new Pair<Vector, int> { FirstElement = vectorX_next, SecondElement = countIter };
		}

		static public Pair<Vector, int> Modified_withSLAU(Vector vectorStart, ScalarFunk_N[][] matrixFunks, ScalarFunk_N[] vectorFunks,
		Stopwatch stopWatch, int iterExit = Int32.MaxValue, double EpsSwitch = 10e-6, double Eps = 10e-6)
		{
			//Итерации вида: J * deltaX = -F; Матрица J инициализируется единожды
			stopWatch.Start();

			//Приближения
			var vectorX_next = new Vector(vectorFunks.Length);
			vectorX_next.Copy(vectorStart);
			var vectorX_current = new Vector(vectorFunks.Length);

			//Прибавка
			var deltaX = new Vector(vectorFunks.Length);

			var vectorF = new Vector(vectorFunks.Length);

			//Инициализация матрицы J
			var matrixJ = new Matrix(matrixFunks.Length);
			matrixJ.SetValueByFunks(matrixFunks, vectorX_next.data);

			//LUP разложение матрицы J
			var matrixJ_L = new Matrix(matrixJ.N);
			var matrixJ_U = new Matrix(matrixJ.N);
			var matrixJ_P = new Matrix(matrixJ.N);
			LUP_decomposition.LUP(matrixJ, matrixJ_L, matrixJ_U, matrixJ_P);

			var countIter = 0;
			do
			{
				//Счётчик итераций
				countIter++;

				vectorX_current.Copy(vectorX_next);

				//Инициализация вектора F новым приближением
				vectorF.SetValueByFunks(vectorFunks, vectorX_current.data);

				//Вычисление deltaX решением СЛАУ через LUP разложение
				deltaX = LUP_decomposition.SLAU(matrixJ_L, matrixJ_U, matrixJ_P, -1 * vectorF);

				//Получаем следующие приближение
				vectorX_next = vectorX_current + deltaX;

				//Вывод значений следующего приближения в консоль
				stopWatch.Stop();
				//Console.Write($"#{countIter} Next vector: ");
				//vectorX_next.Show();
				stopWatch.Start();
			} while (deltaX.Norm() >= Eps && (deltaX.Norm() >= EpsSwitch && countIter < iterExit));

			stopWatch.Stop();
			return new Pair<Vector, int> { FirstElement = vectorX_next, SecondElement = countIter };
		}

		static public Pair<Vector, int> Hybrid_withSLAU(Vector vectorStart, ScalarFunk_N[][] matrixFunks, ScalarFunk_N[] vectorFunks, int iterRecount,
			Stopwatch stopWatch, int iterExit = Int32.MaxValue, double EpsSwitch = 10e-6, double Eps = 10e-6)
		{
			//Итерации вида: J * deltaX = -F; Матрица J пересчитывается через каждые iterRecount итераций
			stopWatch.Start();

			var vectorF = new Vector(vectorFunks.Length);

			var matrixJ = new Matrix(matrixFunks.Length);
			var matrixJ_L = new Matrix(matrixJ.N);
			var matrixJ_U = new Matrix(matrixJ.N);
			var matrixJ_P = new Matrix(matrixJ.N);

			//Приближения
			var vectorX_next = new Vector(vectorFunks.Length);
			vectorX_next.Copy(vectorStart);
			var vectorX_current = new Vector(vectorFunks.Length);

			//Прибавка
			var deltaX = new Vector(vectorFunks.Length);

			var countIter = 0;
			do
			{
				//Счётчик итераций
				countIter++;

				vectorX_current.Copy(vectorX_next);

				if ((countIter - 1) % iterRecount == 0)
				{
					//Инициализация матрицы J новым приближением
					matrixJ.SetValueByFunks(matrixFunks, vectorX_current.data);

					//LUP разложение матрицы J	
					LUP_decomposition.LUP(matrixJ, matrixJ_L, matrixJ_U, matrixJ_P);
				}

				//Инициализация вектора F новым приближением
				vectorF.SetValueByFunks(vectorFunks, vectorX_current.data);

				//Вычисление deltaX решением СЛАУ через LUP разложение
				deltaX = LUP_decomposition.SLAU(matrixJ_L, matrixJ_U, matrixJ_P, -1 * vectorF);

				//Получаем следующие приближение
				vectorX_next = vectorX_current + deltaX;

				//Вывод значений следующего приближения в консоль
				stopWatch.Stop();
				Console.Write($"#{countIter} Next vector: ");
				vectorX_next.Show();
				stopWatch.Start();

			} while (deltaX.Norm() >= Eps && (deltaX.Norm() >= EpsSwitch && countIter < iterExit));

			stopWatch.Stop();
			return new Pair<Vector, int> { FirstElement = vectorX_next, SecondElement = countIter };
		}

		static public Pair<Vector, int> MergeMethods(Vector vectorStart, ScalarFunk_N[][] matrixFunks, ScalarFunk_N[] vectorFunks,
			Stopwatch stopWatch, int iterSwitch)
		{
			int degreeEps = 6;
			double Eps = 1.0 / Math.Pow(10, degreeEps);
			double EpsSwitch = Eps;

			//Для автоматического перехода
			//EpsSwitch = 1.0 / Math.Pow(10, degreeEps / 2);
			//iterSwitch = Int32.MaxValue;

			Console.WriteLine($"Iter switch: {iterSwitch}");

			Console.WriteLine("Def method:");
			stopWatch.Start();
			var vectorResultDef_pair = Defualt_withSLAU(vectorStart, matrixFunks, vectorFunks,
				stopWatch, iterSwitch, EpsSwitch, Eps);
			stopWatch.Stop();

			Console.WriteLine("Mod method:");
			stopWatch.Start();
			var vectorResultMod_pair = Modified_withSLAU(vectorResultDef_pair.FirstElement, matrixFunks, vectorFunks, stopWatch);
			stopWatch.Stop();

			return new Pair<Vector, int>
			{
				FirstElement = vectorResultMod_pair.FirstElement,
				SecondElement = vectorResultDef_pair.SecondElement + vectorResultMod_pair.SecondElement
			};
		}
	}
}