using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MathStat
{
	class RandomVariable
	{
		private void Set_PointVariationRange()
		{
			PointVariationRange = new List<InfoPoint>();
			foreach (var elementSample in Sample)
			{
				var point = PointVariationRange.SingleOrDefault(p => p.Value == elementSample);
				if (point != null)
				{
					point.Count++;
				}
				else
				{
					PointVariationRange.Add(new InfoPoint { Value = elementSample, Count = 1 });
				}
			}
		}

		private void Set_IntervalVariationRange()
		{		
			double maxElementSample = PointVariationRange.Last().Value + 1e-10;
			double minElementSample = PointVariationRange.First().Value;
			int countInterval = (int)Math.Floor(1 + 1.44 * Math.Log(Sample.Count));
			double stepInterval = (maxElementSample - minElementSample) / countInterval;
			IntervalVariationRange = new List<InfoInterval>();
			for (int i = 0; i < countInterval; i++)
			{
				IntervalVariationRange.Add(new InfoInterval
				{
					BorderLeft = minElementSample + i * stepInterval,
					BorderRigth = minElementSample + (i + 1) * stepInterval,
					Count = 0
				});		
			}		
			foreach (var point in PointVariationRange)
			{
				IntervalVariationRange.Single(
					i => i.BorderLeft <= point.Value && i.BorderRigth > point.Value).Count += point.Count;
			}
		}

		private double Get_Moment(int r)
		{
			var momentR = 0.0;
			foreach (var point in PointVariationRange)
			{
				momentR += Math.Pow(point.Value, r) * point.Count;
			}
			return momentR / Sample.Count;
		}

		private double Get_CentralMoment(int r)
		{
			var centralMomentR = 0.0;
			foreach (var point in PointVariationRange)
			{
				centralMomentR += Math.Pow(point.Value - SampleMiddle, r) * point.Count;
			}
			return centralMomentR / Sample.Count;
		}

		private void ExportDataForHistogram()
		{
			using (ExcelPackage excel = new ExcelPackage())
			{
				//Создание листа
				excel.Workbook.Worksheets.Add("Histogram");
				var excelWorksheet = excel.Workbook.Worksheets["Histogram"];

				//Добавление заголовка
				List<string[]> headerRow = new List<string[]>();
				var rowIntervals = new string[IntervalVariationRange.Count];
				for (int i = 0; i < IntervalVariationRange.Count; i++)
				{
					rowIntervals[i] = "[" + IntervalVariationRange[i].BorderLeft.ToString("0.000") + "; " +  IntervalVariationRange[i].BorderRigth.ToString("0.000") + ")";
				}
				headerRow.Add(rowIntervals);

				string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
				excelWorksheet.Cells[headerRange].LoadFromArrays(headerRow);

				//Добавление содержимого
				var cellData = new List<object[]>();
				var rowCounts = new object[IntervalVariationRange.Count];
				for (int i = 0; i < IntervalVariationRange.Count; i++)
				{
					rowCounts[i] = Math.Round((IntervalVariationRange[i].Count + 0.0) / (Sample.Count + 0.0), 3);
				}
				cellData.Add(rowCounts);
				
				excelWorksheet.Cells[2, 1].LoadFromArrays(cellData);

				//Сохранение
				FileInfo excelFile = new FileInfo("Histogram.xlsx");
				excel.SaveAs(excelFile);
			}
		}

		private Func FuncLaplassa =
			(double x) => (1 / (Math.Sqrt(2 * Math.PI))) * 
			Integral.MiddleRectangle((double t) => 
			Math.Exp(- (t * t) / 2), 0, x, 100000);

		//Пункт 7
		public bool ChekHypothesis_NormalDistribution(double levelImportance)
		{
			var observedCounts = IntervalVariationRange;

			var expectedCounts = new List<InfoInterval>();
			for (int i = 0; i < observedCounts.Count; i++)
			{
				var expInterval = new InfoInterval
				{
					BorderLeft = observedCounts[i].BorderLeft,
					BorderRigth = observedCounts[i].BorderRigth
				};
				var valueLeft = (expInterval.BorderRigth - EvaluationMathExpressions) / 
					Math.Sqrt(EvaluationDispersion);
				var valueRight = (expInterval.BorderLeft - EvaluationMathExpressions) / 
					Math.Sqrt(EvaluationDispersion);

				var valueLaplassLeft = FuncLaplassa(valueLeft);
				var valueLaplassRight = FuncLaplassa(valueRight);

				expInterval.Count = (valueLaplassLeft - valueLaplassRight) * 
					Sample.Count;

				expectedCounts.Add(expInterval);
			}
			Console.WriteLine("\nIntervalVariationRange (observed / expected):");
			for (int i = 0; i < observedCounts.Count; i++)
				Console.WriteLine($"[{observedCounts[i].BorderLeft}; {observedCounts[i].BorderRigth}) " +
					$"({observedCounts[i].Count} / {expectedCounts[i].Count})");

			var R_observed = 0.0;
			for (int i = 0; i < observedCounts.Count; i++)
			{
				R_observed += Math.Pow(observedCounts[i].Count - expectedCounts[i].Count, 2) / 
					expectedCounts[i].Count;
			}
			Console.WriteLine($"\nR_observed: {R_observed}");

			//9,48773  13,27670
			Console.Write($"\nInput ChiSquared(p: {levelImportance}; r: {observedCounts.Count - 3}): ");
			var R_critical = Convert.ToDouble(Console.ReadLine());

			return R_observed <= R_critical;
		}

		public RandomVariable(List<double> sample)
		{
			Sample = sample;
			Sample.Sort();
			var N = Sample.Count;

			Set_PointVariationRange(); //Пункт 1
			Set_IntervalVariationRange(); //Пункт 2

			ExportDataForHistogram(); //Пункт 3

			//Пункт 4
			SampleMiddle = Get_Moment(1);
			SampleDispersion = Get_CentralMoment(2);
			SampleDispersion_corrected = N * SampleDispersion / (N - 1);
			SampleMiddleSquare = Math.Sqrt(SampleDispersion);

			SampleCoeffAsymmetry = (Math.Sqrt(N * (N - 1)) / (N - 2)) * 
				Get_CentralMoment(3) / Math.Pow(SampleMiddleSquare, 3);
			SampleCoeffExcess = ((N * N - 1) / ((N - 2) * (N - 3))) * 
				((Get_CentralMoment(4) / Math.Pow(Get_CentralMoment(2), 2))
				- 3 + (6 / (N + 1)));

			//Пункт 5
			EvaluationMathExpressions = SampleMiddle;
			EvaluationDispersion = SampleDispersion;
		}


		public void Print()
		{
			Console.Write("Sample (round 3): ");
			foreach (var sampleElement in Sample) Console.Write(sampleElement.ToString("0.000") + " ");
			Console.WriteLine($"\nSampleSize: {Sample.Count}");

			Console.WriteLine("\nPointVariationRange:");
			foreach (var point in PointVariationRange) Console.WriteLine($"{point.Value} ({point.Count})");

			Console.WriteLine("\nIntervalVariationRange:");
			foreach (var interval in IntervalVariationRange) Console.WriteLine($"[{interval.BorderLeft}; {interval.BorderRigth}) ({interval.Count})");

			Console.WriteLine("\nValues:");
			Console.WriteLine($"SampleMiddle: {SampleMiddle}");
			Console.WriteLine($"SampleDispersion: {SampleDispersion}");
			Console.WriteLine($"SampleDispersion_corrected: {SampleDispersion_corrected}");
			Console.WriteLine($"SampleMiddleSquare: {SampleMiddleSquare}");
			Console.WriteLine($"SampleCoeffAsymmetry: {SampleCoeffAsymmetry}");
			Console.WriteLine($"SampleCoeffExcess: {SampleCoeffExcess}");

			Console.WriteLine($"\nEvaluationMathExpressions: {EvaluationMathExpressions}");
			Console.WriteLine($"EvaluationDispersion: {EvaluationDispersion}");
		}

		//Пункт 6
		public void ConfidenceInterval_print(double coefConfidence)
		{
			Console.WriteLine($"\nConfidenceInterval (alpha: {coefConfidence})________");
			Console.WriteLine($"For M(X) (alpha: {coefConfidence}):");
			//1,9842 & 2,6264
			Console.Write($"Input StudentValue (p: {(coefConfidence + 1) / 2}; n: {Sample.Count - 1}): ");
			var studentValue = Convert.ToDouble(Console.ReadLine());
			var delta_M = studentValue * 
				Math.Sqrt(SampleDispersion / (Sample.Count - 1));
			var leftBorderM = SampleMiddle - delta_M;
			var rigthBorderM = SampleMiddle + delta_M;
			Console.WriteLine($"Interval: ({leftBorderM}; {rigthBorderM})");

			Console.WriteLine($"For D(X) (alpha: {coefConfidence}):");
			//128,422 & 138,987
			Console.Write($"Input ChiSquared1(p: {(1 + coefConfidence) / 2}; n: {Sample.Count - 1}): ");
			var chiSquared1 = Convert.ToDouble(Console.ReadLine());
			//73,361 & 66,510
			Console.Write($"Input ChiSquared2(p: {(1 - coefConfidence) / 2}; n: {Sample.Count - 1}): ");
			var chiSquared2 = Convert.ToDouble(Console.ReadLine());

			var leftBorderD = SampleDispersion * Sample.Count / chiSquared1;
			var rigthBorderD = SampleDispersion * Sample.Count / chiSquared2;
			Console.WriteLine($"Interval: ({leftBorderD}; {rigthBorderD})");
		}


		public List<double> Sample { get; private set; }

		public class InfoPoint
		{
			public double Value { get; set; }
			public double Count { get; set; }
		}
		public List<InfoPoint> PointVariationRange { get; private set; }

		public class InfoInterval
		{
			public double BorderLeft { get; set; }
			public double BorderRigth { get; set; }
			public double Count { get; set; }
		}
		public List<InfoInterval> IntervalVariationRange { get; private set; }

		public double SampleMiddle { get; private set; }

		public double SampleDispersion { get; private set; }

		public double SampleDispersion_corrected { get; private set; }

		public double SampleMiddleSquare { get; private set; }

		public double SampleCoeffAsymmetry { get; private set; }

		public double SampleCoeffExcess { get; private set; }


		public double EvaluationMathExpressions { get; private set; }

		public double EvaluationDispersion { get; private set; }
	}
}
