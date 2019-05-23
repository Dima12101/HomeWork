using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace СomputationMath
{
	public struct Pair<Tfirst, Tsecond>
	{
		public Tfirst FirstElement { get; set; }

		public Tsecond SecondElement { get; set; }
	}

	public delegate double ScalarFunk1_N(double[] X);

	public delegate double ScalarFunk1(double X);

	public delegate double ScalarFunk2(double X, double Y);

	public delegate double[] VectorFunk(double X, double[] Y);

	public delegate double[] VectorFunk1(double X);

	class ExcelTool
	{
		private static ExcelTool instance = null;

		private ExcelTool()
		{
			excelFile = new FileInfo("Results.xlsx");
		}

		public static ExcelTool GetInstance()
		{
			if (instance == null)
				instance = new ExcelTool();
			return instance;
		}

		private static FileInfo excelFile;

		public void Export_points_4Func(string nameTable, Pair<double, Vector>[] points, VectorFunk1 realValues)
		{
			using (ExcelPackage excel = new ExcelPackage(excelFile))
			{
				//Создание листа
				if (excel.Workbook.Worksheets[nameTable] != null)
				{
					excel.Workbook.Worksheets.Delete(nameTable);
				}
				excel.Workbook.Worksheets.Add(nameTable);
				var excelWorksheet = excel.Workbook.Worksheets[nameTable];

				//Добавление заголовка
				List<string[]> headerRow = new List<string[]>();
				var row = new string[9] { "X",
					"Y1_calc", "Y1_real",
					"Y2_calc", "Y2_real",
					"Y3_calc", "Y3_real",
					"Y4_calc", "Y4_real" };
				
				headerRow.Add(row);

				string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
				excelWorksheet.Cells[headerRange].LoadFromArrays(headerRow);

				//Добавление содержимого
				var cellData = new List<object[]>();
				for (int i = 0; i < points.Length; i++)
				{
					var realValuesVector = realValues(points[i].FirstElement);
					var rowValues = new object[9]
					{
						points[i].FirstElement,
						points[i].SecondElement.data[0],
						realValuesVector[0],
						points[i].SecondElement.data[1],
						realValuesVector[1],
						points[i].SecondElement.data[2],
						realValuesVector[2],
						points[i].SecondElement.data[3],
						realValuesVector[3]
					};
					cellData.Add(rowValues);
				}

				excelWorksheet.Cells[2, 1].LoadFromArrays(cellData);

				//Сохранение
				excel.Save();
			}
		}

		public void Export_RealRsOnX(string nameTable, Pair<double, double>[] values)
		{
			using (ExcelPackage excel = new ExcelPackage(excelFile))
			{
				//Создание листа
				if (excel.Workbook.Worksheets[nameTable] != null)
				{
					excel.Workbook.Worksheets.Delete(nameTable);
				}
				excel.Workbook.Worksheets.Add(nameTable);
				var excelWorksheet = excel.Workbook.Worksheets[nameTable];

				//Добавление заголовка
				List<string[]> headerRow = new List<string[]>();
				var row = new string[2] { "X", "R"};

				headerRow.Add(row);

				string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
				excelWorksheet.Cells[headerRange].LoadFromArrays(headerRow);

				//Добавление содержимого
				var cellData = new List<object[]>();
				for (int i = 0; i < values.Length; i++)
				{
					var rowValues = new object[2]
					{
						values[i].FirstElement,
						values[i].SecondElement,	
					};
					cellData.Add(rowValues);
				}

				excelWorksheet.Cells[2, 1].LoadFromArrays(cellData);

				//Сохранение
				excel.Save();
			}
		}


	}

}