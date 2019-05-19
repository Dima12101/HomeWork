using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using СomputationMath.Task_1;
using СomputationMath.Task_2;
using СomputationMath.Task_4;
using СomputationMath.Task_5;

namespace СomputationMath
{
	class Program
	{
		static void PrintResult(Pair<double[], int[]> results)
		{
			var values = results.FirstElement;
			var iters = results.SecondElement;
			for (int i = 0; i < values.Length; i++)
			{
				Console.WriteLine("Result_" + i.ToString() + ": " + values[i].ToString("0.00000") + "; " + "Iter: " + iters[i]);
			}
		}

		static void RunnigStopWatch(Stopwatch stopWatch)
		{
			stopWatch.Start();
			for (int i = 0; i < 10e6; i++)
			{

			}
			stopWatch.Stop();
		}

		#region Задание 1
		static void Task_1()
		{
			using (StreamReader reader = new StreamReader("InputMatrixs.txt"))
			{
				var strLine = "";
				while (!reader.EndOfStream)
				{
					Console.Write("______________________________________\n");
					#region Считывание матрицы 
					strLine = reader.ReadLine();
					if (strLine == "")
					{
						break;
					}
					var sizeMatrix = Int32.Parse(strLine);
					var matrixA = new Matrix(sizeMatrix);
					for (int i = 0; i < sizeMatrix; i++)
					{
						strLine = reader.ReadLine();
						var values = strLine.Split(' ');
						for (int j = 0; j < sizeMatrix; j++)
						{
							matrixA.data[i][j] = Int32.Parse(values[j]);
						}
					}
					#endregion

					#region LU разложение
					var matrixL = new Matrix(sizeMatrix);
					var matrixU = new Matrix(sizeMatrix);
					var matrixP = new Matrix(sizeMatrix);
					LUP_decomposition.LUP(matrixA, matrixL, matrixU, matrixP);

					Console.Write('\n');
					Console.Write("Matrix L\n");
					matrixL.Show();

					Console.Write('\n');
					Console.Write("Matrix U\n");
					matrixU.Show();

					Console.Write('\n');
					Console.Write("Matrix P\n");
					matrixP.Show();

					Console.Write('\n');
					var checkMatrixLeft = matrixL * matrixU;
					Console.Write("Matrix L * U\n");
					checkMatrixLeft.Show();

					Console.Write('\n');
					var checkMatrixRigth = matrixP * matrixA;
					Console.Write("Matrix P * A\n");
					checkMatrixRigth.Show();
					#endregion

					#region Определитель
					Console.Write('\n');
					var detA = LUP_decomposition.Det(matrixL, matrixU, matrixP);
					Console.Write("Det A with LU: " + detA.ToString() + '\n');
					#endregion

					#region Обратная матрица
					Console.Write('\n');
					var A_reverse = LUP_decomposition.Reverse(matrixL, matrixU, matrixP);
					Console.Write("A_reverse with LU:\n");
					A_reverse.Show();

					Console.Write('\n');
					var CheckReverse_1 = matrixA * A_reverse;
					Console.Write("Check Reverse (A * A_reverse):\n");
					CheckReverse_1.Show();

					Console.Write('\n');
					var CheckReverse_2 = A_reverse * matrixA;
					Console.Write("Check Reverse (A_reverse * A):\n");
					CheckReverse_2.Show();
					#endregion

					#region СЛАУ
					Console.Write('\n');
					Console.Write("SLAU with LU:\n");
					Random rnd = new Random();
					Vector b = new Vector(matrixA.N);
					Console.Write("Generate vector b: ");
					for (int i = 0; i < b.data.Length; i++)
					{
						b.data[i] = rnd.Next() % 100;
						Console.Write(b.data[i].ToString("0.00") + ' ');
					}
					Console.Write("\nResult x: ");
					var x = LUP_decomposition.SLAU(matrixL, matrixU, matrixP, b);
					for (int i = 0; i < x.data.Length; i++)
					{
						Console.Write(x.data[i].ToString("0.00") + ' ');
					}
					Console.Write('\n');
					#endregion

					#region Число обусловленности
					Console.Write('\n');
					Console.Write("Condition number: ");
					var condNumber = matrixA.Norm() * LUP_decomposition.Reverse(matrixL, matrixU, matrixP).Norm();
					Console.Write(condNumber.ToString("0.00") + '\n');
					#endregion
				}
			}
		}
		#endregion'

		#region Задание 2
		static void Task_2()
		{
			var matrix = new Matrix(7);
			matrix.GenByDiagonalPred(3);

			Console.WriteLine("Generate matrix:");
			matrix.Show();

			Console.WriteLine("Generate vectorB:");
			var vectorB = new Vector(matrix.N);
			vectorB.Generate();
			vectorB.Show();

			Console.WriteLine("\nMethod LU_________________________________");
			var matrixL = new Matrix(matrix.N);
			var matrixU = new Matrix(matrix.N);
			var matrixP = new Matrix(matrix.N);
			LUP_decomposition.LUP(matrix, matrixL, matrixU, matrixP);
			Console.WriteLine("Result vector:");
			var vectorLU = LUP_decomposition.SLAU(matrixL, matrixU, matrixP, vectorB);
			vectorLU.Show();


			Console.WriteLine("\nGenerate begin VectorX:");
			var beginVectorX = new Vector(matrix.N);
			beginVectorX.Generate();
			beginVectorX.Show();

			Console.WriteLine("Method Yacoby_________________________________");
			var vectorYacoby = IterationMethods_SLAU.SLAU(matrix, vectorB, beginVectorX, IterationMethods_SLAU.Method.Yacoby);
			Console.Write("Result vector: ");
			vectorYacoby.Show();
			Console.Write("Vector LU: ");
			vectorLU.Show();
			Console.Write("Check with LU: ");
			(vectorYacoby - vectorLU).Show();


			Console.WriteLine("Method Zeydel_________________________________");
			var vectorZeydel = IterationMethods_SLAU.SLAU(matrix, vectorB, beginVectorX, IterationMethods_SLAU.Method.Zeydel);
			Console.Write("Result vector: ");
			vectorZeydel.Show();
			Console.Write("Vector LU: ");
			vectorLU.Show();
			Console.Write("Check with LU: ");
			(vectorZeydel - vectorLU).Show();
		}
		#endregion

		#region Задание 3
		static void InitVectorFunks(ScalarFunk1_N[] vectorFunks)
		{
			vectorFunks[0] = (double[] X) => Math.Cos(X[0] * X[1]) - Math.Exp(-3 * X[2]) + X[3] * Math.Pow(X[4], 2) - X[5] - Math.Sinh(2 * X[7]) * X[8] + 2 * X[9] + 2.0004339741653854440;
			vectorFunks[1] = (double[] X) => Math.Sin(X[0] * X[1]) + X[2] * X[8] * X[6] - Math.Exp(-X[9] + X[5]) + 3 * Math.Pow(X[4], 2) - X[5] * (X[7] + 1) + 10.886272036407019994;
			vectorFunks[2] = (double[] X) => X[0] - X[1] + X[2] - X[3] + X[4] - X[5] + X[6] - X[7] + X[8] - X[9] - 3.1361904761904761904;
			vectorFunks[3] = (double[] X) => 2 * Math.Cos(-X[8] + X[3]) + X[4] / (X[2] + X[0]) - Math.Sin(Math.Pow(X[1], 2)) + Math.Pow(Math.Cos(X[6] * X[9]), 2) - X[7] - 0.170747270502230475;
			vectorFunks[4] = (double[] X) => Math.Sin(X[4]) + 2 * X[7] * (X[2] + X[0]) - Math.Exp(-X[6] * (-X[9] + X[5])) + 2 * Math.Cos(X[1]) - 1 / (X[3] - X[8]) - 0.368589627310127786;
			vectorFunks[5] = (double[] X) => Math.Exp(X[0] - X[3] - X[8]) + (Math.Pow(X[4], 2) / X[7]) + 0.5 * Math.Cos(3 * X[9] * X[1]) - X[5] * X[2] + 2.049108601677187511;
			vectorFunks[6] = (double[] X) => Math.Pow(X[1], 3) * X[6] - Math.Sin(X[9] / X[4] + X[7]) + (X[0] - X[5]) * Math.Cos(X[3]) + X[2] - 0.738043007620279801;
			vectorFunks[7] = (double[] X) => X[4] * Math.Pow(X[0] - 2 * X[5], 2) - 2 * Math.Sin(-X[8] + X[2]) + 1.5 * X[3] - Math.Exp(X[1] * X[6] + X[9]) + 3.566832198969380904;
			vectorFunks[8] = (double[] X) => 7 / X[5] + Math.Exp(X[4] + X[3]) - 2 * X[1] * X[7] * X[9] * X[6] + 3 * X[8] - 3 * X[0] - 8.439473450838325749;
			vectorFunks[9] = (double[] X) => X[9] * X[0] + X[8] * X[1] - X[7] * X[2] + Math.Sin(X[3] + X[4] + X[5]) * X[6] - 0.78238095238095238096;
		}

		static void InitMatrixFunks(ScalarFunk1_N[][] matrixFunks)
		{
			matrixFunks[0] = new ScalarFunk1_N[10];
			matrixFunks[0][0] = (double[] X) => -Math.Sin(X[0] * X[1]) * X[1];
			matrixFunks[0][1] = (double[] X) => -Math.Sin(X[0] * X[1]) * X[0];
			matrixFunks[0][2] = (double[] X) => 3.0 * Math.Exp(-(3 * X[2]));
			matrixFunks[0][3] = (double[] X) => X[4] * X[4];
			matrixFunks[0][4] = (double[] X) => 2 * X[3] * X[4];
			matrixFunks[0][5] = (double[] X) => -1;
			matrixFunks[0][6] = (double[] X) => 0;
			matrixFunks[0][7] = (double[] X) => -2 * Math.Cosh(2 * X[7]) * X[8];
			matrixFunks[0][8] = (double[] X) => -Math.Sinh(2 * X[7]);
			matrixFunks[0][9] = (double[] X) => 2;
			matrixFunks[1] = new ScalarFunk1_N[10];
			matrixFunks[1][0] = (double[] X) => Math.Cos(X[0] * X[1]) * X[1];
			matrixFunks[1][1] = (double[] X) => Math.Cos(X[0] * X[1]) * X[0];
			matrixFunks[1][2] = (double[] X) => X[8] * X[6];
			matrixFunks[1][3] = (double[] X) => 0;
			matrixFunks[1][4] = (double[] X) => 6 * X[4];
			matrixFunks[1][5] = (double[] X) => -Math.Exp(-X[9] + X[5]) - X[7] - 1;
			matrixFunks[1][6] = (double[] X) => X[2] * X[8];
			matrixFunks[1][7] = (double[] X) => -X[5];
			matrixFunks[1][8] = (double[] X) => X[2] * X[6];
			matrixFunks[1][9] = (double[] X) => Math.Exp(-X[9] + X[5]);
			matrixFunks[2] = new ScalarFunk1_N[10];
			matrixFunks[2][0] = (double[] X) => 1;
			matrixFunks[2][1] = (double[] X) => -1;
			matrixFunks[2][2] = (double[] X) => 1;
			matrixFunks[2][3] = (double[] X) => -1;
			matrixFunks[2][4] = (double[] X) => 1;
			matrixFunks[2][5] = (double[] X) => -1;
			matrixFunks[2][6] = (double[] X) => 1;
			matrixFunks[2][7] = (double[] X) => -1;
			matrixFunks[2][8] = (double[] X) => 1;
			matrixFunks[2][9] = (double[] X) => -1;
			matrixFunks[3] = new ScalarFunk1_N[10];
			matrixFunks[3][0] = (double[] X) => -X[4] * Math.Pow(X[2] + X[0], -2);
			matrixFunks[3][1] = (double[] X) => -2 * Math.Cos(X[1] * X[1]) * X[1];//
			matrixFunks[3][2] = (double[] X) => -X[4] * Math.Pow(X[2] + X[0], -2);
			matrixFunks[3][3] = (double[] X) => -2.0 * Math.Sin(-X[8] + X[3]);
			matrixFunks[3][4] = (double[] X) => 1.0 / (X[2] + X[0]);
			matrixFunks[3][5] = (double[] X) => 0;
			matrixFunks[3][6] = (double[] X) => -2 * Math.Cos(X[6] * X[9]) * Math.Sin(X[6] * X[9]) * X[9];
			matrixFunks[3][7] = (double[] X) => -1;
			matrixFunks[3][8] = (double[] X) => 2 * Math.Sin(-X[8] + X[3]);
			matrixFunks[3][9] = (double[] X) => -2 * Math.Cos(X[6] * X[9]) * Math.Sin(X[6] * X[9]) * X[6];
			matrixFunks[4] = new ScalarFunk1_N[10];
			matrixFunks[4][0] = (double[] X) => 2 * X[7];
			matrixFunks[4][1] = (double[] X) => -2 * Math.Sin(X[1]);
			matrixFunks[4][2] = (double[] X) => 2 * X[7];
			matrixFunks[4][3] = (double[] X) => Math.Pow(-X[8] + X[3], -2);
			matrixFunks[4][4] = (double[] X) => Math.Cos(X[4]);
			matrixFunks[4][5] = (double[] X) => X[6] * Math.Exp(-X[6] * (-X[9] + X[5]));
			matrixFunks[4][6] = (double[] X) => -(X[9] - X[5]) * Math.Exp(-X[6] * (-X[9] + X[5]));
			matrixFunks[4][7] = (double[] X) => (2 * X[2]) + 2 * X[0];
			matrixFunks[4][8] = (double[] X) => -Math.Pow(-X[8] + X[3], -2);
			matrixFunks[4][9] = (double[] X) => -X[6] * Math.Exp(-X[6] * (-X[9] + X[5]));
			matrixFunks[5] = new ScalarFunk1_N[10];
			matrixFunks[5][0] = (double[] X) => Math.Exp(X[0] - X[3] - X[8]);
			matrixFunks[5][1] = (double[] X) => -3.0 / 2.0 * Math.Sin(3 * X[9] * X[1]) * X[9];
			matrixFunks[5][2] = (double[] X) => -X[5];
			matrixFunks[5][3] = (double[] X) => -Math.Exp(X[0] - X[3] - X[8]);
			matrixFunks[5][4] = (double[] X) => 2 * X[4] / X[7];
			matrixFunks[5][5] = (double[] X) => -X[2];
			matrixFunks[5][6] = (double[] X) => 0;
			matrixFunks[5][7] = (double[] X) => -X[4] * X[4] * Math.Pow(X[7], (-2));
			matrixFunks[5][8] = (double[] X) => -Math.Exp(X[0] - X[3] - X[8]);
			matrixFunks[5][9] = (double[] X) => -3.0 / 2.0 * Math.Sin(3 * X[9] * X[1]) * X[1];
			matrixFunks[6] = new ScalarFunk1_N[10];
			matrixFunks[6][0] = (double[] X) => Math.Cos(X[3]);
			matrixFunks[6][1] = (double[] X) => 3 * X[1] * X[1] * X[6];
			matrixFunks[6][2] = (double[] X) => 1;
			matrixFunks[6][3] = (double[] X) => -(X[0] - X[5]) * Math.Sin(X[3]);
			matrixFunks[6][4] = (double[] X) => Math.Cos(X[9] / X[4] + X[7]) * X[9] * Math.Pow(X[4], -2);
			matrixFunks[6][5] = (double[] X) => -Math.Cos(X[3]);
			matrixFunks[6][6] = (double[] X) => Math.Pow(X[1], 3);
			matrixFunks[6][7] = (double[] X) => -Math.Cos(X[9] / X[4] + X[7]);
			matrixFunks[6][8] = (double[] X) => 0;
			matrixFunks[6][9] = (double[] X) => -Math.Cos(X[9] / X[4] + X[7]) / X[4];
			matrixFunks[7] = new ScalarFunk1_N[10];
			matrixFunks[7][0] = (double[] X) => 2 * X[4] * (X[0] - 2 * X[5]);
			matrixFunks[7][1] = (double[] X) => -X[6] * Math.Exp(X[1] * X[6] + X[9]);
			matrixFunks[7][2] = (double[] X) => -2 * Math.Cos(-X[8] + X[2]);
			matrixFunks[7][3] = (double[] X) => 0.15e1;
			matrixFunks[7][4] = (double[] X) => Math.Pow(X[0] - 2 * X[5], 2);
			matrixFunks[7][5] = (double[] X) => -4 * X[4] * (X[0] - 2 * X[5]);
			matrixFunks[7][6] = (double[] X) => -X[1] * Math.Exp(X[1] * X[6] + X[9]);
			matrixFunks[7][7] = (double[] X) => 0;
			matrixFunks[7][8] = (double[] X) => 2 * Math.Cos(-X[8] + X[2]);
			matrixFunks[7][9] = (double[] X) => -Math.Exp(X[1] * X[6] + X[9]);
			matrixFunks[8] = new ScalarFunk1_N[10];
			matrixFunks[8][0] = (double[] X) => -3;
			matrixFunks[8][1] = (double[] X) => -2 * X[7] * X[9] * X[6];
			matrixFunks[8][2] = (double[] X) => 0;
			matrixFunks[8][3] = (double[] X) => Math.Exp((X[4] + X[3]));
			matrixFunks[8][4] = (double[] X) => Math.Exp((X[4] + X[3]));
			matrixFunks[8][5] = (double[] X) => -0.7e1 * Math.Pow(X[5], -2);
			matrixFunks[8][6] = (double[] X) => -2 * X[1] * X[7] * X[9];
			matrixFunks[8][7] = (double[] X) => -2 * X[1] * X[9] * X[6];
			matrixFunks[8][8] = (double[] X) => 3;
			matrixFunks[8][9] = (double[] X) => -2 * X[1] * X[7] * X[6];
			matrixFunks[9] = new ScalarFunk1_N[10];
			matrixFunks[9][0] = (double[] X) => X[9];
			matrixFunks[9][1] = (double[] X) => X[8];
			matrixFunks[9][2] = (double[] X) => -X[7];
			matrixFunks[9][3] = (double[] X) => Math.Cos(X[3] + X[4] + X[5]) * X[6];
			matrixFunks[9][4] = (double[] X) => Math.Cos(X[3] + X[4] + X[5]) * X[6];
			matrixFunks[9][5] = (double[] X) => Math.Cos(X[3] + X[4] + X[5]) * X[6];
			matrixFunks[9][6] = (double[] X) => Math.Sin(X[3] + X[4] + X[5]);
			matrixFunks[9][7] = (double[] X) => -X[2];
			matrixFunks[9][8] = (double[] X) => X[1];
			matrixFunks[9][9] = (double[] X) => X[0];
		}

		static void Task_3()
		{
			#region Решение скалярного уравнения
			Console.WriteLine("ScalarEquation: x - sin(x) = 0.25");
			ScalarFunk1 func1 = (double X) => (X - Math.Sin(X) - 0.25);
			ScalarFunk1 funcDer1 = (double X) => (1 - Math.Cos(X));
			double a1 = 0.0;
			double b1 = 17.5;
			Console.WriteLine("Mode: Difference");
			var diff_results1 = MethodNewton_Scalar.DifferenceMethod(a1, b1, func1);
			PrintResult(diff_results1);
			Console.WriteLine("Mode: Simplified");
			var sim_results1 = MethodNewton_Scalar.SimplifiedMethod(a1, b1, func1, funcDer1);
			PrintResult(sim_results1);
			Console.WriteLine("Mode: Defualt");
			var def_results1 = MethodNewton_Scalar.DefualtMethod(a1, b1, func1, funcDer1);
			PrintResult(def_results1);

			Console.WriteLine("\nScalarEquation: x^3 = e^x - 1");
			ScalarFunk1 func2 = (double X) => (Math.Pow(X, 3) - Math.Pow(Math.E, X) + 1);
			ScalarFunk1 funcDer2 = (double X) => (3 * Math.Pow(X, 2) - Math.Pow(Math.E, X));
			double a2 = -2.0;
			double b2 = 2.0;
			Console.WriteLine("Mode: Difference");
			var diff_results2 = MethodNewton_Scalar.DifferenceMethod(a2, b2, func2);
			PrintResult(diff_results2);
			Console.WriteLine("Mode: Simplified");
			var sim_results2 = MethodNewton_Scalar.SimplifiedMethod(a2, b2, func2, funcDer2);
			PrintResult(sim_results2);
			Console.WriteLine("Mode: Defualt");
			var def_results2 = MethodNewton_Scalar.DefualtMethod(a2, b2, func2, funcDer2);
			PrintResult(def_results2);
			#endregion

			#region Решение СНАУ
			Console.WriteLine("\nSNAU________________________________________________");
			//Задаём вектор функций системы 
			var vectorFunks = new ScalarFunk1_N[10];
			InitVectorFunks(vectorFunks);

			//Задём матрицу Якоби системы
			var matrixFunks = new ScalarFunk1_N[10][];
			InitMatrixFunks(matrixFunks);

			//Задаём стартовое приближение
			var vectorStart = new Vector(vectorFunks.Length);
			//x5 = -0.5 | -0.2
			vectorStart.SetValues(new double[] { 0.5, 0.5, 1.5, -1.0, -0.2, 1.5, 0.5, -0.5, 1.5, -1.5 });
			Console.Write("Vector start: ");
			vectorStart.Show();

			//Кол-во прераций на LUP и СЛАУ
			var countMathOper_LUP = (matrixFunks.Length * matrixFunks.Length * (matrixFunks.Length + 1)) / 2;
			var countMathOper_SLAU = matrixFunks.Length * (2 * matrixFunks.Length + 5);

			//Инструмент для подсчёта времени
			var stopWatch = new Stopwatch();

			RunnigStopWatch(stopWatch);
			//Console.WriteLine($"Time: {stopWatch.ElapsedMilliseconds} milliseconds");
			stopWatch.Reset();

			//Чистый дефолтный метод Ньютона
			Console.WriteLine("\nSingle: #Defualt_method");
			var vectorResultDef_pair = MethodNewton_SNAU.Defualt_withSLAU(vectorStart, matrixFunks, vectorFunks, stopWatch);
			Console.WriteLine($"Time: {stopWatch.ElapsedTicks} ticks");
			Console.WriteLine("Count math.operation: " +
				$"{(countMathOper_LUP + countMathOper_SLAU + 10) * vectorResultDef_pair.SecondElement}");
			Console.WriteLine($"Count iter: {vectorResultDef_pair.SecondElement}");
			Console.Write("Result vector: ");
			vectorResultDef_pair.FirstElement.Show();
			Console.Write("Check vector: ");
			var checkVec_def = new Vector(vectorFunks.Length);
			checkVec_def.SetValueByFunks(vectorFunks, vectorResultDef_pair.FirstElement.data);
			checkVec_def.Show();

			//stopWatch.Reset();

			//Чистый модифицированный метод Ньютона
			Console.WriteLine("\nSingle: #Modified_Method");
			var vectorResultMod_pair = MethodNewton_SNAU.Modified_withSLAU(vectorStart, matrixFunks, vectorFunks, stopWatch);
			Console.WriteLine($"Time: {stopWatch.ElapsedTicks} ticks");
			Console.WriteLine("Count math.operation: " +
				$"{countMathOper_LUP + (countMathOper_SLAU + 10) * vectorResultMod_pair.SecondElement}");
			Console.WriteLine($"Count iter: {vectorResultMod_pair.SecondElement}");
			Console.Write("Result vector: ");
			vectorResultMod_pair.FirstElement.Show();
			Console.Write("Check vector: ");
			var checkVec_mod = new Vector(vectorFunks.Length);
			checkVec_mod.SetValueByFunks(vectorFunks, vectorResultMod_pair.FirstElement.data);
			checkVec_mod.Show();

			stopWatch.Reset();

			//Чистый гибридный метод Ньютона
			Console.WriteLine("\nSingle: #Hybrid_Method");
			var iterRecount = 4;
			Console.WriteLine($"Iter recount: {iterRecount}");
			var vectorResultHyb_pair = MethodNewton_SNAU.Hybrid_withSLAU(vectorStart, matrixFunks, vectorFunks, iterRecount, stopWatch);
			Console.WriteLine($"Time: {stopWatch.ElapsedTicks} ticks");
			Console.WriteLine("Count math.operation: " +
				$"{countMathOper_LUP * (1 + (vectorResultDef_pair.SecondElement / iterRecount)) + (countMathOper_SLAU + 1) * vectorResultDef_pair.SecondElement}");
			Console.WriteLine($"Count iter: {vectorResultHyb_pair.SecondElement}");
			Console.Write("Result vector: ");
			vectorResultHyb_pair.FirstElement.Show();

			//Thread.Sleep(10000);
			stopWatch.Reset();

			//Прогон на k итераций деф.методом, а потом добивка мод.методом
			var k = 4;
			Console.WriteLine("\nMerge: #Def_method->#Mod_Method");
			var vectorResultMerge_pair = MethodNewton_SNAU.MergeMethods(vectorStart, matrixFunks, vectorFunks, stopWatch, k);
			Console.WriteLine($"Time: {stopWatch.ElapsedTicks} ticks");
			Console.WriteLine("Count math.operation: " +
				$"{countMathOper_LUP * (1 + k) + (countMathOper_SLAU + 1) * vectorResultDef_pair.SecondElement }");
			Console.WriteLine($"Count iter: {vectorResultMerge_pair.SecondElement}");
			Console.Write("Result vector: ");
			vectorResultMerge_pair.FirstElement.Show();

			stopWatch.Reset();
			#endregion
		}
		#endregion

		#region Задание 4
		static void Task_4()
		{
			//Вариант 13
			double alpha = 0.2;
			double a = 1.5;
			double b = 2.3;
			double defA = a;
			ScalarFunk1 f = (double X) => 2 * Math.Cos(3.5 * X) * Math.Exp(5 * X / 3) + 3 * Math.Sin(1.5 * X) * Math.Exp(-4 * X) + 3;
			ScalarFunk1 p = (double X) => 1 / Math.Pow(X - a, alpha);

			ScalarFunk1 F = (double X) => f(X) * p(X);

			int countNode_MRR = 1000;
			double result_MRR = IntegralCalculation.MiddleRectangleRule(F, a, b, countNode_MRR);
			Console.WriteLine($"Result by MiddleRectangleRule (CountNode: {countNode_MRR}): {result_MRR}");

			int countNode_IKF_NC = 3;
			double result_IKF_NC = IntegralCalculation.IKF_NewtonCots(f, defA, a, b, alpha, countNode_IKF_NC);
			Console.WriteLine($"Result by IKF_NewtonCots (CountNode: {countNode_IKF_NC}): {result_IKF_NC}");

			int countNode_KF_G = 3;
			double result_KF_G = IntegralCalculation.KF_Gauss(f, defA, a, b, alpha, countNode_KF_G);
			Console.WriteLine($"Result by KF_Gauss (CountNode: {countNode_KF_G}): {result_KF_G}");

			int countNode_SKF_NC = 3;
			int countPart_SKF_NC = 3;
			double h_SKF_NC = (b - a) / countPart_SKF_NC;
			double result_SKF_NC = IntegralCalculation.SKF(IntegralCalculation.TypeKF.NewtonCots, f, defA, a, b, alpha, countNode_SKF_NC, h_SKF_NC);
			Console.WriteLine($"Result by SKF IKF_NewtonCots (CountNode: {countNode_SKF_NC}; StepH: {h_SKF_NC};): {result_SKF_NC}");

			//__
			int L = 2;
			double result_SKF_NC_check = IntegralCalculation.SKF(IntegralCalculation.TypeKF.NewtonCots, f, defA, a, b, alpha, countNode_SKF_NC, h_SKF_NC / L);
			double Eps_SKF_NC_check = (result_SKF_NC_check - result_SKF_NC) / (1 - Math.Pow(2, -countNode_SKF_NC));

			double Eps_SKF_NC = 10e-15;
			double h_SKF_NC_opt = h_SKF_NC * Math.Pow(Eps_SKF_NC / Math.Abs(Eps_SKF_NC_check), 1 / countNode_SKF_NC);
			h_SKF_NC_opt = (b - a) / ((b - a) / h_SKF_NC_opt);
			double result_SKF_NC_better = IntegralCalculation.SKF(IntegralCalculation.TypeKF.NewtonCots, f, defA, a, b, alpha, countNode_SKF_NC, h_SKF_NC_opt);
			Console.WriteLine($"Result by SKF IKF_NewtonCots (CountNode: {countNode_SKF_NC}; StepH_opt: {h_SKF_NC_opt};; Eps: {Eps_SKF_NC}): {result_SKF_NC_better}");
			//__



			int countNode_SKF_G = 3;
			int countPart_SKF_G = 3;
			double h_SKF_G = (b - a) / countPart_SKF_G;
			double result_SKF_G = IntegralCalculation.SKF(IntegralCalculation.TypeKF.Gauss, f, defA, a, b, alpha, countNode_SKF_G, h_SKF_G);
			Console.WriteLine($"Result by SKF KF_Gauss (CountNode: {countNode_SKF_G}; StepH: {h_SKF_G};): {result_SKF_G}");


			int countNode_test = 3;
			double Eps = 10e-6; ;
			double result_test = IntegralCalculation.AnalysisMethod(IntegralCalculation.TypeKF.NewtonCots, f, defA, a, b, alpha, countNode_test, Eps);
			Console.WriteLine($"Result by AnalysisMethod : {result_test}");
		}
		#endregion

		static void Main(string[] args)
		{
			double c2 = 0.05;

			double x0 = 0;
			double xN = 5;

			double A = 3;
			double B = 3;
			double C = -3;

			var Y0 = new double[4] { 1, 1, A, 1 };

			VectorFunk f = (double X, double[] Y) =>
			{
				var resultY = new double[Y.Length];
				resultY[0] = 2 * X * Math.Pow(Math.Abs(Y[1]), 1 / B) * Math.Sign(Y[1]) * Y[3];
				resultY[1] = 2 * B * X * Math.Exp((B / C) * (Y[2] - A)) * Y[3];
				resultY[2] = 2 * C * X * Y[3];
				resultY[3] = -2 * X * Math.Log(Math.Abs(Y[0]));

				return resultY;
			};

			var methodC2_rk = OduCalculation.CreateMethod_byC2_P2S2(c2, f);
			var middlePoint_rk = OduCalculation.GetMethod_MiddlePoint_P2S2(f);

			//int k = 0;
			//double h = 1 / Math.Pow(2, 15);
			//var result_methodC2 = OduCalculation.Result_ConstH(x0, xN, Y0, methodC2_rk, h);
			//var result_middlePoint = OduCalculation.Result_ConstH(x0, xN, Y0, middlePoint_rk, h);

			int k = 7;
			var Rs_methodC2 = OduCalculation.GetRs(x0, xN, Y0, methodC2_rk, k, 2);
			Console.WriteLine("Rs methodC2: "); 
			Rs_methodC2.Show1();
			var Rs_MiddlePoint = OduCalculation.GetRs(x0, xN, Y0, middlePoint_rk, k, 2);
			Console.WriteLine("\nRs middlePoint: ");
			Rs_MiddlePoint.Show1();

			var tol = 1e-5;
			var h_last = 1.0 / Math.Pow(2, k - 1);
			var v1 = Math.Pow(tol / Rs_methodC2.data[k - 1], 0.5);
			var h_opt_c2 = h_last * v1;
			var v2 = Math.Pow(tol / Rs_MiddlePoint.data[k - 1], 0.5);
			var h_opt_mp = h_last * v2;


			//Console.Write("\nx: ");
			//for (int i = 0; i < 4; i++)
			//{
			//	Console.Write($" y{i}");
			//}

			//for (int i = 0; i < 100; i++)
			//{
			//	string tempPoint = $"({x0 + i * h}".Replace(',', '.') + ", " + $"{result_methodC2[i].data[0]})".Replace(',', '.');
			//	Console.Write($"{tempPoint}, ");
			//}

			//Console.WriteLine("\nResult methodC2:");
			//for (int i = 0; i < 20; i++)
			//{
			//	Console.Write($"\n{x0 + i * h}: ");
			//	result_methodC2[i].Show();
			//}

			//Console.WriteLine("Result middlePoint:");
			//for (int i = 0; i < 20; i++)
			//{
			//	Console.Write($"\ny{i}: ");
			//	result_middlePoint[i].Show();
			//}
		}
	}
}
